using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Gibraltar.AddIn.FogBugz.Internal;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.FogBugz
{
    /// <summary>
    /// This viewer shows a list of FogBugz cases that are related to Gibraltar sessions.
    /// </summary>
    public partial class FogBugzSummaryView : UserControl, ISessionSummaryView
    {
        private const string DefaultTitle = "FogBugz Cases with Sessions";
        private readonly object m_Lock = new object();

        private IRepositoryAddInContext m_Context;
        private AddInController m_Controller;
        private ISessionSummaryCollection m_SessionSummaries;
        private Dictionary<Guid, ISessionSummary> m_SessionIds; //all of the ids of the sessions in session summaries, indexed.
        private List<ISessionSummary> m_SelectedItems;
        private string m_Title;
        private string m_SelectedItemsLabel;
        private bool m_Initialized;
        private FogBugzCaseList m_CaseList;
        private List<FogBugzCaseInfo> m_FilteredCaseList;

        //our current case information
        private volatile int m_CurrentCaseId;
        private List<Guid> m_CaseSessionIds; //PROTECTED BY LOCK
       
        /// <summary>
        /// Raised every time the title text for the view changes.
        /// </summary>
#pragma warning disable 67
        public event EventHandler TitleChanged;
#pragma warning restore 67

        /// <summary>
        /// Raised by the summary view whenever the selected session summaries have changed
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Raised by the summary view when the user indicates it should be made the active view.
        /// </summary>
        public event EventHandler SelectView;

        public FogBugzSummaryView()
        {
            InitializeComponent();

            caseListGridView.AutoGenerateColumns = false;
            m_Title = DefaultTitle;
        }

        #region Public Properties and Methods

        /// <summary>
        /// A display caption for the view (typically the Text property of a control or form)
        /// </summary>
        public string Title
        {
            get { return m_Title; }
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
            m_Context = context;
            m_Controller = (AddInController)context.Controller;
            m_Initialized = true;
            SetDisplayCase(null);
            ThreadSafeDisplayCaseList();
            PerformSearch();
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
            //since our search can contain elements from config, search again.
            PerformSearch();
        }

        /// <summary>
        /// Called by the container to indicate this view is now the active summary view.
        /// </summary>
        public void ActivateView()
        {
            if (!m_Initialized)
                return;

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
        ///             due to refreshing data from the repository or a change in filtering that applies to the view.
        /// </remarks>
        public ISessionSummaryCollection SessionSummaries
        {
            get { return m_SessionSummaries; }
            set { SetDisplaySummaries(value); }
        }

        /// <summary>
        /// The currently selected sessions within the view.
        /// </summary>
        /// <remarks>
        /// Each time this property's value changes the SelectionChanged event must be raised.
        /// </remarks>
        public IList<ISessionSummary> SelectedItems
        {
            get { return m_SelectedItems; }
        }

        /// <summary>
        /// A label for the summary sessions currently selected.
        /// </summary>
        public string SelectedItemsLabel
        {
            get { return m_SelectedItemsLabel; }
        }

        #endregion

        #region protected Properties and Methods

        /// <summary>
        /// Raise the SelectionChanged event
        /// </summary>
        protected virtual void OnSelectionChanged()
        {
            EventHandler tempEvent = SelectionChanged;
            if (tempEvent != null)
            {
                tempEvent(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raise the SelectView Event
        /// </summary>
        protected virtual void OnSelectView()
        {
            EventHandler tempEvent = SelectView;
            if (tempEvent != null)
            {
                tempEvent(this, new EventArgs());
            }
        }

        #endregion

        #region Private Properties and Methods

        private void PerformSearch()
        {
            // Ignore premature calls and recursive calls due to cascading events
            if (!m_Initialized)
                return;

            UseWaitCursor = true;
            //since searching is a network call we need that to be on a background thread.
            ThreadPool.QueueUserWorkItem(AsyncPerformSearch, m_Controller.Filter);
        }

        /// <summary>
        /// Perform our FogBugz interactions on a background thread
        /// </summary>
        /// <param name="state">The filter</param>
        private void AsyncPerformSearch(object state)
        {
            //because we're running from the threadpool we have to catch exceptions because they'd be fatal to the app
            try
            {
                string filter = (string)state;

                lock(m_Lock)
                {
                    //we're out of sync with the new filter; there's another update behind us.
                    if (m_Controller.Filter != filter)
                        return;
                }

                //this call may take real time, so don't do it in the lock.
                FogBugzCaseList newCaseList = new FogBugzCaseList(m_Controller, filter);

                lock(m_Lock)
                {
                    m_CaseList = newCaseList;

                    //update the case list to redact just those that have a session id we include.
                    CalculateSessionCases();
                }

                ThreadSafeDisplayCaseList();
            }
            catch(Exception ex)
            {
                m_Context.Log.ReportException(ex, AddInController.LogCategory, true, false);
            }
        }

        private void CalculateSessionCases()
        {
            lock(m_Lock)
            {
                m_FilteredCaseList = new List<FogBugzCaseInfo>();

                //if we have NO session ids then clearly no cases will match.
                if ((m_SessionIds != null) && (m_SessionIds.Count > 0)
                    && (m_CaseList != null) && (m_CaseList.Cases != null))
                {
                    foreach (FogBugzCaseInfo caseInfo in m_CaseList.Cases)
                    {
                        foreach (Guid sessionId in caseInfo.Sessions)
                        {
                            if (m_SessionIds.ContainsKey(sessionId))
                            {
                                m_FilteredCaseList.Add(caseInfo);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void SetDisplaySummaries(ISessionSummaryCollection summaries)
        {
            lock (m_Lock)
            {
                m_SessionSummaries = summaries;
                m_SessionIds = new Dictionary<Guid, ISessionSummary>();
                foreach (ISessionSummary summary in summaries)
                {
                    m_SessionIds.Add(summary.Id, summary);
                }

                CalculateSessionCases();
            }

            ThreadSafeDisplayCaseList();

            UpdateSelectedSessions();
        }

        private void OpenCase(FogBugzCaseInfo selectedCase)
        {
            if (selectedCase != null)
            {
                if (selectedCase.CaseId != 0)
                {
                    m_Controller.WebSiteOpenCase(selectedCase.CaseId);
                }
                else
                {
                    m_Controller.WebSiteOpen(string.Format("default.asp?pg=pgList&search=2&searchFor={0}", HttpUtility.UrlEncode("tag:Gibraltar " + m_Controller.Filter)));
                }
            }
        }

        /// <summary>
        /// Update the user interface with the latest case list information
        /// </summary>
        private void ThreadSafeDisplayCaseList()
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = ThreadSafeDisplayCaseList;
                Invoke(invoker);
            }
            else
            {
                //determine which secondary columns we want based on our filter.
                StatusColumn.Visible = (m_Controller.UserConfiguration.CaseStatusFilter == CaseStatus.All); //its set in the filter, no reason to display..

                //and how wide are we? if we're wide enough we can show update.
                UpdatedColumn.Visible = (Width > 500);

                //populate our grid of cases
                caseListGridView.DataSource = m_FilteredCaseList;
                caseListGridView.BindingContext= new BindingContext(); //forces it to load right now.

                //and select the first row.
                if (caseListGridView.Rows.Count > 0)
                {
                    caseListGridView.Rows[0].Selected = true;
                    //this will cause its own update call.
                }
                else
                {
                    UpdateSelectedSessions();
                }

                UseWaitCursor = false;
            }
        }

        private void SetDisplayCase(FogBugzCaseInfo selectedCase)
        {
            lock (m_Lock)
            {
                m_CurrentCaseId = (selectedCase == null) ? 0 : selectedCase.CaseId;

                if (m_CurrentCaseId <= 0)
                {
                    //we're in our "ALL/Default" mode
                    m_SelectedItemsLabel = "for " + m_Controller.FilterCaption;
                }
                else
                {
                    //we're looking at just one case.
                    m_SelectedItemsLabel = string.Format("associated with case {0}", m_CurrentCaseId);
                }

                //calculate all of the session Ids for this case.
                m_CaseSessionIds = (selectedCase == null) ? null : selectedCase.Sessions;
            }

            UpdateSelectedSessions();
        }

        /// <summary>
        /// Recalculate the selected sessions for the current selected case and the current set of all sessions
        /// </summary>
        private void UpdateSelectedSessions()
        {
            lock (m_Lock)
            {
                List<ISessionSummary> selectedItems = new List<ISessionSummary>();

                if ((m_CaseSessionIds != null) && (m_CaseSessionIds.Count > 0))
                {
                    foreach (ISessionSummary summary in m_SessionSummaries)
                    {
                        if (m_CaseSessionIds.Contains(summary.Id))
                            selectedItems.Add(summary);
                    }
                }

                m_SelectedItems = selectedItems;
            }

            OnSelectionChanged();
        }

        #endregion

        #region Event Handlers

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void FogBugzSummaryView_Enter(object sender, EventArgs e)
        {
            //when control goes INTO us we want to raise the Select View event to make sure
            //we're the controlling session summary.
            OnSelectView();
        }

        private void caseListGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = null;
            if (caseListGridView.SelectedRows.Count > 0)
            {
                selectedRow = caseListGridView.SelectedRows[0];
            }

            FogBugzCaseInfo caseInfo = (selectedRow == null) ? null : (FogBugzCaseInfo)selectedRow.DataBoundItem;

            lock(m_Lock)
            {
                SetDisplayCase(caseInfo);
            }
        }

        private void caseListGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex >= 0))
            {
                //they clicked the case Id, so we open it in a web browser.
                FogBugzCaseInfo caseInfo = (FogBugzCaseInfo)caseListGridView.Rows[e.RowIndex].DataBoundItem;
                OpenCase(caseInfo);
            }
        }

        #endregion
    }
}
