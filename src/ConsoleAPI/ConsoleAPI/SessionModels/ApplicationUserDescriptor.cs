namespace ConsoleAPI.SessionModels
{
    /// <summary>
    /// Details of an applicatiomn user
    /// </summary>
    public class ApplicationUserDescriptor : ActionDescriptor
    {
        /// <summary>
        /// The user email address
        /// </summary>
        public Email Email { get; set; }

        /// <summary>
        /// The user name
        /// </summary>
        public string UserName { get; set; }
    }
}
