using Gibraltar.Extensibility.Data;

namespace Gibraltar.AddIn.Export
{
    static class ExtensionMethods
    {
        /// <summary>
        /// FullApplicationName is guaranteed to only have a period between Product and Application
        /// </summary>
        public static string FullApplicationName(this ISessionSummary summary)
        {
            // Simplify parsing by ensuring no embedded periods in Product or Application
            return summary.Product.Replace(".", " ") + "." + summary.Application.Replace(".", " ");
        }

        /// <summary>
        /// FullMetricName is guaranteed to not have a period in CounterName
        /// </summary>
        public static string FullMetricName(this IMetricDefinition definition)
        {
            // Simplify parsing by ensuring CounterName has no embedded periods
            return definition.CategoryName + "." + definition.CounterName.Replace(".", " ");
        }
    }
}
