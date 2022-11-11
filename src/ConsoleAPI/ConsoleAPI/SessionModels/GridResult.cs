using System;
using System.Collections.Generic;

namespace ConsoleAPI.SessionModels
{
    public class GridResult<T>
    {
        /// <summary>
        /// Gets or sets the total number of results.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the page that the data represents.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the size of a page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the date and time the query was *started* by the server
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the last data and time that will be displayed to the user
        /// </summary>
        public DateTimeOffset TimeStampDisplay { get; set; }

        /// <summary>
        /// Gets or sets an <see cref="IEnumerable" /> containing the current page of data.
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
