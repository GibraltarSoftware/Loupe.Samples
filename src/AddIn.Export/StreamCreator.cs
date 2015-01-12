using System.IO;
using System.Text.RegularExpressions;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.Export
{
    /// <summary>
    /// Create a well-named file in the appropriate folder
    /// </summary>
    static class StreamCreator
    {
        private const string LogCategory = "SessionExport.Export";
        static private Regex illegalInFileName = new Regex(string.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()))), RegexOptions.Compiled);
        static private Regex illegalInPath = new Regex(string.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()).Replace("\\",""))), RegexOptions.Compiled);

        /// <summary>
        /// Return the path to the exported files associated with an application
        /// </summary>
        public static string GetApplicationPath(ExportAddInConfiguration config, ISessionSummary sessionSummary)
        {
            var info = sessionSummary;
            var subFolder = Path.Combine(info.Product, info.Application);
            subFolder = illegalInPath.Replace(subFolder, "_");
            var fullPath = Path.Combine(config.SessionExportPath, subFolder);
            return fullPath;
        }

        /// <summary>
        /// Create a TXT file for exporting log messages
        /// </summary>
        static public StreamWriter CreateLogStream(IRepositoryAddInContext context, ExportAddInConfiguration config, ISession session)
        {
            var info = session.Summary;
            var subFolder = Path.Combine(info.Product, info.Application);
            var fileName = info.EndDateTime.ToString("yyyy-MM-dd HH-mm-ss") + " on " + info.HostName;
            if (config.UseUniqueFilenames)
                fileName += " " + info.Id;

            return CreateStream(context, config.SessionExportPath, subFolder, fileName, ".txt");
        }

        /// <summary>
        /// Create a CSV for a metric that contains only a single instance
        /// </summary>
        static public StreamWriter CreateMetricStream(IRepositoryAddInContext context, ExportAddInConfiguration config, ISession session, IMetricDefinition metric, ref int metricFileCount)
        {
            var info = session.Summary;
            var subFolder = Path.Combine(info.Product, info.Application);
            var metricName = metric.CategoryName + "." + metric.CounterName;
            subFolder = Path.Combine(subFolder, metricName);
            var fileName = info.EndDateTime.ToString("yyyy-MM-dd HH-mm-ss") + " on " + info.HostName;
            fileName += " (" + ++metricFileCount + ")"; // Uniquify filename for convenience with Excel
            if (config.UseUniqueFilenames)
                fileName += " " + info.Id;

            return CreateStream(context, config.SessionExportPath, subFolder, fileName, ".csv");
        }

        /// <summary>
        /// Create a CSV for a metric that contains multiple instances
        /// </summary>
        public static StreamWriter CreateMetricInstanceStream(IRepositoryAddInContext context, ExportAddInConfiguration config, ISession session, IMetric metric, ref int metricFileCount)
        {
            var info = session.Summary;
            var subFolder = Path.Combine(info.Product, info.Application);
            var metricName = metric.CategoryName + "." + metric.CounterName + "." + metric.InstanceName;
            subFolder = Path.Combine(subFolder, metricName);
            var fileName = info.EndDateTime.ToString("yyyy-MM-dd HH-mm-ss") + " on " + info.HostName;
            fileName += " (" + ++metricFileCount + ")"; // Uniquify filename for convenience with Excel
            if (config.UseUniqueFilenames)
                fileName += " " + info.Id;

            return CreateStream(context, config.SessionExportPath, subFolder, fileName, ".csv");
        }

        /// <summary>
        /// Shared logic for creating all streams
        /// </summary>
        static StreamWriter CreateStream(IRepositoryAddInContext context, string baseFolder, string subFolder, string fileName, string filetype)
        {
            // Guard against illegal characters
            subFolder = illegalInPath.Replace(subFolder, "_");
            fileName = illegalInFileName.Replace(fileName, "_");

            // Ensure the folder exists
            string folder = Path.Combine(baseFolder, subFolder);
            if (Directory.Exists(folder) == false)
                Directory.CreateDirectory(folder);

            // Create the stream
            string fullPath = Path.Combine(folder, fileName + filetype);
            context.Log.Verbose(LogCategory, "Creating file to export session data", fullPath);
            return new StreamWriter(fullPath);
        }
    }
}
