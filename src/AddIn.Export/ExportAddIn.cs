
using Gibraltar.Analyst.AddIn;

namespace Gibraltar.AddIn.Export
{
    /// <summary>
    /// Top-level class for integrating with the Loupe framework
    /// </summary>
    [GibraltarAddIn("Export Sessions", Description = "This add-in exports Loupe session data to text files. "
    + "It is most useful in combination with other programs such as Excel or Splunk that allow analysis of log data. "
    + "You specify which metrics you are interested in from which applications and this add-in will create text files "
    + "for each specified metric", 
        ConfigurationEditor = typeof(ExportConfigurationDialog),
        MachineConfiguration = typeof(ExportAddInConfiguration))]
    public class ExportAddIn: IAddInController
    {
        public void Dispose()
        {
        }

        /// <summary>
        /// Called to initialize the add in.
        /// </summary>
        public void Initialize(IAddInContext context, IAddInControllerContext controllerContext)
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
