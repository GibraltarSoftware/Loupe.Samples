using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Gibraltar.Analyst.AddIn;

namespace Gibraltar.AddIn.FindByUser
{
    public class FindByUserDatabase : IDisposable
    {
        public static FindByUserDatabase GetDatabase(IRepositoryAddInContext context)
        {
            var config = context.Configuration.Machine as FindByUserConfiguration;
            if (config == null)
                return null;

            FindByUserDatabase db = null;
            try
            {
                DbProviderFactory factory;
                switch (config.DatabaseProvider)
                {
                    case "VistaDB":
                        factory = CreateVistaDbProviderFactory();
                        break;

                    default:
                        factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                        break;
                }
                db = new FindByUserDatabase(context, factory, config.ConnectionString);
                DatabaseUnavailable = false;
            }
            catch (Exception ex)
            {
                // Only log the first time we fail until the user edits config
                if (!DatabaseUnavailable)
                {
                    DatabaseUnavailable = true;
                    context.Log.Error(ex, FindByUserAddIn.LogCategory, "Could not open database",
                        config.ConnectionString);
                }
            }

            return db;
        }

        /// <summary>
        /// Get a DbProviderFactory for VistaDB
        /// </summary>
        /// <remarks>
        /// An instance of the VistaDB embedded database engine ships with Loupe Desktop and Loupe Server.
        /// To ensure an easy complile-and-go experience for you while trying out this sample, we will use
        /// that instance regardless of whether or not VistaDB is installed on the local machine and
        /// registered as a database provider.
        /// </remarks>
        private static DbProviderFactory CreateVistaDbProviderFactory()
        {
            // Load the version of VistaDB that ships with Loupe
            var type = Type.GetType("VistaDB.Provider.VistaDBProviderFactory, VistaDB.4, Version=4.1.0.0");
            if (type == null)
                throw new ApplicationException("Could not find VistaDBProviderFactory");

            // Create an instance through a protected constructor
            var constructor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {},
                null);

            if (constructor == null)
                throw new ApplicationException("Could not create an instance of VistaDBProviderFactory");

            return (DbProviderFactory) constructor.Invoke(new object[] {});
        }

        public static bool DatabaseUnavailable { get; set; }

        private DbProviderFactory ProviderFactory { get; set; }
        private string ConnectionString { get; set; }
        private DbConnection Connection { get; set; }
        private IRepositoryAddInContext Context { get; set; }

        private FindByUserDatabase(IRepositoryAddInContext context, DbProviderFactory providerFactory, string connectionString)
        {
            Context = context;
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

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection = null;
            }
        }

        public void AddUsers(Guid sessionId, DateTime sessionDate, IEnumerable<string> users)
        {
            try
            {
                // Check if we already have a row for this session
                var sessionKey = 0;
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = "select Id from dbo.Session where "
                                          + "SessionId = @sessionId and SessionDate = @sessionDate";
                    AddParameter(command, "@sessionId", sessionId);
                    AddParameter(command, "@sessionDate", sessionDate);

                    var key = command.ExecuteScalar();
                    if (key != null)
                        sessionKey = Convert.ToInt32(key);
                }

                // If not, add one now
                if (sessionKey == 0)
                {
                    using (var command = Connection.CreateCommand())
                    {
                        command.CommandText = "insert into dbo.Session (SessionId, SessionDate) values "
                                              + "(@sessionId, @sessionDate); select @@identity";
                        AddParameter(command, "@sessionId", sessionId);
                        AddParameter(command, "@sessionDate", sessionDate);
                        var key = command.ExecuteScalar();
                        if (key != null)
                            sessionKey = Convert.ToInt32(key);
                    }
                }

                // If we can't find or add a session, something's wrong so abandon
                if (sessionKey == 0)
                    return;

                // Next, we will add rows for each user
                foreach (var user in users)
                {
                    var userKey = 0;

                    // First check if we've seen this username before
                    using (var command = Connection.CreateCommand())
                    {
                        command.CommandText = "select id from dbo.Username where Username = @username";
                        AddParameter(command, "@username", user);
                        var key = command.ExecuteScalar();
                        if (key != null)
                            userKey = Convert.ToInt32(key);
                    }

                    // If not, add the user
                    if (userKey == 0)
                    {
                        using (var command = Connection.CreateCommand())
                        {
                            command.CommandText = "insert into dbo.Username (Username) values "
                                                  + "(@username); select @@identity";
                            AddParameter(command, "@username", user);
                            var key = command.ExecuteScalar();
                            if (key != null)
                                userKey = Convert.ToInt32(key);
                        }
                    }

                    // If we can't find or add the user, something's wrong so abandon
                    if (userKey == 0)
                        return;

                    // Next check if we've already associated this user with this session.
                    // This could happen because a session may contain multiple fragments.
                    using (var command = Connection.CreateCommand())
                    {
                        command.CommandText = "select 1 from dbo.UserSession where "
                                              + "Session_FK = @sessionKey and User_FK = @userKey";
                        AddParameter(command, "@sessionKey", sessionKey);
                        AddParameter(command, "@userKey", userKey);
                        var count = command.ExecuteScalar();

                        // If the user is already associated with this session,
                        // there's nothing more to do for this user
                        if (count != null && Convert.ToInt32(count) > 0)
                            continue;
                    }

                    // Finally, associate the current user with the current session
                    using (var command = Connection.CreateCommand())
                    {
                        command.CommandText = "insert into dbo.UserSession(Session_FK, User_FK) values "
                                              + "(@sessionKey, @userKey);";
                        AddParameter(command, "@sessionKey", sessionKey);
                        AddParameter(command, "@userKey", userKey);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                DatabaseUnavailable = true;
                Context.Log.RecordException(ex, FindByUserAddIn.LogCategory, true);
            }
        }

        public List<string> LoadUsernames(int dataRetentionDays)
        {
            var users = new List<string>();
            try
            {
                PruneData(dataRetentionDays);

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = "select Username from dbo.Username order by Username";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new object[1];
                        if (reader.GetValues(row) > 0)
                        {
                            users.Add((string) row[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DatabaseUnavailable = true;
                Context.Log.RecordException(ex, FindByUserAddIn.LogCategory, true);
            }

            return users;
        }

        private void PruneData(int dataRetentionDays)
        {
            var maxSessionDate = DateTime.Now.Date.AddDays(-dataRetentionDays);

            // Cascading delete will prune related UserSession rows
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "delete from dbo.Session where SessionDate < @sessionDate";
                AddParameter(command, "@sessionDate", maxSessionDate);

                command.ExecuteNonQuery();
            }

            // This will prune orphaned Username rows
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "delete from dbo.Username where Id not in "
                                      + "(select distinct User_FK from UserSession)";
                command.ExecuteNonQuery();
            }
        }

        public HashSet<Guid> FindSessions(string username, DateTimeOffset? sessionDate)
        {
            var sessionIds = new HashSet<Guid>();

            try
            {
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = "select distinct s.SessionId from dbo.Username u "
                                          + "inner join dbo.UserSession us on u.Id = us.User_FK "
                                          + "inner join dbo.Session s on s.Id = us.Session_FK ";

                    if (username != null && sessionDate.HasValue)
                    {
                        command.CommandText += "where u.Username = @username and s.SessionDate = @sessionDate";
                        AddParameter(command, "@username", username);
                        AddParameter(command, "@sessionDate", sessionDate.Value.Date);
                    }
                    else if (username != null)
                    {
                        command.CommandText += "where u.Username = @username";
                        AddParameter(command, "@username", username);
                    }
                    else if (sessionDate.HasValue)
                    {
                        command.CommandText += "where s.SessionDate = @sessionDate";
                        AddParameter(command, "@sessionDate", sessionDate.Value.Date);
                    }

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new object[1];
                        if (reader.GetValues(row) > 0)
                        {
                            var id = (Guid) row[0];
                            sessionIds.Add(id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DatabaseUnavailable = true;
                Context.Log.RecordException(ex, FindByUserAddIn.LogCategory, true);
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

        private void AddParameter(DbCommand command, string name, DateTime value)
        {
            var param = command.CreateParameter();
            param.DbType = DbType.DateTime;
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
    }
}
