using System;
using System.Diagnostics;
using Gibraltar.Agent.Metrics;

namespace Gibraltar.Agent
{
    /// <summary>
    /// A very simple class for timing the execution of code blocks as a Loupe metric
    /// </summary>
    public class Caliper : IDisposable
    {
        EventMetric Metric { get; set; }
        private bool ExplicitStartStop { get; set; }
        private Stopwatch Timer { get; set; }
        private TimeSpan? WarningTimeSpan { get; set; }
        private bool _disposed;
        private string _logCategory;

        private const string RootCategory = "Caliper"; // Root category for organizing calipers
        private const string DefaultInstance = "Default"; // Name to use if none specified
        private const string MetricSystem = "Caliper"; // defines a namespace for categories
        private const string DurationCaption = "Duration"; // Caption to use for timer duration
        private const int MaxDecimalDigits = 3; // Maximum number of decimal digits in log messages reporting seconds

        /// <summary>
        /// Returns a Timespan containing the elapsed time of this Caliper
        /// </summary>
        public TimeSpan Elapsed { get { return Timer.Elapsed; } }

        /// <summary>
        /// Returns a long containing the number of milliseconds elapsed for this Caliper
        /// </summary>
        public long ElapsedMilliseconds { get { return Timer.ElapsedMilliseconds; } }

       /// <summary>
       /// Initialize a timer with a dot-delimited name and optional threshold Timespan for warnings
       /// </summary>
       /// <remarks>
       /// Caliper implements IDisposable so you can time a block of code by wrapping it in a using block.
       /// If you want to time something more complex than a contiguous code block, use the Start/Stop methods.
       /// </remarks>
       /// <param name="name">The dot-delimited name of this Caliper</param>
       /// <param name="warningTime">Log a warning if the Caliper exceeds this threshold</param>
        public Caliper(string name, TimeSpan? warningTime = null)
        {
            InitializeMetric(name);
            WarningTimeSpan = warningTime;

            ExplicitStartStop = false;
            Timer = new Stopwatch();
            Timer.Start();
        }

        /// <summary>
        /// Explicitly start timing a block of code 
        /// </summary>
        /// <remarks>
        /// When Start is first called we don't write a bogus sample
        /// for the time in between the Caliper being created and the
        /// call to Start. Aside from that edge case, repeated Start
        /// calls imply a Stop call between them
        /// </remarks>
        public void Start()
        {
            if (_disposed)
                return;

            if (Timer.IsRunning && ExplicitStartStop)
            {
                Timer.Stop();
                WriteMetric();
            }

            ExplicitStartStop = true;
            Timer.Restart();
        }

        /// <summary>
        /// Stop the time and write a metric sample
        /// </summary>
        public void Stop()
        {
            if (_disposed)
                return;

            if (Timer.IsRunning)
            {
                Timer.Stop();
                WriteMetric();
            }
            ExplicitStartStop = true;
        }

        /// <summary>
        /// Dispose is an implicit Stop allowing timing of using blocks 
        /// </summary>
        public void Dispose()
        {
            if (!_disposed && Timer.IsRunning)
                WriteMetric();

            _disposed = true;
        }

        /// <summary>
        /// Perform the one-time work to create a metric
        /// </summary>
        /// <param name="name">Dot-delimited display name for this metric</param>
        private void InitializeMetric(string name)
        {
            // Handle edge cases
            if (string.IsNullOrEmpty(name))
                name = DefaultInstance;

            name = name.Trim();
            while (!string.IsNullOrEmpty(name) && name[0] == '.')
                name = name.Substring(1).Trim();

            // Set up category and instance as Loupe wants
            string category;
            string instance;

            var pos = name.LastIndexOf('.'); // check for delimited name
            if (pos <= 0)
            {
                // If not deliminated, just use base category
                category = RootCategory;
                instance = name;
            }
            else
            {
                // If delimited, just use the last part as instance name
                // and combine the rest with category
                category = RootCategory + '.' + name.Substring(0, pos);
                instance = name.Substring(pos + 1);
            }

            _logCategory = category + "." + instance; // Initializ category for logging

            EventMetricDefinition metricDefinition;

            // Create the metric on first call then use cached copy thereafter
            if (!EventMetricDefinition.TryGetValue(MetricSystem, category, instance, out metricDefinition))
            {
                metricDefinition = new EventMetricDefinition(MetricSystem, category, instance);
                metricDefinition.AddValue(DurationCaption, typeof(TimeSpan), SummaryFunction.Average,
                    null, DurationCaption, null);
                EventMetricDefinition.Register(ref metricDefinition);
            }

            // Grab the metric from cache
            Metric = EventMetric.Register(metricDefinition, null);
        }

        /// <summary>
        /// Write a metric sample
        /// </summary>
        private void WriteMetric()
        {
            EventMetricSample sample = Metric.CreateSample();
            var elapsed = Timer.Elapsed;
            sample.SetValue(DurationCaption, elapsed);
            sample.Write();

            if (WarningTimeSpan.HasValue && WarningTimeSpan.Value > TimeSpan.Zero && WarningTimeSpan.Value < elapsed)
            {
                var threshold = WarningTimeSpan.Value.TotalSeconds;
                var caption = _logCategory + " exceeds " + Math.Round(threshold, MaxDecimalDigits) + " seconds";
                var description = "Elapsed time = " + Math.Round(elapsed.TotalSeconds, MaxDecimalDigits) + " seconds";
                Log.Warning(null, _logCategory, caption, description);
            }
        }
    }
}
