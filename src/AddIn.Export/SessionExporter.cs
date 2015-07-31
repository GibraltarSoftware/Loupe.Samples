using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Loupe.Extensibility.Client;
using Loupe.Extensibility.Data;
using Loupe.Extensibility.Server;

namespace Loupe.Extension.Export
{
    public class SessionExporter : ISessionAnalyzer, ISessionCommand
    {
        public const string ExportLogMessagesSpecifier = "Log Messages";
        private const string LogCategory = "SessionExport.AddIn";

        private IRepositoryContext _addInContext;

        private bool _initialized;
        private bool _isDisposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (_initialized)
                _initialized = false;

            if (_isDisposed)
                throw new InvalidOperationException("The add-in has already been disposed");

            _addInContext = null;
            _isDisposed = true;
        }

        /// <summary>
        /// Called to initialize the add in.
        /// </summary>
        /// <remarks>
        /// If any exception is thrown during this call the Add In will not be loaded.
        /// </remarks>
        public void Initialize(IRepositoryContext context)
        {
            if (_initialized)
                throw new InvalidOperationException("The add-in has already been initialized and shouldn't be re-initialized");

            _initialized = true;
            _addInContext = context;
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }

        /// <summary>
        /// This method is called for background session analysis
        /// </summary>
        void ISessionAnalyzer.Process(ISession session)
        {
            var exportDefintions = new SessionExportDefinition(_addInContext);
            if (!(exportDefintions.Enabled && exportDefintions.Config.AutoExportSessions))
                return;

            exportDefintions.Process(session);
        }

        /// <summary>
        /// This method is called once for each session the user explicitly requests to be exported
        /// </summary>
        private void ExportSession(ISession session)
        {
            var exportDefintions = new SessionExportDefinition(_addInContext);
            if (!exportDefintions.Enabled)
                return;

            exportDefintions.Process(session);
        }

        /// <summary>
        /// Register the context menu commands we support.
        /// </summary>
        public void RegisterCommands(IUserInterfaceContext controller)
        {
            controller.RegisterCommand("export", false, "Export Session(s)", "Export session data to text files in configured export folder","export", null);
            controller.RegisterCommand("edit", true, "Configure Export based on this Session", "Copy the list of metrics in this session to the clipboard","export", null);
            controller.RegisterCommand("view", false, "View Exported Data", "Open the folder containing exported session data", "export", null);
        }


        /// <summary>
        /// Update command availability and text just prior to displaying a list of commands to the user.
        /// </summary>
        public void BeforeCommandsDisplay(IUserInterfaceContext controller, IList<ISessionSummary> sessionSummaries)
        {
            controller.UpdateCommand("export", sessionSummaries.Count == 1 ? "Export Session" : "Export Sessions",
                "Export session data to text files in configured export folder", true);
            controller.UpdateCommand("edit", sessionSummaries.Count == 1);
        }


        /// <summary>
        /// Apply the specified command to the provided set of sessions
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param><param name="commandName">The command that was requested.</param><param name="sessionSummaries">Summaries of the selected sessions.</param>
        /// <remarks>
        /// <para>
        /// Headers of all of the currently selected sessions are provided.  If the command
        /// was configured to provide its own user interface it will be called on the main UI thread and needs to perform
        /// its own background processing to keep the user interface responsive.  If not, it will be called
        /// from a background thread and the user interface will be kept responsive by the framework.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        /// the same object may get calls from multiple controllers.
        /// </para>
        /// </remarks>
        public void Process(IUserInterfaceContext controller, string commandName, IList<ISessionSummary> sessionSummaries)
        {
            try
            {
                _addInContext.Log.Verbose(LogCategory, "Processing " + commandName + " command", null);
                switch (commandName)
                {
                    case "view":
                        if (sessionSummaries.Count > 0)
                            ProcessViewCommand(sessionSummaries[0]);
                        break;

                    case "export":
                        ProcessExportCommand(sessionSummaries);
                        break;

                    case "edit":
                        ProcessEditCommand(sessionSummaries);
                        break;
                }

            }
            catch (Exception ex)
            {
                _addInContext.Log.Error(ex, LogCategory, "Unhandled Exception processing " + commandName + " command",
                    "Called with {0} sessions", sessionSummaries.Count);
                throw;
            }
        }

        /// <summary>
        /// Display the contents of the folder configured for session exports
        /// </summary>
        /// <param name="sessionSummary"></param>
        private void ProcessViewCommand(ISessionSummary sessionSummary)
        {
            var configuration = _addInContext.Configuration.Common as ExportAddInConfiguration;
            if (configuration == null
                || string.IsNullOrEmpty(configuration.SessionExportPath)
                || !Directory.Exists(configuration.SessionExportPath))
            {
                MessageBox.Show("Session Export is not configured with a valid base path. "
                                + "You must first configure the Export Sessions add-in.",
                    "Session Export not Configured", MessageBoxButtons.OK, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000); // MB_TOPMOST
            }
            else
            {
                var applicationPath = StreamCreator.GetApplicationPath(configuration, sessionSummary);
                System.Diagnostics.Process.Start(applicationPath);
            }
        }

        /// <summary>
        /// Export one or more sessions
        /// </summary>
        private void ProcessExportCommand(IList<ISessionSummary> sessionSummaries)
        {
            var skipExport = false;
            if (sessionSummaries.Count == 1)
            {
                if (!sessionSummaries[0].HasData)
                {
                    if (_addInContext.IsUserInterfaceSupported)
                        MessageBox.Show("You must download this session before you can export it.",
                            "Session Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    skipExport = true;
                }
            }
            else
            {
                var missingSessionCount = sessionSummaries.Count(summary => !summary.HasData);
                if (missingSessionCount > 0 && _addInContext.IsUserInterfaceSupported)
                {
                    var msg = string.Format(
                        "{0} of the {1} sessions you selected are not available. "
                        + "This is typically because they have not yet been downloaded.\r\n\r\n"
                        + "Continue anyway?", missingSessionCount, sessionSummaries.Count);

                    var result = MessageBox.Show(msg, "Sessions Not Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000); // MB_TOPMOST
                    if (result == DialogResult.No)
                        skipExport = true;
                }
            }

            if (!skipExport)
            {
                var missingSessions = 0;
                foreach (var summary in sessionSummaries)
                {
                    if (summary.HasData)
                        ExportSession(summary.Session());
                    else
                        missingSessions++;
                }

                var description = string.Format("{0} sessions processed.", sessionSummaries.Count);
                if (missingSessions > 0)
                    description += string.Format("\n{0} were not available", missingSessions);
                _addInContext.Log.Information(LogCategory, "Session Export complete", description);
            }
        }

        /// <summary>
        /// Configure Session Export
        /// </summary>
        private void ProcessEditCommand(IList<ISessionSummary> sessionSummaries)
        {
            if (sessionSummaries.Count > 0)
                if (sessionSummaries[0].HasData)
                    ExtractMetadata(sessionSummaries[0].Session());
                else if (_addInContext.IsUserInterfaceSupported)
                    MessageBox.Show("You must download this session before you can copy metadata from it.",
                        "Session Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000); // MB_TOPMOST
        }

        /// <summary>
        /// Extract metadata from a session and format it as session export config specifications
        /// </summary>
        private void ExtractMetadata(ISession session)
        {
            var metrics = session.MetricDefinitions.Select(md => md.FullMetricName()).ToList();
            metrics.Sort();
            var s = new StringBuilder();

            const string indent = "    "; // consistent spacing for export specifications
            s.AppendLine("// This is the application whose logs you will export:");
            s.AppendLine(session.Summary.FullApplicationName());
            s.AppendLine();
            s.AppendLine("// If you wish to export log messages, uncomment one of lines below:");
            s.AppendLine("// " + indent + ExportLogMessagesSpecifier + " Summary");
            s.AppendLine("// " + indent + ExportLogMessagesSpecifier + " Default");
            s.AppendLine("// " + indent + ExportLogMessagesSpecifier + " Detailed");
            s.AppendLine();
            s.AppendLine("// Uncomment lines below to enable the metrics you wish to export");
            foreach (var metric in metrics)
                s.AppendLine("// " + indent + metric);
            s.AppendLine(); // Add an extra line to space out specifications for multiple applications
            s.AppendLine("// TIP: To keep you config uncluttered, delete unneeded comment lines");
            s.AppendLine("//         ...like this one, for example  :-)");

            // Because we are on a non-STA background thread, we need to create 
            // an STA thread to actually post our string to the clipboard.
            // http://www.codeproject.com/Articles/2207/Clipboard-handling-with-NET?msg=2572888#xx2572888xx
            var t = new Thread(() => Clipboard.SetText(s.ToString()));
            t.SetApartmentState(ApartmentState.STA); // t.ApartmentState is deprecated
            t.Start();
            t.Join(); // so the application wouldn't stop

            MessageBox.Show(
                "To configure Session Export, use Ctrl-V to paste metadata into the textbox labeled Data to be Exported. "
                + "Then delete extraneous lines leaving only what you want to export.", "How to Configure Export",
                MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1,
                (MessageBoxOptions)0x40000); // MB_TOPMOST

            _addInContext.EditConfiguration();
        }
    }
}
