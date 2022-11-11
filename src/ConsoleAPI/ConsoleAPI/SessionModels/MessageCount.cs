using System;

namespace ConsoleAPI.SessionModels
{
    /// <summary>
    /// Defines a message count for a session
    /// </summary>
    public class MessageCount
    {
        /// <summary>
        /// The session id
        /// </summary>
        public Guid? SessionId { get; set; }

        /// <summary>
        /// The severity of the message
        /// </summary>
        public MessageType Severity { get; set; }

        /// <summary>
        /// The number of messages
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The icon to use
        /// </summary>
        public string Icon { get; set; }
    }
}
