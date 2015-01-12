using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Gibraltar.AddIn.Alert
{
    public class AlertDatabase
    {
        private DbProviderFactory ProviderFactory { get; set; }
        private string ConnectionString { get; set; }
        private DbConnection Connection { get; set; }

        public AlertDatabase(DbProviderFactory providerFactory, string connectionString)
        {
            ProviderFactory = providerFactory;
            ConnectionString = connectionString;
            Connection = GetConnection();
        }

        private DbConnection GetConnection()
        {
            DbConnection connection = ProviderFactory.CreateConnection();
            if (connection == null)
                throw new ArgumentException("Could not create a connection for Database Provider " + ProviderFactory.GetType().Name);

            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }

        public void AddOrUpdateSession(Guid sessionId, Guid computerId, DateTimeOffset sessionDate,
            string product, string application, Version appVersion, string hostName,
            double memoryPercent, double processorPercent)
        {
            var updateRequired = false;
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "select MaxMemoryPercent, MaxProcessorPercent from Session "
                                      + "where PK_Session_Id = @sessionId and SessionDate = @sessionDate";
                AddParameter(command, "@sessionId", sessionId);
                AddParameter(command, "@sessionDate", sessionDate);

                var reader = command.ExecuteReader();

                // If there's already a record in the database for this session on this date,
                // then we may need to update it
                if (reader.Read())
                {
                    var row = new object[2];
                    if (reader.GetValues(row) == 2) // should always be true
                    {
                        var memory = (int) row[0];
                        var processor = (int) row[1];

                        // Nothing to do if database is already up-to-date
                        if (memoryPercent <= memory && processorPercent <= processor)
                            return;

                        updateRequired = true;
                    }
                    else
                    {
                        //TODO Log schema problem
                    }
                }
            }

            // This is the simple case of updating an existing row
            if (updateRequired)
            {
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = "update Session "
                                          + "set MaxMemoryPercent = @memory, MaxProcessorPercent = @processor "
                                          + "where PK_Session_Id = @sessionId and SessionDate = @sessionDate";
                    AddParameter(command, "@sessionId", sessionId);
                    AddParameter(command, "@sessionDate", sessionDate);
                    AddParameter(command, "@memory", (int) (memoryPercent*100));
                    AddParameter(command, "@processor", (int) (processorPercent*100));

                    command.ExecuteNonQuery();
                }
                return;
            }

            // In the simple case of updating an existing session, we never get this far
            // This is the general case of addinng a new session record and it includes
            // variants in which we also have to add a new application, appversion, or computer.

            // Deal with application first ensuring that we have a valid application id for this session
            int? applicationId;
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "select PK_Application_Id from Application "
                                      + "where Product = @product and Application = @application";
                AddParameter(command, "@product", product);
                AddParameter(command, "@application", application);
                applicationId = (int?) command.ExecuteScalar();
            }

            if (applicationId == null)
            {
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = "insert into Application (Product, Application) "
                                          + "values (@product, @application); select @@IDENTITY";
                    AddParameter(command, "@product", product);
                    AddParameter(command, "@application", application);
                    applicationId = (int?) command.ExecuteScalar();
                }
            }

            if (!applicationId.HasValue)
            {
                //TODO Log error
                return;
            }

            // Next, we need our version id
            int? versionId;
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "select PK_AppVersion_Id from AppVersion where "
                                      + "Major = @major and Minor = @minor and Build = @build "
                                      + "and Revision = @revision and FK_Application_Id = @applicationId";
                AddParameter(command, "@major", appVersion.Major);
                AddParameter(command, "@minor", appVersion.Minor);
                AddParameter(command, "@build", appVersion.Build);
                AddParameter(command, "@revision", appVersion.Revision);
                AddParameter(command, "@applicationId", applicationId.Value);
                versionId = (int?) command.ExecuteScalar();
            }

            if (versionId == null)
            {

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = "insert into AppVersion "
                                          + "(FK_Application_Id, Major, Minor, Build, Revision) "
                                          + "values (@applicationId, @major, @minor, @build, @revision);"
                                          + "select @@IDENTITY";
                    AddParameter(command, "@applicationId", applicationId.Value);
                    AddParameter(command, "@major", appVersion.Major);
                    AddParameter(command, "@minor", appVersion.Minor);
                    AddParameter(command, "@build", appVersion.Build);
                    AddParameter(command, "@revision", appVersion.Revision);
                    versionId = (int?) command.ExecuteScalar();
                }
            }

            if (!versionId.HasValue)
            {
                //TODO Log error
                return;
            }

            // Next, we need to check if we've logged for this computer before 

            string hostInDatabase;
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "select HostName from Computer where PK_Computer_Id = @computerId";
                AddParameter(command, "@computerId", computerId);
                hostInDatabase = (string) command.ExecuteScalar();
            }

            if (hostInDatabase == null)
            {
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText =
                        "insert into Computer (PK_Computer_Id, HostName) values (@computerId, @hostName)";
                    AddParameter(command, "@computerId", computerId);
                    AddParameter(command, "@hostName", hostName);
                    command.ExecuteNonQuery();
                }
            }
            else if (hostName != hostInDatabase)
            {
                // Update HostName if it has changed
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = "update Computer set HostName = @hostName where PK_Computer_Id = @computerId";
                    AddParameter(command, "@computerId", computerId);
                    AddParameter(command, "@hostName", hostName);

                    command.ExecuteNonQuery();
                }
            }

            // Finally, insert a Session row
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "insert into Session (PK_Session_Id, SessionDate, FK_Computer_Id, "
                                      + "FK_AppVersion_Id, MaxMemoryPercent, MaxProcessorPercent) values "
                                      + "(@sessionId, @sessionDate, @computerId, @versionId, @memory, @processor)";
                AddParameter(command, "@sessionId", sessionId);
                AddParameter(command, "@sessionDate", sessionDate);
                AddParameter(command, "@computerId", computerId);
                AddParameter(command, "@versionId", versionId.Value);
                AddParameter(command, "@computerId", computerId);
                AddParameter(command, "@hostName", hostName);
                AddParameter(command, "@memory", (int)(memoryPercent * 100));
                AddParameter(command, "@processor", (int)(processorPercent * 100)); 
                command.ExecuteNonQuery();
            }
        }


        public HashSet<Guid> FindAlertSessions(DateTimeOffset? startTime, DateTimeOffset? endTime,
            string product, string application, double memoryPercentage, double processorPercentage)
        {
            var sessionIds = new HashSet<Guid>();
            var sql = "select PK_Session_Id as Id from Session s "
                      + "inner join Computer c on c.PK_Computer_Id = s.FK_Computer_Id "
                      + "inner join AppVersion v on v.PK_AppVersion_Id = s.FK_AppVersion_Id "
                      + "inner join Application a on a.PK_Application_Id = v.FK_Application_Id "
                      + "where a.Hidden = 0 and v.Hidden = 0 and s.Hidden = 0 ";

            using (var command = Connection.CreateCommand())
            {
                if (startTime != null)
                {
                    sql += "and s.SessionDate >= @startTime ";
                    AddParameter(command, "@startTime", startTime.Value);
                }

                if (endTime != null)
                {
                    sql += "and s.SessionDate <= @endTime ";
                    AddParameter(command, "@endime", endTime.Value); 
                }

                if (product != null)
                {
                    sql += "and a.Product = @product ";
                    AddParameter(command, "@product", product);
                }

                if (application != null)
                {
                    sql += "and a.Application = @application ";
                    AddParameter(command, "@application", application);
                }

                sql += "and (s.MaxMemoryPercent >= @memory or s.MaxProcessorPercent >= @processor) ";
                AddParameter(command, "@memory", (int)(memoryPercentage * 100));
                AddParameter(command, "@processor", (int)(processorPercentage * 100));

                command.CommandText = sql; // Update connand with additional filters
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var row = new object[1];
                    if (reader.GetValues(row) > 0)
                    {
                        var id = (Guid)row[0];
                        sessionIds.Add(id);
                    }
                }
            }

            return sessionIds;
        }

        private void AddParameter(DbCommand command, string name, int value)
        {
            var param = command.CreateParameter();
            param.DbType = DbType.Int32;
            param.ParameterName = name;
            param.Value = value;
            command.Parameters.Add(param);
        }

        private void AddParameter(DbCommand command, string name, Guid value)
        {
            var param = command.CreateParameter();
            param.DbType = DbType.Guid;
            param.ParameterName = name;
            param.Value = value;
            command.Parameters.Add(param);
        }

        private void AddParameter(DbCommand command, string name, DateTimeOffset value)
        {
            var param = command.CreateParameter();
            param.DbType = DbType.DateTimeOffset;
            param.ParameterName = name;
            param.Value = value;
            command.Parameters.Add(param);
        }

        private void AddParameter(DbCommand command, string name, string value)
        {
            var param = command.CreateParameter();
            param.DbType = DbType.String;
            param.ParameterName = name;
            param.Value = value;
            command.Parameters.Add(param);
        }

        public List<string> HiddenApplications()
        {
            var hiddenApplications = new List<string>();

            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "select Product + '.' + Application as FullAppName from Application where Hidden = 1";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var row = new object[1];
                    if (reader.GetValues(row) > 0)
                    {
                        var appName = row[0].ToString();
                        hiddenApplications.Add(appName);
                    }
                }
            }
            return hiddenApplications;
        }

        public List<string> HiddenAppVersions(string product, string application)
        {
            var hiddenAppVersions = new List<string>();

            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = "select Major + '.' + Minor + '.' + Build + '.' + Revision as Version "
                                  + "from AppVersion v inner join Application a "
                                  + "on a.PK_Application_Id = v.FK_Application_Id "
                                  + "and a.Product = @product and a.Application = @application and v.Hidden = 1 ";

                AddParameter(cmd, "@product", product);
                AddParameter(cmd, "@application", application);
 
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var row = new object[1];
                    if (reader.GetValues(row) <= 0)
                    {
                        //TODO Log error
                        continue;
                    }
                    var version = row[0].ToString();
                    hiddenAppVersions.Add(version);
                }
            }
            return hiddenAppVersions;
        }
    }
}
