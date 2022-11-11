namespace ConsoleAPI.SessionModels
{
    public class Email
    {
        /// <summary>
        /// The email address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The MD5 hash of the email address
        /// </summary>
        public string Hash { get; set; }
    }
}
