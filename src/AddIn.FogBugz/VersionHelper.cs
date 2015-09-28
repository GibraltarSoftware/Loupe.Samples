using System;

namespace Loupe.Extension.FogBugz
{
    /// <summary>
    /// Helper class for comparing version numbers
    /// </summary>
    public class VersionHelper
    {
        private readonly int[] values; // This will contain an array of four values with -1 meaning wildcard (*)
        
        public static void Validate(string version)
        {
            // Will throw exception if invalid
            new VersionHelper(version);
        }

        /// <summary>
        /// Check if a string represents a valid version number pattern
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns> 
        public static bool IsValid(string version)
        {
            try
            {
                Validate(version);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        ///  Compare two version strings by first converting them to VersionHelper instances
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equivalent(string a, string b)
        {
            return Equivalent(new VersionHelper(a), new VersionHelper(b));
        }

        // Use an indexer to make comparison code cleaner
        private int this[int index] { get { return values[index]; } }

        #region Private Properties and Methods

        /// <summary>
        /// Parse the version string and initialize values array to simplify comparison
        /// </summary>
        /// <param name="version"></param> 
        private VersionHelper(string version)
        {
            // Initialize array assuming all wildcards
            values = new[] { -1, -1, -1, -1 };

            // Interpret null or empty string as matching anything
            if(string.IsNullOrEmpty(version))
                return;

            // String might not contain all four elements.
            // Missing elements will be treated as wildcards, i.e. 4 == 4.*.*.*
            string[] parts = version.Split('.');

            // The number of parts must be between 1 and 4, inclusive
            if (parts.Length < 1 || parts.Length > 4)
                ReportInvalidVersion(version);

            try
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    // Each part must either be an non-negative integer or "*" indicating wildcard
                    if (parts[i] != "*")
                    {
                        values[i] = int.Parse(parts[i]);
                        if(values[i] < 0)
                            ReportInvalidVersion(version);
                    }
                }
            }
            catch (Exception)
            {
                ReportInvalidVersion(version);
            }
        }

        // Throw ArgumentException if we get an invalid version string
        private static void ReportInvalidVersion(string version)
        {
            throw new ArgumentException("Invalid version string: " + version);
        }


        private static bool Equivalent(VersionHelper a, VersionHelper b)
        {
            // Compare element by element (major, minor, build, revision)
            for (int i = 0; i < 4; i++)
            {
                // elements only match if either is a wildcard or they match exactly
                if (a[i] >= 0 && b[i] >= 0 && a[i] != b[i])
                    return false;
            }
            return true;
        }

        #endregion
    }
}
