using System;
using System.Collections.Generic;
using Gibraltar.AddIn.FogBugz.Internal;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.FogBugz
{
    /// <summary>
    /// This class implements a LogMessageCommand allowing a Gibraltar user to create an new FogBugz case
    /// based on the selected log messages.
    /// </summary>
    /// <remarks>
    /// <para>When multiple lines are selected, the last line is treated as the primary one (assuming the other
    /// lines provide context of the conditions leading up to an error.</para>
    /// <para>If the fingerprint of the primary message matches an existing case in FogBugz, that case will
    /// by updated.  Otherwise, a new case will be created.</para>
    /// <para>If updating an existing case, Project is read-only.  Otherwise, it may be selected.  If the 
    /// current session has an affinity for a particular FogBugz project based on the add-in configuration,
    /// that project will be pre-selected.  Otherwise, the user must select a project.</para>
    /// </remarks>
    public class AddDefectCommand : ILogMessageCommand
    {
        private const string AddDefectCommandName = "addDefect";

        private IAddInContext m_Context;
        private AddInController m_Controller;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }

        /// <summary>
        /// Called to initialize the add in.
        /// </summary>
        /// <remarks>
        /// If any exception is thrown during this call the add-in will not be loaded.
        /// </remarks>
        public void Initialize(ISessionAddInContext context)
        {
            m_Context = context;
            m_Controller = (AddInController)m_Context.Controller;

            //load up our baseline configuration
            ConfigurationChanged();
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }

        /// <summary>
        /// Called to have the log message command object register all commands that it supports.
        /// </summary>
        public void RegisterCommands(IUserInterfaceContext controller)
        {
            controller.RegisterCommand(AddDefectCommandName, true, "Create New Defect...", "Use this message to create a new defect");
        }

        /// <summary>
        /// Called just prior to displaying the list of commands to a user on a set of messages.
        /// </summary>
        /// <param name="controller"/><param name="messages">The selected messages.</param>
        /// <remarks>
        /// <para>
        /// Use this method to change what commands are available or what their labels should be prior to being displayed.
        /// </para>
        /// <para>
        /// If exactly one session is currently selected it will be provided so you can customize commands and labels.
        /// For performance reasons, if multiple sessions are selected they aren't provided (since some operations could 
        /// conceivably affect hundreds or thousands of sessions)
        /// </para>
        /// </remarks>
        public void BeforeCommandsDisplay(IUserInterfaceContext controller, IList<ILogMessage> messages)
        {
            string toolTip = (messages.Count > 1)
                ? "Use these messages to create a new defect"
                : "Use this message to create a new defect";
            controller.UpdateCommand(AddDefectCommandName, "Create New Defect...", toolTip, true);
        }

        /// <summary>
        /// Apply the specified command to the provided set of sessions
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param>
        /// <param name="commandName">The command that was requested.</param>
        /// <param name="messages">The selected messages to be processed.</param>
        /// <remarks>
        /// <para>
        /// All of the currently selected messages are provided. The set of messages will always contain
        /// at least one value and be from the same session. If the command was configured to provide its
        /// own user interface it will be called on the main UI thread and needs to perform its own
        /// background processing to keep the user interface responsive.  If not, it will be called
        /// from a background thread and the user interface will be kept responsive by the framework.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        /// the same object may get calls from multiple controllers.
        /// </para>
        /// </remarks>
        public void Process(IUserInterfaceContext controller, string commandName, IList<ILogMessage> messages)

        {
            switch (commandName)
            {
                case AddDefectCommandName:
                    ActionAddDefect(controller, messages);
                    break;
            }
        }

        #region Private Properties and Methods

        /// <summary>
        /// Create a new defect in the defect tracking system with the provided set messages
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="messages"></param>
        private void ActionAddDefect(IUserInterfaceContext controller, IList<ILogMessage> messages)
        {
            using(AddDefectDialog dialog = new AddDefectDialog())
            {
                FBApi api = m_Controller.GetApi();
                dialog.AddDefect(m_Context, messages, m_Controller, api);
            }
        }

        #endregion
    }
}
