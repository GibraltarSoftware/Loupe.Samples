using System;
using System.IO;

namespace Gibraltar.AddIn.FindByUser
{
    /// <summary>
    /// Configuration data for the Session Export add-in
    /// </summary>
    [Serializable]
    public class FindByUserConfiguration
    {
        public string DatabaseProvider { get; set; }
        public string ConnectionString { get; set; }
        public bool AutoScanSessions { get; set; }

        public FindByUserConfiguration()
        {
            //set our defaults - by doing it here the editor and everything will reflect them.
            DatabaseProvider = "VistaDB";

            var baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var filePath = Path.Combine(baseFolder, @"Gibraltar\Add In\FindByUser.vdb4");
            ConnectionString = string.Format(
                @"Data Source={0};Open Mode=NonExclusiveReadWrite;Pooling=true;Min Pool Size=3;Max Pool Size=8;",
                filePath);

            AutoScanSessions = true;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}\nAutoScan: {2}", DatabaseProvider, ConnectionString, AutoScanSessions);
        }

        public FindByUserConfiguration Clone()
        {
            var clone = new FindByUserConfiguration
            {
                DatabaseProvider = DatabaseProvider,
                ConnectionString = ConnectionString,
                AutoScanSessions = AutoScanSessions
            };
            return clone;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FindByUserConfiguration) obj);
        }

        protected bool Equals(FindByUserConfiguration config)
        {
            return string.Equals(DatabaseProvider, config.DatabaseProvider)
                   && string.Equals(ConnectionString, config.ConnectionString)
                   && AutoScanSessions.Equals(config.AutoScanSessions);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DatabaseProvider != null ? DatabaseProvider.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ConnectionString != null ? ConnectionString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ AutoScanSessions.GetHashCode();
                return hashCode;
            }
        }
    }
}
