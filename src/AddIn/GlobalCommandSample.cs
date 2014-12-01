using System;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;


namespace Gibraltar.AddIn.Test
{
    public class GlobalCommandSample : IAddInCommand
    {
        private IAddInContext m_AddInContext;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }

        /// <summary>
        /// Called to initialize the command.
        /// </summary>
        /// <param name="context">A standard interface to the hosting environment for the command</param>
        /// <remarks>
        /// If any exception is thrown during this call this command will not be loaded.
        /// </remarks>
        public void Initialize(IAddInContext context)
        {
            m_AddInContext = context;
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }

        /// <summary>
        /// Called to have the command object register all commands that it supports.
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param>
        /// <remarks>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
        /// </remarks>
        public void RegisterCommands(IUserInterfaceContext controller)
        {
            controller.RegisterCommand("sample1", true, "Grouped Command 1", "Our first global command", "firstgroup", null);
            controller.RegisterCommand("sample2", false, "Second Global", "Our second global command");
            controller.RegisterCommand("sample3", true, "Third Command", "Our third global command");
            controller.RegisterCommand("sample4", true, "Grouped Command 2", "Our second global command", "firstgroup", null);
            controller.RegisterCommand("configure", true, "Configure", "Show the configuration dialog for our add in");
            controller.RegisterCommand("sample6", true, "Group 2 Command 1", "Our second global command", "secondgroup", null);
            controller.RegisterCommand("sample7", true, "Group 2 Command 2", "Our second global command", "secondgroup", null);
            controller.RegisterCommand("sample8", true, "Grouped Command 3", "Our second global command", "firstgroup", null);
            controller.RegisterCommand("sample9", true, "Group 3 Command 1", "Our second global command", "thirdgroup", null);
        }

        /// <summary>
        /// Called just prior to displaying the list of commands to a user.
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param>
        /// <remarks>
        /// <para>
        /// Use this method to change what commands are available or what their labels should be prior to being displayed.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
        /// </para>
        /// </remarks>
        public void BeforeCommandsDisplay(IUserInterfaceContext controller)
        {
            //suppress everything in the middle group
            controller.UpdateCommand("sample6", false);
            controller.UpdateCommand("sample7", false);
        }

        /// <summary>
        /// Execute the specified command 
        /// </summary>
        /// <param name="controller">The User Interface Controller for the current process</param><param name="commandName">The command that was requested.</param>
        /// <remarks>
        /// <para>
        /// If the command
        ///             was configured to provide its own user interface it will be called on the main UI thread and needs to perform
        ///             its own background processing to keep the user interface responsive.  If not, it will be called
        ///             from a background thread and the user interface will be kept responsive by the framework.
        /// </para>
        /// <para>
        /// The controller should not be persisted or accessed between calls, it may change and 
        ///             the same object may get calls from multiple controllers.
        /// </para>
        /// </remarks>
        public void Process(IUserInterfaceContext controller, string commandName)
        {
            switch (commandName)
            {
                case "configure":
                    m_AddInContext.EditConfiguration();
                    break;
            }
        }
    }
}
