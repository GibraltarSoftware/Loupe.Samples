namespace ConsoleAPI.SessionModels
{
    /// <summary>
    /// Descriptor for application versions
    /// </summary>
    public class VersionDescriptor : ActionDescriptor
    {
        /// <summary>
        /// Binary, sortable, representation of the version number
        /// </summary>
        public long Bin { get; set; }

        /// <summary>
        /// Version number
        /// </summary>
        public string Version { get; set; }
    }
}
