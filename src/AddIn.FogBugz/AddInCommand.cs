using System;
using Gibraltar.Analyst.AddIn;

namespace Gibraltar.AddIn.FogBugz
{
    /// <summary>
    /// Provides global commands
    /// </summary>
    public class AddInCommand : IAddInCommand
    {
        private const string ConfigureCommand = "Configure";
        private const string OpenSiteCommand = "GoToWeb";

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
        /// Called to initialize the command.
        /// </summary>
        /// <param name="context">A standard interface to the hosting environment for the command</param>
        /// <remarks>
        /// If any exception is thrown during this call this command will not be loaded.
        /// </remarks>
        public void Initialize(IAddInContext context)
        {
            m_Context = context;
            m_Controller = (AddInController)m_Context.Controller;
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
            controller.RegisterCommand(OpenSiteCommand, true, "Open FogBugz", "Display your FogBugz site in a new web browser", "view", null);
            controller.RegisterCommand(ConfigureCommand, true, "Configure Integration...", "View and edit the FogBugz integration configuration", "config", null);
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
            //make sure we have a server to connect to...
            if (string.IsNullOrEmpty(m_Controller.CommonConfiguration.Url))
            {
                controller.UpdateCommand(OpenSiteCommand, false);
            }
            else
            {
                controller.UpdateCommand(OpenSiteCommand, true);               
            }
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
                case ConfigureCommand:
                    m_Context.EditConfiguration();
                    break;
                case OpenSiteCommand:
                    m_Controller.WebSiteOpen(null);
                    break;
            }
        }
    }
}
