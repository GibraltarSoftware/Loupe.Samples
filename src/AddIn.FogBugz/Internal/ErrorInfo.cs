using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Loupe.Extensibility.Data;

namespace Loupe.Extension.FogBugz.Internal
{
    /// <summary>
    /// Encapsulates an algorithm for determining the fingerprint of a log messages
    /// </summary>
    [DebuggerDisplay("{Fingerprint}")]
    internal class ErrorInfo
    {
        public string Fingerprint { get; private set; }
        public List<ILogMessage> Messages { get; private set; }


        public ErrorInfo(ILogMessage message)
        {
            Fingerprint = GetFingerprint(message);
            Messages = new List<ILogMessage> { message };
        }

        /// <summary>
        /// Return a unique fingerprint string for this message (presumably an error)
        /// </summary>
        /// <remarks>
        /// <para>This is an important method.  Think of the fingerprint like a hashcode.
        /// All instances of the same logical error should have the same fingerprint.
        /// Each unique error should have a unique fingerprint.
        /// </para>
        /// <para>This implementation makes the reasonably good assumption that a fingerprint
        /// should be based on product + application + caption + classname + category.</para>
        /// <para>There are three main weaknesses to this approach:</para>
        /// <para>1. If the caption varies across instances, each will be assigned
        /// a unique fingerprint. To minimize this, make sure to put all dynamic fields such as
        /// parameter values in the message description, not the caption.</para>
        /// <para>2. Localization. If the caption is localized, each language will result
        /// in a unique fingerprint.</para>
        /// <para>3. If the caption is too generic (ex., null reference exception) different
        /// errors might be erroneously grouped together.</para>
        /// </remarks>
        private static string GetFingerprint(ILogMessage message)
        {
            ISessionSummary summary = message.Session.Summary;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Loupe Error Fingerprint:\r\n");
            builder.AppendFormat("Product: {0}\r\n", summary.Product);
            builder.AppendFormat("Application: {0}\r\n", summary.Application);
            builder.AppendFormat("Caption: {0}\r\n", message.Caption);
            builder.AppendFormat("ClassName: {0}\r\n", message.ClassName);
            builder.AppendFormat("Category: {0}\r\n", message.CategoryName);

            string hash = GetHash(builder.ToString());
            return hash;
        }

        /// <summary>
        /// We use an MD5 hash of the raw fingerprint text to ensure it fits within the 255 character limit
        /// imposed by FogBugz 
        /// </summary>
        private static string GetHash(string originalText)
        {
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] originalBytes = ASCIIEncoding.Default.GetBytes(originalText);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }
    }
}
