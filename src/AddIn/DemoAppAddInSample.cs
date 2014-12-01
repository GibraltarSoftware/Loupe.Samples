using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.Test
{
    /// <summary>
    /// This session command will only apply to sessions for applications with the product name "Demo"
    /// </summary>
    public class DemoAppAddInSample: ISessionCommand
    {
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
        public void Initialize(IRepositoryAddInContext context)
        {
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
            controller.RegisterCommand("DoNothing", false, "Do Nothing at All", "This just means you have a session for the demo product");
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
                case "DoNothing":
                    Trace.WriteLine("Don't do anything at all", "Gibraltar.Add In");
                    break;
            }
        }
    }
}
