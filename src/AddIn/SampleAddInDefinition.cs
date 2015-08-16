using Loupe.Extensibility;
using Loupe.Extensibility.Client;

namespace Loupe.Extension.Sample
{
    [LoupeExtension(ConfigurationEditor = typeof(SampleConfigurationDialog),
        CommonConfiguration = typeof(SampleAddInConfiguration))]
    public class SampleAddInDefinition : IExtensionDefinition
    {
        public void Register(IGlobalContext context, IExtensionDefinitionContext definitionContext)
        {
            //we have to register all of our types during this call or they won't be used at all.
            definitionContext.RegisterSessionAnalyzer(typeof(SessionAnalysisAddInSample));
            definitionContext.RegisterSessionCommand(typeof(SessionAnalysisAddInSample));
            definitionContext.RegisterSessionCommand(typeof(DemoAppAddInSample));
            definitionContext.RegisterLogMessageCommand(typeof(LogMessageCommandSample));
            definitionContext.RegisterGlobalCommand(typeof(GlobalCommandSample));

            //you have to provide more information for views because the user has to be able to pick them without them being created.
            definitionContext.RegisterSessionSummaryView(typeof(SessionSummaryViewAddInSample), "Session Search", "Find sessions based on their description, user, host, or command line information", null);
            definitionContext.RegisterSessionView(typeof(SessionViewAddInSample), "Exceptions", "Displays all of the exceptions in the session", null);
        }
    }
}
