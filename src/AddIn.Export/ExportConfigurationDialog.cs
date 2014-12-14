using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.Export
{
    /// <summary>
    /// Configuration Editor for the Session Export add-in
    /// </summary>
    public partial class ExportConfigurationDialog : Form, IConfigurationEditor
    {
        private const string LogCategory = "SessionExport.Config";
        private ExportAddInConfiguration _configuration;

        public ExportConfigurationDialog()
        {
            InitializeComponent();
            toolTipGenerator.SetToolTip(txtMetricsToExport,
                "Format:\n{Product.Application 1}\n    {Metric 1}\n    ...\n    {Metric m}\n\n ... repeat for multiple applications");
        }

        /// <summary>
        /// Called by Gibraltar to have the configuration editor display itself and edit the provided configuration
        /// </summary>
        public DialogResult EditConfiguration(IAddInContext context, IAddInConfiguration configuration, bool initialConfiguration)
        {
            context.Log.Verbose(LogCategory, "Begin editing Session Export config", null);

            _configuration = configuration.Machine as ExportAddInConfiguration ?? new ExportAddInConfiguration();

            DisplayConfiguration(context);

            DialogResult result = ShowDialog();
            if (result == DialogResult.OK)
            {
                //copy back our changes, but first make a clone to allow change logging
                var oldConfig = _configuration.Clone();

                _configuration.SessionExportPath = txtExportPath.Text;
                _configuration.Environment = NormalizeEnvironmentList(txtEnvironment.Text);
                _configuration.AutoExportSessions = chkEnableAutoExport.Checked;
                _configuration.UseUniqueFilenames = chkEnsureUniqueFilenames.Checked;
                _configuration.MetricsToExport = txtMetricsToExport.Text;
                _configuration.EnableLogMessageExport = chkEnableLogMessageExport.Checked;
                _configuration.IncludeSessionSummary = chkIncludeSessionSummary.Checked;
                _configuration.IncludeExceptionDetails = chkIncludeExceptionDetails.Checked;

                // Store selected format, but guard against parse errors
                var format = LogMessageFormat.Default; // Initialize in case TryParse fails
                if (!Enum.TryParse(cboMessageFormatter.Text, true, out format))
                    context.Log.Error(LogCategory, "Could not parse Log Message Formatter", "Selected value: {0}", cboMessageFormatter.Text);
                _configuration.LogMessageFormat = format;

                // Store selected severity, but guard against parse errors
                var severity = LogMessageSeverity.Verbose; // Initialize in case TryParse fails
                if (!Enum.TryParse(cboMinimumSeverity.Text, true, out severity))
                    context.Log.Error(LogCategory, "Could not parse Log Message Severity", "Selected value: {0}", cboMinimumSeverity.Text);
                _configuration.MinimumSeverity = severity;

                configuration.Machine = _configuration;

                LogConfigurationChanges(context, oldConfig);
            }
            else
            {
                context.Log.Verbose(LogCategory, "Canceling Session Export config dialog", null);
            }

            return result;
        }

        private string NormalizeEnvironmentList(string text)
        {
            var environments = text.Split(new[] {',', ';'}, StringSplitOptions.RemoveEmptyEntries);
            var environment = string.Empty;
            foreach (var env in environments)
            {
                if (environment.Length > 0)
                    environment += ", ";
                var trimmed = env.Trim();
                if (string.IsNullOrEmpty(trimmed))
                    continue;
                environment += trimmed;
            }
            return environment;
        }

        private void DisplayConfiguration(IAddInContext context)
        {
            txtExportPath.Text = _configuration.SessionExportPath;
            txtEnvironment.Text = _configuration.Environment;
            chkEnableAutoExport.Checked = _configuration.AutoExportSessions;
            chkEnsureUniqueFilenames.Checked = _configuration.UseUniqueFilenames;
            chkEnableLogMessageExport.Checked = _configuration.EnableLogMessageExport;
            chkIncludeSessionSummary.Checked = _configuration.IncludeSessionSummary;
            chkIncludeExceptionDetails.Checked = _configuration.IncludeExceptionDetails;
            AssignValueToCombobox(context, cboMessageFormatter, _configuration.LogMessageFormat.ToString());
            AssignValueToCombobox(context, cboMinimumSeverity, _configuration.MinimumSeverity.ToString());

            // Ensure that newlines are in the form the TextBox wants

            if (string.IsNullOrWhiteSpace(_configuration.MetricsToExport))
                ResetMetricConfiguration();
            else
                txtMetricsToExport.Text = _configuration.MetricsToExport.Replace("\n", Environment.NewLine);
        }

        private void AssignValueToCombobox(IAddInContext context, ComboBox comboBox, string text)
        {
            if (comboBox.Items.Contains(text))
                comboBox.Text = text;
            else
            {
                comboBox.SelectedIndex = 0;
                context.Log.Warning(LogCategory, "Cannot assign illegal value to combobox",
                    "\"{0}\" is not a legal value for {1}", text, comboBox.Name);
            }
        }

        private void LogConfigurationChanges(IAddInContext context, ExportAddInConfiguration oldConfig)
        {
            if (oldConfig.Equals(_configuration))
                context.Log.Verbose(LogCategory, "No change to Session Export config", null);
            else
            {
                var msg = new StringBuilder();

                if (_configuration.SessionExportPath != oldConfig.SessionExportPath)
                    msg.AppendFormat("SessionExportPath changed\nOLD: {0}\n|NEW: {1}\n\n",
                        oldConfig.SessionExportPath, _configuration.SessionExportPath);

                if (_configuration.Environment != oldConfig.Environment)
                    msg.AppendFormat("Environment changed\nOLD: {0}\nNEW: {1}\n\n",
                        oldConfig.Environment, _configuration.Environment);

                if (_configuration.AutoExportSessions != oldConfig.AutoExportSessions)
                    msg.AppendFormat("AutoExportSessions changed from {0} to {1}\n",
                        oldConfig.AutoExportSessions, _configuration.AutoExportSessions);

                if (_configuration.UseUniqueFilenames != oldConfig.UseUniqueFilenames)
                    msg.AppendFormat("UseUniqueFilenames changed from {0} to {1}\n",
                        oldConfig.UseUniqueFilenames, _configuration.UseUniqueFilenames);

                if (_configuration.EnableLogMessageExport != oldConfig.EnableLogMessageExport)
                    msg.AppendFormat("EnableLogMessageExport changed from {0} to {1}\n",
                        oldConfig.EnableLogMessageExport, _configuration.EnableLogMessageExport);

                if (_configuration.IncludeSessionSummary != oldConfig.IncludeSessionSummary)
                    msg.AppendFormat("IncludeSessionSummary changed from {0} to {1}\n",
                        oldConfig.IncludeSessionSummary, _configuration.IncludeSessionSummary);

                if (_configuration.IncludeExceptionDetails != oldConfig.IncludeExceptionDetails)
                    msg.AppendFormat("IncludeExceptionDetails changed from {0} to {1}\n",
                        oldConfig.IncludeExceptionDetails, _configuration.IncludeExceptionDetails);

                if (_configuration.LogMessageFormat != oldConfig.LogMessageFormat)
                    msg.AppendFormat("LogMessageFormat changed from {0} to {1}\n",
                        oldConfig.LogMessageFormat, _configuration.LogMessageFormat);

                if (_configuration.MinimumSeverity != oldConfig.MinimumSeverity)
                    msg.AppendFormat("MinimumSeverity changed from {0} to {1}\n",
                        oldConfig.MinimumSeverity, _configuration.MinimumSeverity);

                if (_configuration.MetricsToExport != oldConfig.MetricsToExport)
                {
                    var newLines = ExtractNonComments(_configuration.MetricsToExport);
                    var oldLines = ExtractNonComments(oldConfig.MetricsToExport);
                    if (newLines == oldLines)
                        msg.AppendFormat("MetricsToExport contains minor changes to comments only");
                    else
                        msg.AppendFormat("MetricsToExport changed\nOLD:\n{0}\n|NEW:\n{1}\n",
                            oldConfig.MetricsToExport, _configuration.MetricsToExport);
                }

                context.Log.Information(LogCategory, "Export configuration changed", msg.ToString());
            }
        }

        // We need this because...
        // The TextBox control does not support the CTRL+A shortcut key when the Multiline property value is true.
        // http://msdn.microsoft.com/en-us/library/system.windows.forms.textboxbase.shortcutsenabled.aspx
        private void txtMetricsToExport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        private string ExtractNonComments(string text)
        {
            if (text == null)
                text = string.Empty;

            var lines = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var builder = new StringBuilder();

            foreach (var line in lines.Where(line => !(line.Trim().StartsWith("//"))))
            {
                builder.AppendLine(line);
            }
            return builder.ToString();
        }

        private void btnDeleteComments_Click(object sender, EventArgs e)
        {
            var lines = txtMetricsToExport.Lines.Where(line =>
                !(line.Trim().StartsWith("//") || line.Trim().Length == 0)).ToList();
            txtMetricsToExport.Lines = lines.ToArray();
        }

        private void btnResetMetrics_Click(object sender, EventArgs e)
        {
            ResetMetricConfiguration();
        }

        private void ResetMetricConfiguration()
        {
            txtMetricsToExport.Text =
                "// Follow these steps to export metrics:\r\n" +
                "//   1. Click OK on this dialog\r\n" +
                "//   2. Restart Loupe Desktop if you've just enabled this add-in\r\n" +
                "//   3. Right-click on an exemplar log session\r\n" +
                "//   4. Select the menu option to Copy Metric Metadata...";

        }

        readonly FolderBrowserDialog _dlgPathBrowserDialog = new FolderBrowserDialog
        {
            Description = "Select the root path to hold exported log data.",
            ShowNewFolderButton = true
        };
        private void btnExportPathBrowse_Click(object sender, EventArgs e)
        {
            var result =_dlgPathBrowserDialog.ShowDialog();
            if (result == DialogResult.Cancel)
                return;
            txtExportPath.Text = _dlgPathBrowserDialog.SelectedPath.Trim();
        }
    }
}
