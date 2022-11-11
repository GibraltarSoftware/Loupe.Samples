using System.Collections.Generic;

namespace ConsoleAPI.QueryModels
{
    public class Query
    {
        public IEnumerable<Filter> Filters { get; set; }
        public Pagination Paging { get; set; }
        public IEnumerable<Sort> Sort { get; set; }
    }
}
