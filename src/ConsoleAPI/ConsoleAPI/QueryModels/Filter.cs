namespace ConsoleAPI.QueryModels
{
    public class Filter
    {
        public string Column { get; set; }

        public Operator Operator { get; set; }

        public object Value1 { get; set; }

        public object Value2 { get; set; }

        public object Grouping { get; set; }
    }
}
