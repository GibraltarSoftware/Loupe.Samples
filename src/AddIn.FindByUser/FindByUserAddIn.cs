
using Gibraltar.Extensibility;
using Gibraltar.Extensibility.Client;

namespace Gibraltar.AddIn.FindByUser
{
    /// <summary>
    /// Top-level class for integrating with the Loupe framework
    /// </summary>
    [LoupeExtension("Gibraltar.AddIn.FindByUser", 
        ConfigurationEditor = typeof(FindByUserConfigurationDialog),
        CommonConfiguration = typeof(FindByUserConfiguration))]
    public class FindByUserAddIn : IExtensionController
    {
        public const string LogCategory = "Find By User";

        public void Dispose()
        {
        }

        /// <summary>
        /// Called to initialize the add in.
        /// </summary>
        public void Initialize(IExtensionContext context, IExtensionControllerContext controllerContext)
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
