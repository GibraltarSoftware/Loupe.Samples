namespace Loupe.Extension.FogBugz
{
    /// <summary>
    /// The high-level FogBugz Case Status
    /// </summary>
    public enum CaseStatus
    {
        /// <summary>
        /// Active
        /// </summary>
        Active = 1,

        /// <summary>
        /// Resolved
        /// </summary>
        Resolved = 2,

        /// <summary>
        /// Resolved and Closed
        /// </summary>
        Closed = 4,

        /// <summary>
        /// Any status
        /// </summary>
        All = Active | Resolved | Closed
    }
}
