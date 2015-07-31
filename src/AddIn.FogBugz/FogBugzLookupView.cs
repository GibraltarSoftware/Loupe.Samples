using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Loupe.Extensibility.Client;
using Loupe.Extensibility.Data;
using Loupe.Extension.FogBugz.Internal;

namespace Loupe.Extension.FogBugz
{
    /// <summary>
    /// A session summary view that looks up a single FogBugz case and finds its related sessions
    /// </summary>
    public partial class FogBugzLookupView : UserControl, ISessionSummaryView
    {
        private readonly object m_Lock = new object(); //used for coordination between threads

        private IRepositoryContext m_Context;
        private RepositoryController m_Controller;
        private ISessionSummaryCollection m_SessionSummaries;
        private List<ISessionSummary> m_SelectedItems;
        private bool m_Initialized;
        private string m_Title;
        private string m_SelectedItemsLabel;

        //our current case information
        private volatile int m_CurrentCaseId;
        private List<Guid> m_CaseSessionIds; //PROTECTED BY LOCK

        private delegate void ThreadSafeDisplayCaseInformationInvoker(FogBugzCaseReader caseReader);

        /// <summary>
        /// Raised every time the title text for the view changes.
        /// </summary>
        public event EventHandler TitleChanged;

        /// <summary>
        /// Raised by the summary view whenever the selected session summaries have changed
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Raised by the summary view when the user indicates it should be made the active view.
        /// </summary>
        public event EventHandler SelectView;

        public FogBugzLookupView()
        {
            InitializeComponent();
            m_Title = "FogBugz Lookup";
            m_CurrentCaseId = -1;
            DisplayCase(null); //clear designer values
        }

        /// <summary>
        /// A display caption for the view (typically the Text property of a control or form)
        /// </summary>
        public string Title
        {
            get { return m_Title; }
            set
            {
                if (value != m_Title)
                {
                    m_Title = value;
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
        public void Initialize(IRepositoryContext context)
        {
            m_Context = context;
            m_Controller = (RepositoryController)m_Context.RepositoryController;

            m_Initialized = true;
        }

        /// <summary>
        /// Called by Loupe to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
            
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
        /// Called by the container to indicate this view is no longer the active summary view.
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
        /// Raise the TitleChanged event
        /// </summary>
        protected virtual void OnTitleChanged()
        {
            EventHandler tempEvent = TitleChanged;
            if (tempEvent != null)
            {
                tempEvent(this, new EventArgs());
            }
        }

        #endregion

        #region Private Properties and Methods

        private void PerformSearch()
        {
            if (!m_Initialized)
                return;

            // Avoid possibility of NullReference exception if we don't have a
            // candidate list of sessions to filter
            if (m_SessionSummaries == null)
                return;

            int caseId;
            if (!int.TryParse(CaseIdTextBox.Text, out caseId) || caseId <= 0)
                return;

            m_CurrentCaseId = caseId;

            UseWaitCursor = true;
            //since searching is a network call we need that to be on a background thread.
            ThreadPool.QueueUserWorkItem(AsyncPerformSearch, caseId);
        }

        private void AsyncPerformSearch(object state)
        {
            //since we're on the thread pool we MUST catch exceptions or the app will fail.
            try
            {
                int caseId = (int)state;

                //make sure we're still looking for this case id
                if (caseId != m_CurrentCaseId)
                    return;

                FogBugzCaseReader caseReader = new FogBugzCaseReader(m_Controller, caseId);
                m_CaseSessionIds = caseReader.SessionIds;
                UpdateSelectedSessions();
                ThreadSafeDisplayCaseInformation(caseReader);
            }
            catch (Exception ex)
            {
                m_Context.Log.ReportException(ex, RepositoryController.LogCategory, true, false);
            }
        }

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
                OnSelectionChanged();
            }
        }

        /// <summary>
        /// Displays the case information provided, or clears the display if no case is provided
        /// </summary>
        /// <param name="caseReader"></param>
        private void DisplayCase(FogBugzCaseReader caseReader)
        {
            if ((caseReader == null) || (string.IsNullOrEmpty(caseReader.Title)))
            {
                caseDetailsFlowPanel.Visible = false;
                TitleLabel.Text = string.Empty;
                ProjectLabel.Text = string.Empty;
                UpdatedLabel.Text = string.Empty;
                LatestSummaryLabel.Text = string.Empty;
            }
            else
            {
                TitleLabel.Text = caseReader.Title;
                ProjectLabel.Text = string.Format("{0}: {1} | {2} ({3})",
                    caseReader.Project,
                    caseReader.Area,
                    caseReader.Status,
                    caseReader.Priority);
                UpdatedLabel.Text = string.Format("{0} on {1} {2}",
                    caseReader.LastUpdatedBy,
                    caseReader.LastUpdated.ToShortDateString(),
                    caseReader.LastUpdated.ToShortTimeString());
                LatestSummaryLabel.Text = caseReader.LatestSummary;
                caseDetailsFlowPanel.Visible = true;
            }
        }

        private void OpenWebSiteCase()
        {
            int selectedCase;
            int.TryParse(CaseIdTextBox.Text, out selectedCase);
            if (selectedCase > 0)
            {
                m_Controller.WebSiteOpenCase(selectedCase);
            }
        }

        private void SetDisplaySummaries(ISessionSummaryCollection summaries)
        {
            lock(m_Lock)
            {
                m_SessionSummaries = summaries;

                //and if we have any session ids for our current case w need to recalculate the summaries
                if ((m_CaseSessionIds != null) && (m_CaseSessionIds.Count > 0))
                {
                    UpdateSelectedSessions();
                }
            }
        }

        private void ThreadSafeDisplayCaseInformation(FogBugzCaseReader caseReader)
        {
            if (m_CurrentCaseId != caseReader.CaseId)
                return; //they changed their mind before we completed, we're an interim result.

            if (InvokeRequired)
            {
                ThreadSafeDisplayCaseInformationInvoker invoker = ThreadSafeDisplayCaseInformation;
                Invoke(invoker, new object[] {caseReader});
            }
            else
            {
                //we're on the UI thread.  We only update these items from the UI thread so we're not bothering with a lock.
                try
                {
                    DisplayCase(caseReader);
                    m_SelectedItemsLabel = string.Format("associated with FogBugz case {0}", caseReader.CaseId);
                    OnSelectionChanged();
                }
                finally
                {
                    UseWaitCursor = false; //we set this true WAY WAY back when we started this whole search thing.
                }
            }
        }

        #endregion

        private void CaseIdLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWebSiteCase();
        }

        private void FogBugzSummaryView_Enter(object sender, EventArgs e)
        {
            //when control goes INTO us we want to raise the Select View event to make sure
            //we're the controlling session summary.
            OnSelectView();
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

        private void SearchButton_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void CaseIdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return )
            {
                e.Handled = true;
                PerformSearch();
            }
        }
    }
}
