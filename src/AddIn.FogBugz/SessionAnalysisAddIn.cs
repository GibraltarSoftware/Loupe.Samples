using System;
using System.Collections.Generic;
using Gibraltar.AddIn.FogBugz.Internal;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.FogBugz
{
    /// <summary>
    /// Allows sessions to be scanned and errors reported to FogBugz
    /// </summary>
    public class SessionAnalysisAddIn : ISessionAnalyzer, ISessionCommand
    {
        private const string LogCategory = AddInController.LogCategory +  ".Session Analyzer";

        private bool m_Initialized;
        private bool m_IsDisposed;
        private IAddInContext m_Context;
        private AddInController m_Controller;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (m_Initialized)
                m_Initialized = false;

            if (m_IsDisposed)
                throw new InvalidOperationException("The add-in has already been disposed");

            m_IsDisposed = true;
        }

        /// <summary>
        /// Called to initialize the session analyzer.
        /// </summary>
        /// <param name="context">A standard interface to the hosting environment for the analyzer</param>
        /// <remarks>
        /// If any exception is thrown during this call this add in will not be loaded.
        ///             The analyzer can keep the context object for its entire lifespan
        /// </remarks>
        public void Initialize(IRepositoryAddInContext context)
        {
            if (m_Initialized)
                throw new InvalidOperationException("The add-in has already been initialized and shouldn't be re-initialized");
            
            m_Context = context;
            m_Controller = (AddInController)m_Context.Controller;

            //load up our baseline configuration
            ConfigurationChanged();

            m_Initialized = true;
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }

        /// <summary>
        /// Called to have the session command object register all commands that it supports.
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param>
        /// <remarks>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
        /// </remarks>
        public void RegisterCommands(IUserInterfaceContext controller)
        {
            controller.RegisterCommand("scan", false, "Analyze Session", "Reports errors to FogBugz");
        }

        /// <summary>
        /// Called just prior to displaying the list of commands to a user on a set of sessions.
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param><param name="sessionSummaries">Summaries of the selected sessions.</param>
        /// <remarks>
        /// <para>
        /// Use this method to change what commands are available or what their labels should be prior to being displayed.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
        /// </para>
        /// </remarks>
        public void BeforeCommandsDisplay(IUserInterfaceContext controller, IList<ISessionSummary> sessionSummaries)
        {
            controller.UpdateCommand("scan", "Analyze Session", "Reports errors to FogBugz", true);
        }

        /// <summary>
        /// Apply the specified command to the provided set of sessions
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param><param name="commandName">The command that was requested.</param><param name="sessionSummaries">Summaries of the selected sessions.</param>
        /// <remarks>
        /// <para>
        /// Headers of all of the currently selected sessions are provided.  If the command
        ///             was configured to provide its own user interface it will be called on the main UI thread and needs to perform
        ///             its own background processing to keep the user interface responsive.  If not, it will be called
        ///             from a background thread and the user interface will be kept responsive by the framework.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
        /// </para>
        /// </remarks>
        public void Process(IUserInterfaceContext controller, string commandName, IList<ISessionSummary> sessionSummaries)
        {
            switch (commandName)
            {
                case "scan":
                    foreach (ISessionSummary summary in sessionSummaries)
                    {
                        // Ignore sesssions that do not contain any errors.  Checking the header first saves the time to load the session data
                        if (summary.ErrorCount == 0)
                        {
                            Log.Verbose(LogCategory, "Skipping session because there are no errors to inspect", null);
                        }
                        else
                        {
                            ActionProcessSession(summary.Session());                            
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Analyze the provided session.
        /// </summary>
        /// <param name="session">The session to be analyzed</param>
        /// <remarks>
        /// <para>
        /// The FogBugz configuration file is read for each session so that
        /// changes to the file can be made without having to restart Gibraltar.
        /// </para>
        /// </remarks>
        public void Process(ISession session)
        {
            //if we aren't supposed to run on this tier, don't.
            if (m_Context.Environment == GibraltarEnvironment.HubServer)
            {
                if (m_Controller.HubConfiguration.EnableAutomaticAnalysis == false)
                {
                    Log.Verbose(LogCategory, "Skipping session because automatic session analysis not enabled on the Hub", null);
                }
            }
            else
            {
                if (m_Controller.UserConfiguration.EnableAutomaticAnalysis == false)
                {
                    Log.Verbose(LogCategory, "Skipping session because automatic session analysis is not enabled", null);
                }
            }

            // Ignore sesssions that do not contain any errors
            if (session.ErrorCount == 0)
            {
                Log.Verbose(LogCategory, "Skipping session because there are no errors to inspect", null);
                return;
            }

            // Only process this session if there is a matching target project in FogBugz
            Mapping target = m_Controller.FindTarget(session);
            if (target == null)
            {
                Log.Verbose(LogCategory, "Skipping session because it doesn't match any target in the configuration file", null);
                return; // this could be perfectly normal
            }

            // Throws an exception if we can't connect with FogBugz
            FBApi api;
            try
            {
                api = m_Controller.GetApi();
            }
            catch(Exception ex)
            {
                Log.Error(ex, LogCategory, "Unable to analyze session because FogBugz server is not available", "When connecting to the FogBugz API an exception was thrown.  We will assume it is not available.");
                throw; //we really do want to abort out to our caller who is designed to handle failures like this.
            }

            // Process each unique error (as defined in IssuesByClass)
            Dictionary<string, ErrorInfo> errorList = new Dictionary<string, ErrorInfo>();

            foreach (ILogMessage message in session.Messages)
            {
                //process errors
                if (message.Severity > LogMessageSeverity.Error)
                    continue;

                ErrorInfo currentError = new ErrorInfo(message);
                ErrorInfo previousError;

                // This error is either new or another instance of a previous error
                if (errorList.TryGetValue(currentError.Fingerprint, out previousError))
                    previousError.Messages.Add(message);
                else
                    errorList.Add(currentError.Fingerprint, currentError);
            }

            foreach (KeyValuePair<string, ErrorInfo> pair in errorList)
            {
                ErrorInfo error = pair.Value;
                FogBugzCaseWriter fogBugzCase = new FogBugzCaseWriter(Log, error);
                fogBugzCase.Submit(api, target);
            }
        }

        #region Private Properties and Methods

        /// <summary>
        /// Our log interface for recording diagnostic info
        /// </summary>
        private ILog Log { get { return m_Context.Log; } }

        /// <summary>
        /// Process the provided session
        /// </summary>
        /// <param name="session"></param>
        private void ActionProcessSession(ISession session)
        {
            // Ignore sesssions that do not contain any errors
            if (session.ErrorCount == 0)
            {
                Log.Verbose(LogCategory, "Skipping session because there are no errors to inspect", null);
                return;
            }

            // Only process this session if there is a matching target project in FogBugz
            Mapping target = m_Controller.FindTarget(session);
            if (target == null)
            {
                Log.Verbose(LogCategory, "Skipping session because it doesn't match any target in the configuration file", null);
                return; // this could be perfectly normal
            }

            // Throws an exception if we can't connect with FogBugz
            FBApi api;
            try
            {
                api = m_Controller.GetApi();
            }
            catch(Exception ex)
            {
                Log.Error(ex, LogCategory, "Unable to analyze session because FogBugz server is not available", "When connecting to the FogBugz API an exception was thrown.  We will assume it is not available.");
                throw; //we really do want to abort out to our caller who is designed to handle failures like this.
            }

            // Process each unique error (as defined in IssuesByClass)
            Dictionary<string, ErrorInfo> errorList = new Dictionary<string, ErrorInfo>();

            foreach (ILogMessage message in session.Messages)
            {
                //process errors
                if (message.Severity > LogMessageSeverity.Error)
                    continue;

                ErrorInfo currentError = new ErrorInfo(message);
                ErrorInfo previousError;

                // This error is either new or another instance of a previous error
                if (errorList.TryGetValue(currentError.Fingerprint, out previousError))
                    previousError.Messages.Add(message);
                else
                    errorList.Add(currentError.Fingerprint, currentError);
            }

            foreach (KeyValuePair<string, ErrorInfo> pair in errorList)
            {
                ErrorInfo error = pair.Value;
                FogBugzCaseWriter fogBugzCase = new FogBugzCaseWriter(Log, error);
                fogBugzCase.Submit(api, target);
            }            
        }

        #endregion
    }
}