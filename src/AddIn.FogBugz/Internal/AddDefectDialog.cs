using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Loupe.Extensibility.Client;
using Loupe.Extensibility.Data;

namespace Loupe.Extension.FogBugz.Internal
{
    /// <summary>
    /// Dialog to enter a new FogBugz case from a selected error.
    /// </summary>
    internal partial class AddDefectDialog : Form
    {
        private const string LogCategory = "AddIn.FogBugz.Add Defect Dialog";
        private readonly Font m_DescriptionFogBugzFont;
        private Dictionary<string, List<String>> m_ProjectsAndAreas = new Dictionary<string, List<string>>();

        private string m_Fingerprint;
        private string m_SessionIdMessage;
        private RepositoryController m_Controller;

        private bool m_UseFogBugzFonts;
        private IList<ILogMessage> m_LogMessages;
        private ILogMessage m_PrimaryMessage;
        private IRepositoryContext m_Context;

        private delegate void ThreadSafeDisplayDefectInvoker(string caseId, string projectName, string areaName);

        public AddDefectDialog()
        {
            InitializeComponent();

            m_DescriptionFogBugzFont = new Font("Verdana", 11.25f);
            SetUseFogBugzFonts(true);
        }

        #region Public Properties and Methods

        /// <summary>
        /// Process a user's Add Defect request
        /// </summary>
        public DialogResult AddDefect(IRepositoryContext context, IList<ILogMessage> messages, RepositoryController controller, FBApi api)
        {
            // It shouldn't happen that we get called with no messages selected,
            // but check for it anyway.
            if (messages.Count <= 0)
                return DialogResult.Cancel;

            UseWaitCursor = true;

            //store off our initial state
            m_Context = context;
            m_Controller = controller;
            m_LogMessages = messages;
            int lastMessageIndex = messages.Count - 1;
            m_PrimaryMessage = messages[lastMessageIndex];
            ErrorInfo errorInfo = new ErrorInfo(m_PrimaryMessage);
            m_Fingerprint = errorInfo.Fingerprint;

            //Blank our display
            ClearDisplay();

            //now go to the background to initialize it
            ThreadPool.QueueUserWorkItem(AsyncInitializeData, api);

            DialogResult result = ShowDialog();
            if (result == DialogResult.OK)
            {
                Submit(api);
            }
            else
            {
                m_Context.Log.Verbose(LogCategory, "User canceled dialog, no changes will be submitted", null);
            }

            return DialogResult;
        }

        public bool UseFogBugzFonts
        {
            get
            {
                return m_UseFogBugzFonts;
            }
            set
            {
                SetUseFogBugzFonts(value);
            }
        }

        #endregion

        #region Private Properties and Methods

        /// <summary>
        /// Load the FB data asynchronously
        /// </summary>
        /// <param name="state"></param>
        private void AsyncInitializeData(object state)
        {
            try //we're on the thread pool so if we don't catch the exception, it's terminal to the application
            {
                //unbundle the state
                FBApi api = (FBApi)state;

                //make our FB calls
                m_ProjectsAndAreas = api.ListProjectsAndAreas();
                string caseId = GetCaseId(api);

                // If a case already exists, get info about it from FogBugz
                string projectName = null, areaName = null;
                if (string.IsNullOrEmpty(caseId) == false)
                {
                    Dictionary<string, string> queryArgs = new Dictionary<string, string>
                                                               {
                                                                   {"q", caseId},
                                                                   {"cols", "sProject,sArea,sStatus"}
                                                               };

                    string queryResults = api.Cmd("search", queryArgs);
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(queryResults);
                    XmlNode projectNode = xml.SelectSingleNode("/response/cases/case/sProject");
                    projectName = projectNode.InnerText;

                    XmlNode areaNode = xml.SelectSingleNode("/response/cases/case/sArea");
                    areaName = areaNode.InnerText;
                }

                ThreadSafeDisplayDefect(caseId, projectName, areaName);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Add or update a FogBugz case
        /// </summary>
        private void Submit(FBApi api)
        {
            // Whether we're adding or updating a case, we'll want to update the
            // title and add an event description that includes our session id.
            // Appending session id ensures that this session is associated
            // with this case.  We append it here after the user is done
            // editing to prevent the user from mangling our guid which would
            // break our ability to do session lookups.
            string text = string.Format("{0}\r\n{1}\r\n",
                                     DescriptionTextBox.Text,
                                     m_SessionIdMessage);

            Dictionary<string, string> args = new Dictionary<string, string>
                            {
                                {"sTitle", TitleTextBox.Text},
                                {"sEvent", text}
                            };

            m_Context.Log.Information(LogCategory, "User is submitting new / updated case", "Title: {0}\r\n\r\nEvent: {1}", TitleTextBox.Text, text);

            string results;

            // Create or update a case depending on whether we have a case id
            if (CaseLabel.Tag == null)
            {
                // We only provide project, tags and fingerprint when creating a new case
                args.Add("sProject", ProjectSelection.Text);
                args.Add("sTags", "Gibraltar");
                args.Add("sScoutDescription", m_Fingerprint);
                results = api.Cmd("new", args);
            }
            else
            {
                // When updating an existing case, we must provide the case id
                args.Add("ixBug", CaseLabel.Text);
                results = api.Cmd("edit", args);
            }

            m_Context.Log.Verbose(LogCategory, "Case submission completed", "Result from server:\r\n{0}", results);
        }

        /// <summary>
        /// Clean up the display for initial use.
        /// </summary>
        private void ClearDisplay()
        {
            CaseLabel.Text = string.Empty;
            TitleTextBox.Text = string.Empty;
            TitleTextBox.Enabled = false;
            ProjectSelection.Enabled = false;
            AreaSelection.Enabled = false;
            DescriptionTextBox.Text = string.Empty;
            DescriptionTextBox.Enabled = false;

            ValidateData();
        }

        private void ThreadSafeDisplayDefect(string caseId, string projectName, string areaName)
        {
            if (InvokeRequired)
            {
                ThreadSafeDisplayDefectInvoker invoker = ThreadSafeDisplayDefect;
                Invoke(invoker, new object[] { caseId, projectName, areaName });
            }
            else
            {
                try
                {
                    //now add these items to the project selection area.
                    ProjectSelection.Items.Clear();
                    ProjectSelection.DataSource = new List<string>(m_ProjectsAndAreas.Keys); //data source requires a list

                    int lastMessageIndex = m_LogMessages.Count - 1;
                    StringBuilder builder = new StringBuilder();

                    // Use compact formatting of all but the last message
                    for (int i = 0; i < lastMessageIndex; i++)
                    {
                        LogMessageFormatter preludeFormatter = new LogMessageFormatter(m_LogMessages[i]);
                        builder.Append(preludeFormatter.GetLogMessageSummary());
                    }

                    // Skip a line between last "prelude" message and the primary message
                    if (lastMessageIndex > 0)
                        builder.AppendLine();

                    // The primary message is the last of the selected messages
                    LogMessageFormatter formatter = new LogMessageFormatter(m_PrimaryMessage);
                    builder.Append(formatter.GetLogMessageDetails());

                    // Add session details after the primary message
                    builder.AppendLine();
                    builder.AppendFormat("Session Details:\r\n");
                    LogMessageFormatter.AppendDivider(builder);
                    builder.Append(formatter.GetSessionDetails());

                    // This will be applied later, after user editing is done
                    m_SessionIdMessage = formatter.GetSessionIdMessage();

                    // Check the fingerprint of the primary message to determine if
                    // a case for this error already exists in FogBugz            
                    if (string.IsNullOrEmpty(caseId))
                    {
                        // If there is no existing case, check if this session has an
                        // affinity for a particular project (i.e. is there a 
                        // MappingTarget associated with this MappingSource in the config?)
                        // If so, default the project selection accordingly.
                        Mapping target = m_Controller.FindTarget(m_PrimaryMessage.Session);
                        m_Context.Log.Verbose(LogCategory, "No existing case found, user will be able to select project", "The current mapping information for the message is:\r\n{0}", (target == null) ? "(No mapping found)" : target.Project);
                        ProjectSelection.SelectedItem = target == null ? null : target.Project;
                        ProjectSelection.Enabled = true;
                        AreaSelection.Enabled = true;
                        CaseLabel.Tag = null;
                        CaseLabel.Text = "(New Case)";
                        Text = "Create New Case";
                    }
                    else
                    {
                        // If a case already exists, get info about it from FogBugz
                        ProjectSelection.SelectedItem = projectName;
                        ProjectSelection.Enabled = false;

                        AreaSelection.SelectedItem = areaName;
                        AreaSelection.Enabled = false;
                        m_Context.Log.Verbose(LogCategory, "Existing case fond, user will not be able to change project / area", "The current case is in:\r\n {0} {1}", projectName, areaName);

                        CaseLabel.Tag = caseId;
                        CaseLabel.Text = caseId;
                        Text = "Edit Case " + caseId;
                    }

                    TitleTextBox.Text = formatter.GetTitle();
                    TitleTextBox.SelectionStart = 0;
                    TitleTextBox.SelectionLength = 0;
                    TitleTextBox.Enabled = true;

                    DescriptionTextBox.Text = builder.ToString();
                    DescriptionTextBox.SelectionStart = 0;
                    DescriptionTextBox.SelectionLength = 0;
                    DescriptionTextBox.Enabled = true;

                    DescriptionTextBox.Select();
                    DescriptionTextBox.Focus(); //this is what they should edit.

                    ValidateData();
                }
                finally
                {
                    //one way or the other, we're done so the user shouldn't be given the impression we're waiting around.
                    UseWaitCursor = false;
                }
            }
        }

        private void ValidateData()
        {
            bool isValid = true;

            if (ProjectSelection.SelectedItem == null)
            {
                isValid = false;
            }

            if (AreaSelection.SelectedItem == null)
            {
                isValid = false;
            }

            btnOk.Enabled = isValid;
        }

        private void SetUseFogBugzFonts(bool value)
        {
            if (value == m_UseFogBugzFonts)
                return;

            m_UseFogBugzFonts = value;

            DescriptionTextBox.Font = m_UseFogBugzFonts ? m_DescriptionFogBugzFont : SystemFonts.MessageBoxFont;

            //make sure the UI is in sync so users aren't confused.
            if (chkUseFogBugzFonts.Checked != value)
            {
                chkUseFogBugzFonts.Checked = value;
            }
        }

        private string GetCaseId(FBApi api)
        {
            Dictionary<string, string> args = new Dictionary<string, string>
                           {
                               {"sScoutDescription", m_Fingerprint},

                           };
            string results = api.Cmd("listScoutCase", args);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(results);

            XmlNode caseNode = xml.SelectSingleNode("/response/case/ixBug");

            string caseId = caseNode != null ? caseNode.InnerText : null;
            m_Context.Log.Verbose(LogCategory, "Completed Case Id Lookup", "Looking for fingerprint: {0}\r\nParsed Id: {1}\r\n\r\nRaw Return:\r\n{2}", m_Fingerprint, caseId ?? "(null)", results);
            return caseId;
        }

        #endregion

        #region Event Handlers

        private void ProjectSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            //update our list of areas....
            List<string> areas;
            if ((ProjectSelection.SelectedValue != null) && (m_ProjectsAndAreas.TryGetValue((string)ProjectSelection.SelectedValue, out areas)))
            {
                AreaSelection.DataSource = areas;
                AreaSelection.Enabled = ProjectSelection.Enabled;
            }
            else
            {
                AreaSelection.DataSource = null;
                AreaSelection.Enabled = false;
            }

            ValidateData();
        }

        private void AreaSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void CaseLabel_Click(object sender, EventArgs e)
        {
            if(CaseLabel.Tag != null)
            {
                m_Controller.WebSiteOpen((string)CaseLabel.Tag);
            }
        }

        private void chkUseFogBugzFonts_CheckedChanged(object sender, EventArgs e)
        {
            SetUseFogBugzFonts(chkUseFogBugzFonts.Checked);
        }

        #endregion


    }
}