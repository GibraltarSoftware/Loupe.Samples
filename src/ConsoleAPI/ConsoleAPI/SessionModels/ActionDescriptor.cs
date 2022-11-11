namespace ConsoleAPI.SessionModels
{
    /// <summary>
    /// Describes an action link used by a column value.
    /// </summary>
    public class ActionDescriptor
    {

        /// <summary>
        /// Gets or sets the title of the action.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url to navigate to
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the id for navigation
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// The product (if any) associated with the action
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The application (if any) associated with the action
        /// </summary>
        public string ApplicationName { get; set; }
    }
}
