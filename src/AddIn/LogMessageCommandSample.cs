using System.Collections.Generic;
using Loupe.Extensibility.Client;
using Loupe.Extensibility.Data;

namespace Loupe.Extension.Sample
{
    public class LogMessageCommandSample : ILogMessageCommand
    {
        private const string AddDefectCommandName = "addDefect";

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
        /// If any exception is thrown during this call the Add In will not be loaded.
        /// </remarks>
        public void Initialize(ISessionContext context)
        {
        }

        /// <summary>
        /// Called by Loupe to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }

        /// <summary>
        /// Called to have the log message command object register all commands that it supports.
        /// </summary>
        /// <param name="controller"/>
        public void RegisterCommands(IUserInterfaceContext controller)
        {
            controller.RegisterCommand(AddDefectCommandName, true, "Create New Defect...", "Use this message to create a new defect");
            controller.RegisterCommand("sample1", true, "Grouped Command 1", "Our first log message command", "firstgroup", null);
            controller.RegisterCommand("sample2", false, "Second Global", "Our second log message command");
            controller.RegisterCommand("sample3", true, "Third Command", "Our third log message command");
            controller.RegisterCommand("sample4", true, "Grouped Command 2", "Our second log message command", "firstgroup", null);
            controller.RegisterCommand("sample5", false, "Configure", "Show the configuration dialog for our add in");
            controller.RegisterCommand("sample6", true, "Group 2 Command 1", "Our second log message command", "secondgroup", null);
            controller.RegisterCommand("sample7", true, "Group 2 Command 2", "Our second log message command", "secondgroup", null);
            controller.RegisterCommand("sample8", true, "Grouped Command 3", "Our second log message command", "firstgroup", null);
            controller.RegisterCommand("sample9", true, "Group 3 Command 1", "Our second log message command", "thirdgroup", null);

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
        ///             For performance reasons, if multiple sessions are selected they aren't provided (since some operations could 
        ///             conceivably affect hundreds or thousands of sessions)
        /// </para>
        /// </remarks>
        public void BeforeCommandsDisplay(IUserInterfaceContext controller, IList<ILogMessage> messages)
        {
            string toolTip = (messages.Count > 1) ? "Use these messages to create a new defect" : "Use this message to create a new defect";
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
        /// All of the currently selected messages are provided. The set of messages will always contain at least one value and be from the same session.
        ///             If the command was configured to provide its own user interface it will be called on the main UI thread and needs to perform
        ///             its own background processing to keep the user interface responsive.  If not, it will be called
        ///             from a background thread and the user interface will be kept responsive by the framework.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
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
        private static void ActionAddDefect(IUserInterfaceContext controller, IList<ILogMessage> messages)
        {
            using(AddDefectDialog dialog = new AddDefectDialog())
            {
                dialog.AddDefect(messages);
            }
        }

        #endregion
    }
}
