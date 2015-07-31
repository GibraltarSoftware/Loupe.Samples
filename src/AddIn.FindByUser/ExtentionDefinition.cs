using Loupe.Extensibility;
using Loupe.Extensibility.Client;

namespace Loupe.Extension.FindByUser
{
    /// <summary>
    /// Top-level class for integrating with the Loupe framework
    /// </summary>
    [LoupeExtension("Loupe.Extension.FindByUser", 
        ConfigurationEditor = typeof(FindByUserConfigurationDialog),
        CommonConfiguration = typeof(FindByUserConfiguration))]
    public class ExtentionDefinition : IExtensionDefinition
    {
        public const string LogCategory = "Find By User";

        /// <summary>
        /// Called to initialize the add in.
        /// </summary>
        public void Register(IGlobalContext context, IExtensionDefinitionContext controllerContext)
        {
            //we have to register all of our types during this call or they won't be used at all.
            controllerContext.RegisterSessionAnalyzer(typeof(SessionAnalyzer));
            controllerContext.RegisterSessionCommand(typeof(SessionAnalyzer));
            controllerContext.RegisterSessionSummaryView(typeof(SessionFilterView), "Find By User", "Find sessions associated with an application user", null);
        }
    }
}
