using System;
using System.Collections.Generic;
using System.Text;

namespace Gibraltar.AddIn.FogBugz
{
    /// <summary>
    /// Used to specify how back to check for last update dats
    /// </summary>
    public enum LastUpdatedFilter
    {
        /// <summary>
        /// Go back forever
        /// </summary>
        None = 0,

        /// <summary>
        /// One year
        /// </summary>
        OneYear = 1,

        /// <summary>
        /// Three Months
        /// </summary>
        ThreeMonths = 2,

        /// <summary>
        /// One Month
        /// </summary>
        OneMonth = 3,

        /// <summary>
        /// One Week
        /// </summary>
        OneWeek = 4,

        /// <summary>
        /// One Day.  Seriously.
        /// </summary>
        OneDay = 5,
    }
}
