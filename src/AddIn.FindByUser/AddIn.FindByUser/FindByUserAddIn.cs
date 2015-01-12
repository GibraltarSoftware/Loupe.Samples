
using Gibraltar.Analyst.AddIn;

namespace Gibraltar.AddIn.FindByUser
{
    /// <summary>
    /// Top-level class for integrating with the Loupe framework
    /// </summary>
    [GibraltarAddIn("Find By User", Description = "This add-in indexes each session by the associated user names.",
        ConfigurationEditor = typeof(FindByUserConfigurationDialog),
        MachineConfiguration = typeof(FindByUserConfiguration))]
    public class FindByUserAddIn : IAddInController
    {
        public const string LogCategory = "AddIn.FindByUser";

        public void Dispose()
        {
        }

        /// <summary>
        /// Called to initialize the add in.
        /// </summary>
        public void Initialize(IAddInContext context, IAddInControllerContext controllerContext)
        {
            //we have to register all of our types during this call or they won't be used at all.
            controllerContext.RegisterSessionAnalyzer(typeof(SessionAnalyzer));
            controllerContext.RegisterSessionCommand(typeof(SessionAnalyzer));
            controllerContext.RegisterSessionSummaryView(typeof(SessionFilterView), "Find By User", "Find sessions associated with an application user", null);
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }
    }
}
