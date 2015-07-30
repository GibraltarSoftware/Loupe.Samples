using System;
using Gibraltar.Extensibility.Data;

namespace Gibraltar.AddIn.Export
{
    /// <summary>
    /// Configuration data for the Session Export add-in
    /// </summary>
    [Serializable]
    public class ExportAddInConfiguration
    {
        private const string DefaultOutputPath = @"C:\Loupe Exports";

        public ExportAddInConfiguration()
        {
            //set our defaults - by doing it here the editor and everything will reflect them.
            SessionExportPath = DefaultOutputPath;
            AutoExportSessions = true;
            LogMessageFormat = LogMessageFormat.Default;
            MinimumSeverity = LogMessageSeverity.Verbose;
            EnableLogMessageExport = true;
        }

        public string SessionExportPath { get; set; }
        public string Environment { get; set; }
        public bool AutoExportSessions { get; set; }
        public bool UseUniqueFilenames { get; set; }
        public string MetricsToExport { get; set; }
        public bool EnableLogMessageExport { get; set; }
        public LogMessageFormat LogMessageFormat { get; set; }
        public LogMessageSeverity MinimumSeverity { get; set; }
        public bool IncludeSessionSummary { get; set; }
        public bool IncludeExceptionDetails { get; set; }

        public override string ToString()
        {
            return "Path: " + SessionExportPath + "  AutoExport: " + AutoExportSessions + "\n" + MetricsToExport;
        }

        public ExportAddInConfiguration Clone()
        {
            var clone = new ExportAddInConfiguration
            {
                SessionExportPath = SessionExportPath,
                Environment = Environment,
                AutoExportSessions = AutoExportSessions,
                UseUniqueFilenames = UseUniqueFilenames,
                MetricsToExport = MetricsToExport,
                EnableLogMessageExport = EnableLogMessageExport,
                LogMessageFormat = LogMessageFormat,
                MinimumSeverity = MinimumSeverity,
                IncludeSessionSummary = IncludeSessionSummary,
                IncludeExceptionDetails = IncludeExceptionDetails
            };
            return clone;
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExportAddInConfiguration) obj);
        }

        protected bool Equals(ExportAddInConfiguration config)
        {
            return string.Equals(SessionExportPath, config.SessionExportPath)
                && string.Equals(Environment, config.Environment)
                && AutoExportSessions.Equals(config.AutoExportSessions)
                && UseUniqueFilenames.Equals(config.UseUniqueFilenames)
                && string.Equals(MetricsToExport, config.MetricsToExport)
                && EnableLogMessageExport.Equals(config.EnableLogMessageExport)
                && LogMessageFormat == config.LogMessageFormat
                && MinimumSeverity == config.MinimumSeverity
                && IncludeSessionSummary.Equals(config.IncludeSessionSummary)
                && IncludeExceptionDetails.Equals(config.IncludeExceptionDetails);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SessionExportPath != null ? SessionExportPath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Environment != null ? Environment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ AutoExportSessions.GetHashCode();
                hashCode = (hashCode * 397) ^ UseUniqueFilenames.GetHashCode();
                hashCode = (hashCode * 397) ^ (MetricsToExport != null ? MetricsToExport.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ EnableLogMessageExport.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)LogMessageFormat;
                hashCode = (hashCode * 397) ^ (int)MinimumSeverity;
                hashCode = (hashCode * 397) ^ IncludeSessionSummary.GetHashCode();
                hashCode = (hashCode * 397) ^ IncludeExceptionDetails.GetHashCode();
                return hashCode;
            }
        }
    }
}
