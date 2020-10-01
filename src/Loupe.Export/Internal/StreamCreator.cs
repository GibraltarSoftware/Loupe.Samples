using System.IO;
using Gibraltar;
using Gibraltar.Monitor;
using Loupe.Extensibility.Data;

namespace Loupe.Export.Internal
{
    /// <summary>
    /// Create a well-named file in the appropriate folder
    /// </summary>
    internal static class StreamCreator
    {
        private const string LogCategory = "SessionExport.Export";
        
        /// <summary>
        /// Shared logic for creating all streams
        /// </summary>
        public static StreamWriter CreateStream(string fileNamePath)
        {
            FileSystemTools.EnsurePathExists(fileNamePath);

            // Create the stream
            Log.Write(LogMessageSeverity.Verbose, LogCategory, "Creating file to export session data", fileNamePath);
            return new StreamWriter(File.Create(fileNamePath));
        }
    }
}
