using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Loupe.Extensibility.Client;
using Loupe.Extensibility.Data;
using Loupe.Extensibility.Server;

namespace Loupe.Extension.FindByUser
{
    public class SessionAnalyzer : ISessionAnalyzer, ISessionCommand
    {
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
        /// Called by Loupe to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }

        /// <summary>
        /// This method is called for background session analysis
        /// </summary>
        void ISessionAnalyzer.Process(ISession session)
        {
            var config = _addInContext.Configuration.Common as FindByUserConfiguration;
            if (config == null || config.AutoScanSessions == false)
                return;

            ScanSession(session);
        }

        /// <summary>
        /// This method is called once for each session the user explicitly requests to be exported
        /// </summary>
        private void ScanSession(ISession session)
        {
            using (var scanner = new SessionScanner(session, _addInContext))
            {
                scanner.Scan();
            }
        }

        /// <summary>
        /// Register the context menu commands we support.
        /// </summary>
        public void RegisterCommands(IUserInterfaceContext controller)
        {
            controller.RegisterCommand("scan", false, "Scan session(s) for users", "Scan for users associated with each session", "scan",
                Properties.Resources.UserIcon);
        }


        /// <summary>
        /// Update command availability and text just prior to displaying a list of commands to the user.
        /// </summary>
        public void BeforeCommandsDisplay(IUserInterfaceContext controller, IList<ISessionSummary> sessionSummaries)
        {
            controller.UpdateCommand("scan", sessionSummaries.Count == 1 ? "Scan session for users" : "Scan sessions for users",
                "Scan for users associated with each session", true);
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
                _addInContext.Log.Verbose(ExtentionDefinition.LogCategory, "Processing " + commandName + " command", null);
                switch (commandName)
                {
                    case "scan":
                        ProcessScanCommand(sessionSummaries);
                        break;
                }
            }
            catch (Exception ex)
            {
                _addInContext.Log.Error(ex, ExtentionDefinition.LogCategory, "Unhandled Exception processing " + commandName + " command",
                    "Called with {0} sessions", sessionSummaries.Count);
                throw;
            }
        }

        /// <summary>
        /// Export one or more sessions
        /// </summary>
        private void ProcessScanCommand(IList<ISessionSummary> sessionSummaries)
        {
            var skipScan = false;
            if (sessionSummaries.Count == 1)
            {
                if (!sessionSummaries[0].HasData)
                {
                    if (_addInContext.IsUserInterfaceSupported)
                        MessageBox.Show("You must download this session before you can scan it.",
                            "Session Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    skipScan = true;
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
                        skipScan = true;
                }
            }

            if (!skipScan)
            {
                var missingSessions = 0;
                var localSession = 0;
                foreach (var summary in sessionSummaries)
                {
                    if (summary.HasData)
                        try
                        {
                            ScanSession(summary.Session());
                        }
                        catch (NotImplementedException ex)
                        {
                            localSession++;
                            missingSessions++;
                        }
                    else
                        missingSessions++;
                }

                if (localSession > 0)
                    _addInContext.Log.Warning(ExtentionDefinition.LogCategory, "AddIns are not compatible with local sessions",
                        "This will be corrected in a future version of Loupe Desktop.");

                var description = string.Format("{0} sessions scanned.", sessionSummaries.Count);
                if (missingSessions > 0)
                    description += string.Format("\n{0} were not available", missingSessions);
                _addInContext.Log.Information(ExtentionDefinition.LogCategory, "Find By User scanning complete", description);
            }
        }
    }
}
