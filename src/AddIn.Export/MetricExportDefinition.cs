using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Loupe.Extensibility.Client;
using Loupe.Extensibility.Data;

namespace Loupe.Extension.Export
{
    /// <summary>
    /// Core data and logic associated with exporting session data
    /// </summary>
    class MetricExportDefinition
    {
        private const string LogCategory = "SessionExport.Export"; 
        public HashSet<string> Applications { get; private set; }
        public HashSet<string> Metrics { get; private set; }
        private ExportAddInConfiguration Config { get; set; }
        private readonly IRepositoryContext _context;

        public MetricExportDefinition(IRepositoryContext context, ExportAddInConfiguration config)
        {
            _context = context;
            Config = config;
            Applications = new HashSet<string>();
            Metrics = new HashSet<string>();
        }

        /// <summary>
        /// Exports configured data for this session, if it is an application we care about
        /// </summary>
        public void Process(ISession session)
        {
            // Validate that we have work to do for the application that generated this session
            if (!Applications.Contains(session.Summary.FullApplicationName()))
                return;

            var metrics = session.MetricDefinitions.Where(md => Metrics.Contains(md.FullMetricName())).ToList();
            var metricFileCount = 0;
            foreach (var metric in metrics)
            {
                if (metric.SampleType == SampleType.Sampled)
                    ExportSampledMetric(session, metric, ref metricFileCount);
                else
                    ExportEventMetric(session, (IEventMetricDefinition)metric, ref metricFileCount);
            }
        }

        /// <summary>
        /// Export a CSV file for each instance of a sampled metric
        /// </summary>
        private void ExportSampledMetric(ISession session, IMetricDefinition metricDefinition, ref int metricFileCount)
        {
            if (metricDefinition.Metrics.Count == 1)
            {
                try
                {
                    using (
                        var writer = StreamCreator.CreateMetricStream(_context, Config, session, metricDefinition,
                            ref metricFileCount))
                    {
                        ExportSamples(writer, metricDefinition.Metrics[0]);
                    }
                }
                catch (Exception ex)
                {
                    _context.Log.RecordException(ex, LogCategory, true);
                }
            }
            else
            {
                foreach (var metric in metricDefinition.Metrics)
                {
                    try
                    {
                        using (
                            var writer = StreamCreator.CreateMetricInstanceStream(_context, Config, session, metric,
                                ref metricFileCount))
                        {
                            ExportSamples(writer, metric);
                        }
                    }
                    catch (Exception ex)
                    {
                        _context.Log.RecordException(ex, LogCategory, true);
                    }
                }
            }
        }

        /// <summary>
        /// ExportSamples is the shared logic for all sampled metric exports
        /// </summary>
        private void ExportSamples(StreamWriter writer, IMetric metric)
        {
            writer.Write("\"Sequence\",\"Timestamp\",\"{0}\"\r\n",
                string.IsNullOrEmpty(metric.InstanceName) ? "Value" : metric.InstanceName);

            foreach (var sample in metric.Samples)
            {
                writer.Write("{0},{1},{2}\r\n", sample.Sequence,sample.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), sample.Value);
            }
        }


        /// <summary>
        /// Export a CSV file for each instance of am EventMetric
        /// </summary>
        private void ExportEventMetric(ISession session, IEventMetricDefinition metricDefinition, ref int metricFileCount)
        {
            if (metricDefinition.Metrics.Count == 1)
            {
                try
                {
                    using (
                        var writer = StreamCreator.CreateMetricStream(_context, Config, session, metricDefinition,
                            ref metricFileCount))
                    {
                        ExportEventSamples(writer, metricDefinition, metricDefinition.Metrics[0] as IEventMetric);
                    }
                }
                catch (Exception ex)
                {
                    _context.Log.RecordException(ex, LogCategory, true);
                }
            }
            else
            {
                foreach (var metric in metricDefinition.Metrics)
                {
                    try
                    {
                        using (
                            var writer = StreamCreator.CreateMetricInstanceStream(_context, Config, session, metric,
                                ref metricFileCount))
                        {
                            ExportEventSamples(writer, metricDefinition, metric as IEventMetric);
                        }
                    }
                    catch (Exception ex)
                    {
                        _context.Log.RecordException(ex, LogCategory, true);
                    }
                }
            }
        }

        /// <summary>
        /// ExportEvemtSamples is the shared logic for all event metric exports
        /// </summary>
        private void ExportEventSamples(StreamWriter writer, IEventMetricDefinition metricDefinition, IEventMetric metric)
        {
            writer.Write("\"Sequence\",\"Timestamp\"");
            foreach (var valueDefinition in metricDefinition.Values)
            {
                writer.Write(",\"{0}\"", valueDefinition.Caption);
            }
            writer.WriteLine();

            foreach (var sample in metric.Samples)
            {
                var eventSample = sample as IEventMetricSample;
                if (eventSample == null)
                    continue;

                writer.Write("{0},\"{1}\"", eventSample.Sequence, eventSample.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
                for (var i = 0; i < eventSample.Values.Count(); i++)
                {
                    writer.Write(",\"{0}\"", eventSample.Values[i]);
                }
                writer.WriteLine();
            }
        }
    }
}
