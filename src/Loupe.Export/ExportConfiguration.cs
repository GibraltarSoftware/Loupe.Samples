using System;
using Loupe.Extensibility.Data;

namespace Loupe.Export
{
    /// <summary>
    /// Configuration data for the Session Export add-in
    /// </summary>
    [Serializable]
    public class ExportConfiguration
    {
        private const string DefaultOutputPath = @"C:\Loupe Exports";

        public ExportConfiguration()
        {
            //set our defaults - by doing it here the editor and everything will reflect them.
            LogMessageFormat = LogMessageFormat.Default;
            MinimumSeverity = LogMessageSeverity.Verbose;
            EnableLogMessageExport = true;
        }

        public string SourceFileNamePath { get; set; }

        public string TargetFileNamePath { get; set; }

        public string Environment { get; set; }
        public bool EnableLogMessageExport { get; set; }
        public LogMessageFormat LogMessageFormat { get; set; }
        public LogMessageSeverity MinimumSeverity { get; set; }
        public bool IncludeSessionSummary { get; set; }
        public bool IncludeExceptionDetails { get; set; }

        public ExportConfiguration Clone()
        {
            var clone = new ExportConfiguration
            {
                SourceFileNamePath = SourceFileNamePath,
                TargetFileNamePath = TargetFileNamePath,
                Environment = Environment,
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
            return Equals((ExportConfiguration) obj);
        }

        protected bool Equals(ExportConfiguration config)
        {
            return string.Equals(SourceFileNamePath, config.SourceFileNamePath)
                && string.Equals(TargetFileNamePath, config.TargetFileNamePath)
                   && string.Equals(Environment, config.Environment)
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
                var hashCode = (SourceFileNamePath != null ? SourceFileNamePath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TargetFileNamePath != null ? TargetFileNamePath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Environment != null ? Environment.GetHashCode() : 0);
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
