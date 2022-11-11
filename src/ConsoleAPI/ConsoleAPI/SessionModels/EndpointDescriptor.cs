namespace ConsoleAPI.SessionModels
{
    /// <summary>
    /// Describes an endpoint action link used by a column value.
    /// </summary>
    public class EndpointDescriptor : ActionDescriptor
    {
        /// <summary>
        /// Caption of the environment the computer is in
        /// </summary>
        public string EnvironmentCaption { get; set; }

    }
}
