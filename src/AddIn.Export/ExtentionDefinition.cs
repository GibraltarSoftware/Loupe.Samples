using Loupe.Extensibility;
using Loupe.Extensibility.Client;

namespace Loupe.Extension.Export
{
    /// <summary>
    /// Top-level class for integrating with the Loupe framework
    /// </summary>
    [LoupeExtension("Loupe.Extension.Export", 
        ConfigurationEditor = typeof(ExportConfigurationDialog),
        MachineConfiguration = typeof(ExportAddInConfiguration))]
    public class ExtentionDefinition : IExtensionDefinition
    {
        /// <summary>
        /// Called to register the extension.
        /// </summary>
        /// <param name="context">A standard interface to the hosting environment for the Extension, provided to all the different extensions that get loaded.</param><param name="definitionContext">Used to register the various types used by the extension.</param>
        /// <remarks>
        /// <para>
        /// If any exception is thrown during this call this Extension will not be loaded.
        /// </para>
        /// <para>
        /// Register each of the other extension types that should be available to end users through appropriate calls to
        ///             the definitionContext.  These objects will be created and initialized as required and provided the same IExtensionContext object instance provided to this
        ///             method to enable coordination between all of the components.
        /// </para>
        /// <para>
        /// After registration the extension definition object is unloaded and disposed.
        /// </para>
        /// </remarks>
        public void Register(IGlobalContext context, IExtensionDefinitionContext definitionContext)
        {
            //we have to register all of our types during this call or they won't be used at all.
            definitionContext.RegisterSessionAnalyzer(typeof(SessionExporter));
            definitionContext.RegisterSessionCommand(typeof(SessionExporter));
        }
    }
}
