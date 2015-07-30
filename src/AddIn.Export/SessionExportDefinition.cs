using System;
using System.Collections.Generic;
using System.Linq;
using Gibraltar.Extensibility.Client;
using Gibraltar.Extensibility.Data;

namespace Gibraltar.AddIn.Export
{
    /// <summary>
    /// Top-level data and logic for session exports
    /// </summary>
    class SessionExportDefinition
    {
        public bool Enabled { get; private set; }
        public ExportAddInConfiguration Config { get; set; }
        private readonly IRepositoryExtensionContext _context;
        private readonly List<string> _environments = new List<string>();
        private readonly HashSet<string> _configuredApplications = new HashSet<string>();
        private readonly List<MetricExportDefinition> _exportDefinitions = new List<MetricExportDefinition>();

        public SessionExportDefinition(IRepositoryExtensionContext context)
        {
            _context = context;
            Config = context.Configuration.Common as ExportAddInConfiguration;
            if (Config == null)
                return; // Enabled is false

            if (!(string.IsNullOrEmpty(Config.Environment) || Config.Environment.Trim().Length == 0))
            {
                var environments = Config.Environment.Split(new []{',',';'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var trimmed in environments.Select(environment => environment.Trim()))
                {
                    _environments.Add(trimmed);
                }
            }

            ParseExportDefinitions(Config.MetricsToExport);
            if (_configuredApplications.Count == 0)
                return;

            Enabled = true; // Indicates that there is work we can do
        }

        /// <summary>
        /// Process all exports associated with a single session
        /// </summary>
        public void Process(ISession session)
        {
            if (!Enabled)
                return;

            // Validate that we have work to do for the application that generated this session
            if (!_configuredApplications.Contains(session.Summary.FullApplicationName()))
                return;

            // If an environment filter is defined, we will only process sessions from those environments
            // as a convenience, math either Environment or PromotionLevel fields
            if (_environments.Count > 0)
            {
                var sessionEnvironment = session.Summary.Environment.Trim();
                var sessionPromotionLevel = session.Summary.PromotionLevel.Trim();
                var environmentMatch = _environments.Any(environment
                    => string.Compare(environment, sessionEnvironment, StringComparison.CurrentCultureIgnoreCase) == 0);
                var promotionLevelMatch = _environments.Any(promotionLevel
                    => string.Compare(promotionLevel, sessionPromotionLevel, StringComparison.CurrentCultureIgnoreCase) == 0);
                if (!(environmentMatch || promotionLevelMatch))
                    return;
            }

            if (Config.EnableLogMessageExport)
            {
                LogMessageFormatter formatter;
                switch (Config.LogMessageFormat)
                {
                    case LogMessageFormat.Summary:
                        formatter = new SummaryLogMessageFormatter(Config);
                        break;
                    case LogMessageFormat.Detailed:
                        formatter = new DetailedLogMessageFormatter(Config);
                        break;
                    default:
                        formatter = new DefaultLogMessageFormatter(Config);
                        break;
                }
                formatter.Export(_context, session);
            }

            foreach (var exportDefinition in _exportDefinitions)
            {
                exportDefinition.Process(session);
            }
        }

        /// <summary>
        /// ParseExportDefinitions builds the in-memory data representation of the Session Export configuration
        /// </summary>
        private void ParseExportDefinitions(string metricsToExport)
        {
            if (metricsToExport == null)
                return;

            var lines = metricsToExport.Split(new[] { '\r', '\n' });
            MetricExportDefinition currentDefinition = null;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.Trim().Length == 0)
                    continue;

                var trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("//"))
                    continue;

                // We distinguish Metric names by having leading whitespace
                // However, we have to hav at least one application name before we can
                // specify any metrics, so if the first line has leading whitespace,
                // we'll be forgiving and interpret it as an application name
                if (char.IsWhiteSpace(line[0]) && currentDefinition != null)
                {
                    currentDefinition.Metrics.Add(trimmedLine);
                }
                else // This is the name of another application
                {
                    if (!_configuredApplications.Contains(trimmedLine))
                        _configuredApplications.Add(trimmedLine);

                    if (currentDefinition == null || currentDefinition.Metrics.Count > 0)
                    {
                        currentDefinition = new MetricExportDefinition(_context, Config);
                        currentDefinition.Applications.Add(trimmedLine);
                        _exportDefinitions.Add(currentDefinition);
                    }
                    else
                    {
                        currentDefinition.Applications.Add(line.Trim());
                    }
                }
            }
        }
    }
}
