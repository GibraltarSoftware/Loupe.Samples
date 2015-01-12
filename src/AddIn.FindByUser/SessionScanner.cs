using System;
using System.Collections.Generic;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.FindByUser
{
    internal class SessionScanner : IDisposable
    {
        private readonly ISession _session;
        private FindByUserDatabase _db;

        public SessionScanner(ISession session, IRepositoryAddInContext context)
        {
            _session = session;
            _db = FindByUserDatabase.GetDatabase(context);
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public void Scan()
        {
            if (_db == null || FindByUserDatabase.DatabaseUnavailable)
                return;

            var sessionId = _session.Id;
            var currentDate = new DateTime(2000, 1, 1);
            var users = new HashSet<string>();

            // Iterate across all messages in this session fragment
            foreach (var message in _session.Messages)
            {
                // Handle the case of sessions that span multiple days
                var timestamp = message.Timestamp.ToLocalTime();
                if (timestamp.Date != currentDate)
                {
                    // Update the database with the users associated with this session on this date
                    if (users.Count > 0)
                    {
                        _db.AddUsers(sessionId, currentDate, users);
                        users.Clear();
                    }
                    currentDate = timestamp.Date;
                }
                users.Add(message.UserName);
            }

            // Update the database with the users associated with the last day of this session fragment
            if (users.Count > 0)
            {
                _db.AddUsers(sessionId, currentDate, users);
            }
        }
    }
}