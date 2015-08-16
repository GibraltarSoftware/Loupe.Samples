using Loupe.Extensibility;
using Loupe.Extensibility.Client;

namespace Loupe.Extension.FogBugz
{
    [LoupeExtension(ConfigurationEditor = typeof(ConfigurationEditor),
        CommonConfiguration = typeof(CommonConfig),
        UserConfiguration = typeof(UserConfig),
        ServerConfiguration = typeof(ServerConfig))]
    public class ExtensionDefinition : IExtensionDefinition
    {
        public void Register(IGlobalContext context, IExtensionDefinitionContext definitionContext)
        {
            definitionContext.RegisterRepositoryController(typeof(RepositoryController));
            definitionContext.RegisterRepositoryCommand(typeof(RepositoryCommand));
            definitionContext.RegisterSessionCommand(typeof(SessionAnalyzer));
            definitionContext.RegisterSessionAnalyzer(typeof(SessionAnalyzer));
            definitionContext.RegisterLogMessageCommand(typeof(AddDefectCommand));
            definitionContext.RegisterSessionSummaryView(typeof(FogBugzSummaryView), "FogBugz Case List", "Find sessions associated with FogBugz cases", null);
            definitionContext.RegisterSessionSummaryView(typeof(FogBugzLookupView), "FogBugz Lookup", "Find sessions associated with a FogBugz Case Id", null);
        }
    }
}
