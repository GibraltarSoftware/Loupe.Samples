using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.FindByUser
{
    public sealed partial class SessionFilterView : UserControl, ISessionSummaryView
    {
        private const int DataRetentionDays = 30;
        private readonly object _lock = new object(); //used for coordination between threads
        private bool _initialized;
        private IRepositoryAddInContext _context;
        private string _selectedUsername;

        private string _title;
        private ISessionSummaryCollection _sessionSummaries;
        private List<ISessionSummary> _selectedItems;
        private HashSet<Guid> _sessionIds;

        private delegate void ThreadSafeUpdateUserListInvoker(List<string> users);

        private class UserSessionQueryParameters
        {
            public string User { get; set; }
            public DateTime Date { get; set; }
        }

        public SessionFilterView()
        {
            InitializeComponent();
            _title = "Find Sessions by User";
        }

        private void UserLookupView_Enter(object sender, EventArgs e)
        {
            //when control goes INTO us we want to raise the Select View event to make sure
            //we're the controlling session summary.
            OnSelectView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshUserList();
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterSelectedItems();
        }

        private void calSessionDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            FilterSelectedItems();
        }

        private void RefreshUserList()
        {
            if (!Enabled || _context == null)
                return;

            Enabled = false;
            _selectedUsername = cboUser.SelectedIndex >= 0 ? cboUser.Items[cboUser.SelectedIndex].ToString() : null;
            cboUser.Items.Clear();
            cboUser.Items.Add("<All Users>");

            // Check if the database is available
            using (var db = FindByUserDatabase.GetDatabase(_context))
            {
                if (db == null || FindByUserDatabase.DatabaseUnavailable)
                {
                    ShowError = true;
                    return;
                }
            }

            ThreadPool.QueueUserWorkItem(AsyncLoadUserList, null);
        }

        private void AsyncLoadUserList(object state)
        {
            try
            {
                using (var db = FindByUserDatabase.GetDatabase(_context))
                {
                    ThreadSafeRefreshUsers(db.LoadUsernames(DataRetentionDays));
                }
            }
            catch (Exception ex)
            {
                _context.Log.RecordException(ex, FindByUserAddIn.LogCategory, true);
                ThreadSafeRefreshUsers(new List<string>());
                Enabled = true;
            }
        }

        private void ThreadSafeRefreshUsers(List<string> users)
        {
            if (InvokeRequired)
            {
                var del = new ThreadSafeUpdateUserListInvoker(ThreadSafeRefreshUsers);
                Invoke(del, users);
            }
            else
            {
                //we're on the UI thread.  We only update these items from the UI thread so we're not bothering with a lock.
                try
                {
                    foreach (var user in users)
                    {
                        cboUser.Items.Add(user);
                    }

                    if (_selectedUsername != null && cboUser.Items.Contains(_selectedUsername))
                    {
                        cboUser.SelectedItem = _selectedUsername;
                    }
                    else
                    {
                        cboUser.SelectedIndex = 0;
                        _selectedUsername = null;
                    }

                    calSessionDate.MinDate = DateTime.Now.Date.AddDays(-DataRetentionDays);
                    calSessionDate.MaxDate = DateTime.Now.Date;
                    Enabled = true;
                    FilterSelectedItems();
                }
                finally
                {
                    UseWaitCursor = false; //we set this true WAY WAY back when we started this whole search thing.
                    ShowError = FindByUserDatabase.DatabaseUnavailable;
                }
            }
        }

        private void FilterSelectedItems()
        {
            if (!_initialized || !Enabled)
                return;

            // Avoid possibility of NullReference exception if we don't have a
            // candidate list of sessions to filter
            if (_sessionSummaries == null)
                return;

            UseWaitCursor = true;

            calSessionDate.MonthlyBoldedDates = new[] {calSessionDate.SelectionStart};

            var state = new UserSessionQueryParameters
            {
                User = cboUser.SelectedIndex <= 0 ? null : cboUser.SelectedItem.ToString(),
                Date = calSessionDate.SelectionStart
            };

            ThreadPool.QueueUserWorkItem(AsyncLoadSessionIds, state);
        }

        private void AsyncLoadSessionIds(object state)
        {
            var args = (UserSessionQueryParameters)state;
            try
            {
                using (var db = FindByUserDatabase.GetDatabase(_context))
                {
                    var sessionIds = db.FindSessions(args.User, args.Date);
                    lock (_lock)
                    {
                        _sessionIds = sessionIds;
                    }
                }
            }
            catch (Exception ex)
            {
                _context.Log.ReportException(ex, FindByUserAddIn.LogCategory, true, false);
                lock (_lock)
                {
                    _sessionIds = new HashSet<Guid>();
                }
            }

            var del = new MethodInvoker(UpdateSelectedSessions);
            Invoke(del);
        }


        private void UpdateSelectedSessions()
        {
            //we're on the UI thread.  We only update these items from the UI thread so we're not bothering with a lock.
            try
            {
                lock (_lock)
                {
                    var selectedItems = new List<ISessionSummary>();
                    if ((_sessionIds != null) && (_sessionIds.Count > 0))
                    {
                        selectedItems.AddRange(_sessionSummaries.Where(summary => _sessionIds.Contains(summary.Id)));
                    }

                    _selectedItems = selectedItems;
                    OnSelectionChanged();
                }
            }
            finally
            {
                UseWaitCursor = false; //we set this true WAY WAY back when we started this whole search thing.
                if (FindByUserDatabase.DatabaseUnavailable)
                    ShowError = true;
            }
        }


        private void btnEditConfig_Click(object sender, EventArgs e)
        {
            _context.EditConfiguration();
            RefreshUserList();
        }

        private bool ShowError
        {
            get { return pnlBadConfig.Visible; }
            set
            {
                if (value)
                {
                    pnlBadConfig.Dock = DockStyle.Fill;
                    pnlBadConfig.Visible = true;
                }
                else
                {
                    pnlBadConfig.Visible = false;
                }
                Enabled = true;
                UseWaitCursor = false;
            }
        }

        #region ISessionSummaryView Implementation

        /// <summary>
        /// Raised by the summary view whenever the selected session summaries have changed
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Raised every time the title text for the view changes.
        /// </summary>
        public event EventHandler TitleChanged;

        /// <summary>
        /// Raised by the summary view when the user indicates it should be made the active view.
        /// </summary>
        public event EventHandler SelectView;

        /// <summary>
        /// Raise the SelectionChanged event
        /// </summary>
        private void OnSelectionChanged()
        {
            SelectedItemsLabel = string.Format("on {1} with {0}",
                cboUser.SelectedIndex > 0 ? cboUser.Text : "any user",
                calSessionDate.SelectionStart.ToShortDateString());
            EventHandler tempEvent = SelectionChanged;
            if (tempEvent != null)
            {
                tempEvent(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raise the TitleChanged event
        /// </summary>
        private void OnTitleChanged()
        {
            EventHandler tempEvent = TitleChanged;
            if (tempEvent != null)
            {
                tempEvent(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raise the SelectView Event
        /// </summary>
        private void OnSelectView()
        {
            EventHandler tempEvent = SelectView;
            if (tempEvent != null)
            {
                tempEvent(this, new EventArgs());
            }
        }

        /// <summary>
        /// A display caption for the view (typically the Text property of a control or form)
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnTitleChanged();
                }
            }
        }

        /// <summary>
        /// Gets the icon to use for the view
        /// </summary>
        /// <remarks>
        /// This may return null, in which case the container will make the best
        /// </remarks>
        public Icon Icon
        {
            get { return null; }
        }

        /// <summary>
        /// Called to initialize the session summary view.
        /// </summary>
        /// <param name="context">A standard interface to the hosting environment for the view, specific to the repository where the view was activated</param>
        /// <remarks>
        /// If any exception is thrown during this call this view will not be loaded.
        /// </remarks>
        public void Initialize(IRepositoryAddInContext context)
        {
            _context = context;
            _initialized = true;
            RefreshUserList();
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
            RefreshUserList();
        }

        /// <summary>
        /// Called by the container to indicate this view is now the active summary view.
        /// </summary>
        public void ActivateView()
        {
            //we have no specific action when we go active/inactive
        }

        /// <summary>
        /// Called by the contaioner to indicate this view is no longer the active summary view.
        /// </summary>
        public void DeactivateView()
        {
            //we have no specific action when we go active/inactive
        }

        /// <summary>
        /// Summary information on the set of all sessions available at this level.
        /// </summary>
        /// <remarks>
        /// This property will be set each time the eligible set of sessions changes, typically 
        /// due to refreshing data from the repository or a change in filtering that applies to the view.
        /// </remarks>
        public ISessionSummaryCollection SessionSummaries
        {
            get { return _sessionSummaries; }
            set
            {
                lock (_lock)
                {
                    _sessionSummaries = value;
                    UpdateSelectedSessions();
                }
            }
        }

        /// <summary>
        /// The currently selected sessions within the view.
        /// </summary>
        /// <remarks>
        /// Each time this property's value changes the SelectionChanged event must be raised.
        /// </remarks>
        public IList<ISessionSummary> SelectedItems
        {
            get { return _selectedItems; }
        }

        /// <summary>
        /// A label for the summary sessions currently selected.
        /// </summary>
        public string SelectedItemsLabel { get; private set; }

        #endregion ISessionSummaryView Implementation
    }
}
