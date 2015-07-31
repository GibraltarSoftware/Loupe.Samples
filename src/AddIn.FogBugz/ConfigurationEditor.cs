using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Loupe.Extensibility.Client;
using Loupe.Extension.FogBugz.Internal;

namespace Loupe.Extension.FogBugz
{
    /// <summary>
    /// User interface dialog for managing the FogBugz integration configuration
    /// </summary>
    public partial class ConfigurationEditor : Form, IConfigurationEditor
    {
        private IRepositoryContext m_Context;
        private CommonConfig m_WorkingCommonConfig;
        private UserConfig m_WorkingUserConfig;
        private ServerConfig m_WorkingHubConfig;
        private readonly Color m_InvalidEntryColor;
        private bool m_ServerIsValid;
        private bool m_HubIsValid;
        private bool m_Loading;

        private Dictionary<string, List<String>> m_ProductsAndApplications;
        private Dictionary<string, List<String>> m_ProjectsAndAreas;
        private Dictionary<int, string> m_Priorities;
        private IRepositoryConfiguration m_WorkingConfiguration;

        public class NameValuePair<T>
        {
            public NameValuePair(string name, T value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; set; }

            public T Value { get; set; }
        }

        public ConfigurationEditor()
        {
            InitializeComponent();

            m_InvalidEntryColor = Color.LightPink;

            //prepopulate our combos
            NameValuePair<CaseStatus>[] statusList = new[] {
                                                             new NameValuePair<CaseStatus>("Any / All", CaseStatus.All),
                                                             new NameValuePair<CaseStatus>("Active", CaseStatus.Active),
                                                             new NameValuePair<CaseStatus>("Resolved", CaseStatus.Resolved),
                                                             new NameValuePair<CaseStatus>("Closed", CaseStatus.Closed)
                                                         };
            CaseStatusList.DataSource = statusList;
            CaseStatusList.DisplayMember = "Name";
            CaseStatusList.ValueMember = "Value";

            NameValuePair<LastUpdatedFilter>[] lastUpdateList = new[]
                                                                    {
                                                                        new NameValuePair<LastUpdatedFilter>("Any Time", LastUpdatedFilter.None),
                                                                        new NameValuePair<LastUpdatedFilter>("1 Year", LastUpdatedFilter.OneYear),
                                                                        new NameValuePair<LastUpdatedFilter>("3 Months", LastUpdatedFilter.ThreeMonths),
                                                                        new NameValuePair<LastUpdatedFilter>("1 Month", LastUpdatedFilter.OneMonth),
                                                                        new NameValuePair<LastUpdatedFilter>("1 Week", LastUpdatedFilter.OneWeek),
                                                                        new NameValuePair<LastUpdatedFilter>("1 Day", LastUpdatedFilter.OneDay),
                                                                    };

            LastUpdatedList.DataSource = lastUpdateList;
            LastUpdatedList.DisplayMember = "Name";
            LastUpdatedList.ValueMember = "Value";
        }

        #region Public Properties and Methods

        /// <summary>
        /// Called by Loupe to have the configuration editor display itself and edit the provided configuration
        /// </summary>
        /// <param name="context">The Add In Context provides a connection to the hosting environment.</param><param name="configuration">The current configuration.</param><param name="initialConfiguration">Indicates if the configuration has ever completed in the current environment.</param>
        /// <returns>
        /// DialogResult.OK if the configuration is complete and should be accepted as the new configuration.  Any other result to cancel.  If this
        ///             is the initial configuration and it is not OK the add in will not be enabled.
        /// </returns>
        public DialogResult EditConfiguration(IRepositoryContext context, IRepositoryConfiguration configuration, bool initialConfiguration)
        {
            m_Context = context;

            m_WorkingConfiguration = configuration;

            //make SURE we have a configuration.
            m_WorkingCommonConfig = configuration.Common as CommonConfig;
            m_WorkingHubConfig = configuration.Server as ServerConfig;
            m_WorkingUserConfig = configuration.User as UserConfig;

            if (m_WorkingCommonConfig == null)
            {
                m_WorkingCommonConfig = new CommonConfig();
            }

            if (m_WorkingHubConfig == null)
            {
                m_WorkingHubConfig = new ServerConfig();
            }

            if (m_WorkingUserConfig == null)
            {
                m_WorkingUserConfig = new UserConfig();
            }

            if (initialConfiguration)
            {
                //set our default tab to the correct tab
                mainTabControl.SelectedTab = serverTab;
            }
            else
            {
                mainTabControl.SelectedTab = mappingTab;
            }

            CaseStatusList.DataBindings.Add("SelectedValue", m_WorkingUserConfig, "CaseStatusFilter");
            LastUpdatedList.DataBindings.Add("SelectedValue", m_WorkingUserConfig, "LastUpdatedFilter");

            DialogResult result = ShowDialog();

            if (result == DialogResult.OK)
            {
                configuration.Common = m_WorkingCommonConfig;
                configuration.User = m_WorkingUserConfig;
                configuration.Server = m_WorkingHubConfig;
            }

            CaseStatusList.DataBindings.Clear();
            LastUpdatedList.DataBindings.Clear();

            return DialogResult.OK;
        }

        #endregion

        #region Protected Properties and Methods

        protected override void OnShown(EventArgs e)
        {
            m_Loading = true;

            try
            {
                //initialize our display from the current configuration
                txtServerUrl.Text = m_WorkingCommonConfig.Url;

                //and lets see if we have any credentials...
                string userName, password;
                m_WorkingConfiguration.GetUserCredentials(RepositoryController.ServerCredentialsKey, out userName, out password);

                txtLocalUserName.Text = userName;
                txtLocalPassword.Text = password;

                ValidateServer();

                if (m_WorkingConfiguration.ServerCredentialsAvailable)
                {
                    m_WorkingConfiguration.GetServerCredentials(RepositoryController.ServerCredentialsKey, out userName, out password);
                    txtHubUserName.Text = userName;
                    txtHubPassword.Text = password;
                    hubAccountInformation.Enabled = true;
                }
                else
                {
                    hubAccountInformation.Enabled = false;
                }

                ValidateHubServer();

                chkEnableAutomaticAnalysis.Checked = m_WorkingUserConfig.EnableAutomaticAnalysis || m_WorkingHubConfig.EnableAutomaticAnalysis;
                chkAnalyzeOnHub.Checked = m_WorkingHubConfig.EnableAutomaticAnalysis;

                hubAccountInformation.Visible = true;
                chkAnalyzeOnHub.Visible = true;

                UpdateValidState();

                //calculate up the items behind the grid cells
                //populate our grid of cases
                mappingsGrid.DataSource = m_WorkingCommonConfig;
                mappingsGrid.DataMember = "Mappings";
                mappingsGrid.BindingContext = new BindingContext(); //forces it to load right now.
            }
            finally
            {
                m_Loading = false;
            }


            base.OnShown(e);
        }

        #endregion

        #region Private Properties and Methods

        private void ActionAddMapping()
        {
            DisableRuleButtons();
            UseWaitCursor = true;

            //make sure we've loaded up information from FogBugz and our own mappings
            if ((m_ProductsAndApplications == null) || (m_ProjectsAndAreas == null))
            {
                string userName, password;
                m_WorkingConfiguration.GetUserCredentials(RepositoryController.ServerCredentialsKey, out userName, out password);

                if (string.IsNullOrEmpty(userName))
                {
                    MessageBox.Show("Please configure an account to log into FogBugz with before configuring mappings.", "FogBugz Account Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mainTabControl.SelectedTab = serverTab;
                    txtLocalUserName.Focus();
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(AsyncRetrieveOptions, new object[] { userName, password });
                }
            }
            else
            {
                //we can go strait to the add mapping dialog.
                ThreadSafeAddMapping();
            }
        }

        private void ActionEditMapping(Mapping mapping)
        {
            DisableRuleButtons();
            UseWaitCursor = true;

            //make sure we've loaded up information from FogBugz and our own mappings
            if ((m_ProductsAndApplications == null) || (m_ProjectsAndAreas == null))
            {
                ThreadPool.QueueUserWorkItem(AsyncRetrieveOptions, mapping);
            }
            else
            {
                //we can go strait to the add mapping dialog.
                ThreadSafeEditMapping(mapping);
            }
        }

        private void ActionRemoveMapping()
        {
            //find the selected row and remove it
            foreach (DataGridViewRow selectedRow in mappingsGrid.SelectedRows)
            {
                Mapping currentMapping = (Mapping)selectedRow.DataBoundItem;
                m_WorkingCommonConfig.Mappings.Remove(currentMapping);
            }
        }

        private void AsyncRetrieveOptions(object state)
        {
            //since we're called from the threadpool we have to catch exceptions or the application will fail.
            try
            {
                object[] args = (object[])state;
                string userName = (string)args[0];
                string password = (string)args[1];

                FBApi fogBugzServer = new FBApi(m_WorkingCommonConfig.Url, userName, password);
                m_ProjectsAndAreas = fogBugzServer.ListProjectsAndAreas();
                m_Priorities = fogBugzServer.ListPriorities();

                m_ProductsAndApplications = new Dictionary<string, List<string>>();
            }
            catch (Exception ex)
            {
                m_Context.Log.ReportException(ex, RepositoryController.LogCategory, true, false);
            }

            Mapping existingMapping = state as Mapping;
            if (existingMapping == null)
            {
                ThreadSafeAddMapping();
            }
            else
            {
                ThreadSafeEditMapping(existingMapping);
            }
        }

        private void DisableRuleButtons()
        {
            btnAddRule.Enabled = false;
            btnCopy.Enabled = false;
            btnRemoveRule.Enabled = false;
        }

        private void EnableRuleButtons()
        {
            btnAddRule.Enabled = true;
            ValidateMappings();
        }

        private void ThreadSafeAddMapping()
        {
            //what makes this thread safe is that it'll dispatch to the UI thread if necessary
            if (InvokeRequired)
            {
                MethodInvoker invoker = ThreadSafeAddMapping;
                Invoke(invoker);
            }
            else
            {
                try
                {
                    using (NewMappingDialog newMappingDialog = new NewMappingDialog())
                    {
                        Mapping newMapping;
                        DialogResult result = newMappingDialog.AddMapping(m_ProductsAndApplications, m_ProjectsAndAreas, m_Priorities, out newMapping);
                        if (result == DialogResult.OK)
                        {
                            m_WorkingCommonConfig.Mappings.Add(newMapping);
                        }
                    }

                }
                finally
                {
                    UseWaitCursor = false;
                    EnableRuleButtons();
                }
            }
        }

        private delegate void ThreadSafeEditMappingHandler(Mapping mapping);

        private void ThreadSafeEditMapping(Mapping mapping)
        {
            //what makes this thread safe is that it'll dispatch to the UI thread if necessary
            if (InvokeRequired)
            {
                ThreadSafeEditMappingHandler invoker = ThreadSafeEditMapping;
                Invoke(invoker, new object[] { mapping });
            }
            else
            {
                try
                {
                    using (NewMappingDialog newMappingDialog = new NewMappingDialog())
                    {
                        DialogResult result = newMappingDialog.EditMapping(m_ProductsAndApplications, m_ProjectsAndAreas, m_Priorities, mapping);
                        if (result == DialogResult.OK)
                        {
                            mappingsGrid.Refresh();
                        }
                    }
                }
                finally
                {
                    UseWaitCursor = false;
                    EnableRuleButtons();
                }
            }
        }

        private void ActionTestServer()
        {
            btnLocalTest.Enabled = false;
            try
            {
                UseWaitCursor = true;

                string userName, password;
                m_WorkingConfiguration.GetUserCredentials(RepositoryController.ServerCredentialsKey, out userName, out password);

                TestServerConnection(m_WorkingCommonConfig.Url, userName, password);
            }
            finally
            {
                btnLocalTest.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void ActionTestHubServer()
        {
            btnHubTest.Enabled = false;
            try
            {
                UseWaitCursor = true;

                string userName, password;
                m_WorkingConfiguration.GetServerCredentials(RepositoryController.ServerCredentialsKey, out userName, out password);

                TestServerConnection(m_WorkingCommonConfig.Url, userName, password);
            }
            finally
            {
                btnHubTest.Enabled = true;
                UseWaitCursor = false;
            }
        }

        private static void TestServerConnection(string url, string userName, string password)
        {
            try
            {
                //this does an immediate login
                FBApi testApi = new FBApi(url, userName, password);
                string message = "Successfully authenticated to the server at " + url;
                MessageBox.Show(message, "Connected to Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string message = "Unable to log into the server at " + url + ":\r\n\r\n" + ex.Message;
                MessageBox.Show(message, "Unable to Connect to Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidateServer()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(txtServerUrl.Text))
            {
                isValid = false;
                txtServerUrl.BackColor = m_InvalidEntryColor;
            }
            else
            {
                txtServerUrl.BackColor = SystemColors.Window;
            }

            if (string.IsNullOrEmpty(txtLocalUserName.Text))
            {
                isValid = false;
                txtLocalUserName.BackColor = m_InvalidEntryColor;
            }
            else
            {
                txtLocalUserName.BackColor = SystemColors.Window;
            }

            if (string.IsNullOrEmpty(txtLocalPassword.Text))
            {
                isValid = false;
                txtLocalPassword.BackColor = m_InvalidEntryColor;
            }
            else
            {
                txtLocalPassword.BackColor = SystemColors.Window;
            }

            btnLocalTest.Enabled = isValid;
            m_ServerIsValid = isValid;

            UpdateValidState();
        }

        private void ValidateHubServer()
        {
            bool isValid = true;

            if ((chkAnalyzeOnHub.Checked) && (string.IsNullOrEmpty(txtServerUrl.Text)))
            {
                isValid = false;
                txtServerUrl.BackColor = m_InvalidEntryColor;
            }
            else
            {
                txtServerUrl.BackColor = SystemColors.Window;
            }

            if (m_WorkingConfiguration.ServerCredentialsAvailable)
            {
                if ((chkAnalyzeOnHub.Checked) && (string.IsNullOrEmpty(txtHubUserName.Text)))
                {
                    isValid = false;
                    txtHubUserName.BackColor = m_InvalidEntryColor;
                }
                else
                {
                    txtHubUserName.BackColor = SystemColors.Window;
                }

                if ((chkAnalyzeOnHub.Checked) && (string.IsNullOrEmpty(txtHubPassword.Text)))
                {
                    isValid = false;
                    txtHubPassword.BackColor = m_InvalidEntryColor;
                }
                else
                {
                    txtHubPassword.BackColor = SystemColors.Window;
                }
            }

            btnHubTest.Enabled = isValid;
            m_HubIsValid = isValid;

            UpdateValidState();
        }

        private void ValidateMappings()
        {
            //see if we have anything selected at all.
            if (mappingsGrid.SelectedRows.Count == 0)
            {
                btnCopy.Enabled = false;
                btnRemoveRule.Enabled = false;
            }
            else
            {
                btnCopy.Enabled = true;
                btnRemoveRule.Enabled = true;
            }
        }

        private void UpdateValidState()
        {
            //OK is only enabled if all of our individual validities pass.
            btnOK.Enabled = m_ServerIsValid && m_HubIsValid;
        }

        private void UpdateAutomaticAnalysis()
        {
            if (m_Loading)
                return;


            if (m_WorkingHubConfig != null)
            {
                m_WorkingHubConfig.EnableAutomaticAnalysis = chkAnalyzeOnHub.Checked && chkEnableAutomaticAnalysis.Checked;
            }

            if (m_WorkingUserConfig != null)
            {
                m_WorkingUserConfig.EnableAutomaticAnalysis = !chkAnalyzeOnHub.Checked &&
                                                              chkEnableAutomaticAnalysis.Checked;
            }

            chkAnalyzeOnHub.Enabled = chkEnableAutomaticAnalysis.Checked;
        }

        #endregion

        #region Event Handlers

        private void txtServerUrl_TextChanged(object sender, EventArgs e)
        {
            if ((m_WorkingCommonConfig != null) && (m_Loading == false))
            {
                m_WorkingCommonConfig.Url = txtServerUrl.Text;

                ValidateServer();
            }
        }

        private void txtLocalUserName_TextChanged(object sender, EventArgs e)
        {
            if ((m_WorkingUserConfig != null) && (m_Loading == false))
            {
                //we don't leave the password in the text box, so we have to get it from the credential store.
                m_WorkingConfiguration.SetUserCredentials(RepositoryController.ServerCredentialsKey, txtLocalUserName.Text, txtLocalPassword.Text);

                ValidateServer();
            }
        }

        private void txtLocalPassword_TextChanged(object sender, EventArgs e)
        {
            if ((m_WorkingUserConfig != null) && (m_Loading == false))
            {
                if (string.IsNullOrEmpty(txtLocalPassword.Text) == false)
                {
                    m_WorkingConfiguration.SetUserCredentials(RepositoryController.ServerCredentialsKey, txtLocalUserName.Text, txtLocalPassword.Text);
                }

                ValidateServer();
            }
        }

        private void txtHubUserName_TextChanged(object sender, EventArgs e)
        {
            if ((m_WorkingHubConfig != null) && (m_Loading == false))
            {
                m_WorkingConfiguration.SetServerCredentials(RepositoryController.ServerCredentialsKey, txtHubUserName.Text, txtHubPassword.Text);

                ValidateHubServer();
            }
        }

        private void txtHubPassword_TextChanged(object sender, EventArgs e)
        {
            if ((m_WorkingHubConfig != null) && (m_Loading == false))
            {
                if (string.IsNullOrEmpty(txtHubPassword.Text) == false)
                {
                    m_WorkingConfiguration.SetServerCredentials(RepositoryController.ServerCredentialsKey, txtHubUserName.Text, txtHubPassword.Text);
                }

                ValidateHubServer();
            }
        }

        private void chkEnableAutomaticAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutomaticAnalysis();
        }

        private void chkAnalyzeOnHub_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAutomaticAnalysis();
        }

        private void btnHubTest_Click(object sender, EventArgs e)
        {
            ActionTestHubServer();
        }

        private void btnLocalTest_Click(object sender, EventArgs e)
        {
            ActionTestServer();
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            ActionAddMapping();
        }

        private void btnRemoveRule_Click(object sender, EventArgs e)
        {
            ActionRemoveMapping();
        }

        private void mappingsGrid_SelectionChanged(object sender, EventArgs e)
        {
            ValidateMappings();
        }

        private void mappingsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //we want to go into edit on the row that was double clicked
            DataGridViewRow selectedRow = mappingsGrid.Rows[e.RowIndex];

            ActionEditMapping(selectedRow.DataBoundItem as Mapping);
        }

        #endregion



    }
}
