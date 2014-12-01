using System;

namespace Gibraltar.AddIn.Test
{
    /// <summary>
    /// Stores the configuration of of our sample addin
    /// </summary>
    [Serializable]
    public class SampleAddInConfiguration
    {
        public const string DefaultOutputPath = @"C:\Data\Sessions";

        public SampleAddInConfiguration()
        {
            //set our defaults - by doing it here the editor and everything will reflect them.
            AutoExportSessions = true;
            SessionExportPath = DefaultOutputPath;
        }

        public bool AutoExportSessions { get; set; }

        public string SessionExportPath { get; set; }
    }
}
