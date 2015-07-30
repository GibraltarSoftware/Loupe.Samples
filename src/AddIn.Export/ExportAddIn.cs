
using Gibraltar.Extensibility;
using Gibraltar.Extensibility.Client;

namespace Gibraltar.AddIn.Export
{
    /// <summary>
    /// Top-level class for integrating with the Loupe framework
    /// </summary>
    [LoupeExtension("Gibraltar.AddIn.Export", 
        ConfigurationEditor = typeof(ExportConfigurationDialog),
        MachineConfiguration = typeof(ExportAddInConfiguration))]
    public class ExportAddIn : IExtensionController
    {
        public void Dispose()
        {
        }

        /// <summary>
        /// Called to initialize the add in.
        /// </summary>
        public void Initialize(IExtensionContext context, IExtensionControllerContext controllerContext)
        {
            //we have to register all of our types during this call or they won't be used at all.
            controllerContext.RegisterSessionAnalyzer(typeof(SessionExporter));
            controllerContext.RegisterSessionCommand(typeof(SessionExporter));
        }

        /// <summary>
        /// Called by Gibraltar to indicate the configuration of the add in has changed at runtime
        /// </summary>
        public void ConfigurationChanged()
        {
        }
    }
}
