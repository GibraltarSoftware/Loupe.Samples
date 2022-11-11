using System;

namespace ConsoleAPI.QueryModels
{
    public class SessionQueryModel
    {
        public Query Query { get; set; }

        public string SortKey { get; set; }

        public SortDirection SortDirection { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
