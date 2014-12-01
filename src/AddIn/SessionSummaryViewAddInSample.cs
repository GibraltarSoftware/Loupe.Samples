using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.Test
{
    public partial class SessionSummaryViewAddInSample : UserControl, ISessionSummaryView
    {
        private const string ApplicationDescriptionItemName = "Description";
        private const string CommandLineItemName = "Command Line";
        private const string DnsDomainNameItemName = "DNS Domain";
        private const string HostNameItemName = "Host Name";
        private const string UserDomainNameItemName = "User Domain";
        private const string UserNameItemName = "Username";

        private ISessionSummaryCollection m_SessionSummaries;
        private List<ISessionSummary> m_SelectedItems;
        private string m_SelectedItemsLabel;
        private bool m_ViewIsActive; //tracks if we are active and need to keep our search active.

        private SessionMatchDelegate m_SessionQuery; //the current filter method

        private delegate bool SessionMatchDelegate(ISessionSummary summary); //prototype for our filter methods

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

        public SessionSummaryViewAddInSample()
        {
            InitializeComponent();

            //set up the combo box with the possible values to search by.
            cboColumnList.Items.Add(UserNameItemName);
            cboColumnList.Items.Add(UserDomainNameItemName);
            cboColumnList.Items.Add(HostNameItemName);
            cboColumnList.Items.Add(DnsDomainNameItemName);
            cboColumnList.Items.Add(CommandLineItemName);
            cboColumnList.Items.Add(ApplicationDescriptionItemName);

            btnSearch.Enabled = IsValid;
        }

        /// <summary>
        /// A display caption for the view (typically the Text property of a control or form)
        /// </summary>
        public string Title
        {
            get { return "Session Detail Search"; } 
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
            //we don't need any repository access, so we're not going to do anything with it.
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
            
        }

        /// <summary>
        /// Called by the container to indicate this view is now the active summary view.
        /// </summary>
        public void ActivateView()
        {
            //if we don't have a current set of search data, we need to get it.
            EnsureSearchCurrent();
            m_ViewIsActive = true;
        }

        /// <summary>
        /// Called by the contaioner to indicate this view is no longer the active summary view.
        /// </summary>
        public void DeactivateView()
        {
            m_ViewIsActive = false;
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

        #endregion

        #region Private Properties and Methods

        private void ClearSearch()
        {
            SetSelectedItems(null);
            btnSearch.Enabled = IsValid;
        }

        private void EnsureSearchCurrent()
        {
            if ((m_SessionSummaries == null) && (IsValid))
            {
                //we don't have current data - time to search
                PerformSearch();
            }
        }

        /// <summary>
        /// Indicates if the search input is sufficient to perform a search
        /// </summary>
        /// <returns></returns>
        private bool IsValid
        {
            get
            {
                bool isValid = (m_SessionQuery != null); //we have to have a query delegate...

                if (string.IsNullOrEmpty(txtValue.Text))
                {
                    isValid = false;
                }

                return isValid;
            }
        }

        private void PerformSearch()
        {
            try
            {
                searchProgressBar.Visible = true;
                btnSearch.Enabled = false;

                List<ISessionSummary> selectedItems = new List<ISessionSummary>();

                foreach (ISessionSummary summary in m_SessionSummaries)
                {
                    if (m_SessionQuery(summary))
                        selectedItems.Add(summary);
                }

                //update the label:  it may get pulled when the selected item event is raised.
                m_SelectedItemsLabel = string.Format("Sessions with {0} matching '{1}'", cboColumnList.SelectedText, txtValue.Text);

                SetSelectedItems(selectedItems);
            }
            finally
            {
                searchProgressBar.Visible = false;
                btnSearch.Enabled = IsValid;
            }
        }

        private void SetDisplaySummaries(ISessionSummaryCollection summaries)
        {
            m_SessionSummaries = summaries;

            //and if we're the active view then we need to re-search
            if ((m_ViewIsActive) && (IsValid))
            {
                PerformSearch();
            }
            else
            {
                //we need to clear the selected values since we should presume it has changed. 
                ClearSearch();
            }
        }

        private void SetSelectedItems(List<ISessionSummary> selectedItems)
        {
            //avoid a purely gratuitous null/null event.
            if (ReferenceEquals(selectedItems, null) && ReferenceEquals(m_SelectedItems, null))
            {
                return;
            }

            m_SelectedItems = selectedItems;

            OnSelectionChanged();
        }

        private bool MatchOnApplicationDescription(ISessionSummary summary)
        {
            return MatchString(summary.ApplicationDescription, txtValue.Text);
        }

        private bool MatchOnCommandLine(ISessionSummary summary)
        {
            return MatchString(summary.CommandLine, txtValue.Text);
        }

        private bool MatchOnDnsDomainName(ISessionSummary summary)
        {
            return MatchString(summary.DnsDomainName, txtValue.Text);
        }

        private bool MatchOnHostName(ISessionSummary summary)
        {
            return MatchString(summary.HostName, txtValue.Text);
        }

        private bool MatchOnUserDomainName(ISessionSummary summary)
        {
            return MatchString(summary.UserDomainName, txtValue.Text);
        }

        private bool MatchOnUserName(ISessionSummary summary)
        {
            return MatchString(summary.UserName, txtValue.Text);
        }

        private static bool MatchString(string summaryString, string search)
        {
            //This is a simple way to do our basic search.  By all means, write your own fancy one - 
            //or use LINQ or Lamda expressions or what have you.
            bool matchFound;

            string adjustedSearchValue = search.ToUpperInvariant(); //to perform case insensitive searches we have to force case.

            bool leadingWild = search.StartsWith("*");
            bool trailingWild = search.EndsWith("*");

            if (leadingWild)
            {
                adjustedSearchValue = adjustedSearchValue.Substring(adjustedSearchValue.IndexOf("*") + 1);
            }

            if (trailingWild)
            {
                adjustedSearchValue = adjustedSearchValue.Substring(0, adjustedSearchValue.LastIndexOf("*"));
            }

            if (leadingWild && trailingWild)
            {
                matchFound = summaryString.ToUpperInvariant().Contains(adjustedSearchValue);
            }
            else if (leadingWild)
            {
                matchFound = summaryString.ToUpperInvariant().EndsWith(adjustedSearchValue);
            }
            else if (trailingWild)
            {
                matchFound = summaryString.ToUpperInvariant().StartsWith(adjustedSearchValue);
            }
            else
            {
                //exact match
                matchFound = summaryString.ToUpperInvariant().Equals(adjustedSearchValue);
            }

            return matchFound;
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

        #endregion

        #region Event Handlers

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void cboColumnList_SelectedValueChanged(object sender, EventArgs e)
        {
            //update our delegate to do the right match method based on what they selected in the combo.
            switch ((string)cboColumnList.SelectedItem)
            {
                case ApplicationDescriptionItemName:
                    m_SessionQuery = MatchOnApplicationDescription;
                    break;
                case CommandLineItemName:
                    m_SessionQuery = MatchOnCommandLine;
                    break;
                case DnsDomainNameItemName:
                    m_SessionQuery = MatchOnDnsDomainName;
                    break;
                case HostNameItemName:
                    m_SessionQuery = MatchOnHostName;
                    break;
                case UserDomainNameItemName:
                    m_SessionQuery = MatchOnUserDomainName;
                    break;
                case UserNameItemName:
                    m_SessionQuery = MatchOnUserName;
                    break;
                default:
                    m_SessionQuery = null;
                    break;
            }

            //clear our existing results so we will do a new search.
            ClearSearch();
            btnSearch.Enabled = IsValid;
        }

        private void SessionSummaryViewAddInSample_Enter(object sender, EventArgs e)
        {
            //when control goes INTO us we want to raise the Select View event to make sure
            //we're the controlling session summary.
            OnSelectView();
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            //clear our existing results so we will do a new search.
            ClearSearch();
            btnSearch.Enabled = IsValid;
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            //if the user pressed Enter we want to go ahead and run the search.
            if ((e.Modifiers == 0) && (e.KeyCode == Keys.Enter))
            {
                if (IsValid)
                {
                    PerformSearch();
                }
            }
        }


        #endregion
    }
}
