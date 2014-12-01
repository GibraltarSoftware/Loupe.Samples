using Gibraltar.Analyst.AddIn;

namespace Gibraltar.AddIn.Test
{
    [GibraltarAddIn("Sample Add In", Description = "This is a sample addin used to demonstrate basic addin development", 
        ConfigurationEditor = typeof(SampleConfigurationDialog),
        MachineConfiguration = typeof(SampleAddInConfiguration))]
    public class SampleAddIn: IAddInController
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
        /// <param name="context">A standard interface to the hosting environment for the add in, provided to all the different extensions that get loaded.  The controller
        ///             can keep this object for its entire lifecycle.</param><param name="controllerContext">Additional methods available to the repository controller during initialization.  This object should not be held between calls.</param>
        /// <remarks>
        /// <para>
        /// If any exception is thrown during this call this add in will not be loaded.
        /// </para>
        /// <para>
        /// During initialization the controller should register each of the other extension types that should be available to end users through appropriate calls to
        ///             the controllerContext.  These objects will be created and initialized as requred and provided the same IAddInContext object instance provided to this
        ///             method to enable coordination between all of the components.
        /// </para>
        /// </remarks>
        public void Initialize(IAddInContext context, IAddInControllerContext controllerContext)
        {
            //we have to register all of our types during this call or they won't be used at all.
            controllerContext.RegisterSessionAnalyzer(typeof(SessionAnalysisAddInSample));
            controllerContext.RegisterSessionCommand(typeof(SessionAnalysisAddInSample));
            controllerContext.RegisterSessionCommand(typeof(DemoAppAddInSample));
            controllerContext.RegisterLogMessageCommand(typeof(LogMessageCommandSample));
            controllerContext.RegisterCommand(typeof(GlobalCommandSample));

            //you have to provide more information for views because the user has to be able to pick them without them being created.
            controllerContext.RegisterSessionSummaryView(typeof(SessionSummaryViewAddInSample), "Session Search", "Find sessions based on their description, user, host, or command line information", null);
            controllerContext.RegisterSessionView(typeof(SessionViewAddInSample), "Exceptions", "Displays all of the exceptions in the session", null);
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }
    }
}
