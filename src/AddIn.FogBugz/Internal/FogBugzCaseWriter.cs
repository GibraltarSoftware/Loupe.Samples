using System;
using System.Collections.Generic;
using System.Xml;
using Gibraltar.Analyst.AddIn;
using Gibraltar.Analyst.Data;

namespace Gibraltar.AddIn.FogBugz.Internal
{
    /// <summary>
    /// This class encapsulates the details of creating or updating a case in FogBugz
    /// based on the data in an ErrorInfo.
    /// </summary>
    internal class FogBugzCaseWriter
    {
        private const string LogCategory = AddInController.LogCategory +  ".Case Writer";

        public ErrorInfo Error { get; private set; }
        public ILogMessage FirstMessage { get { return Error.Messages[0]; } }
        public ISession Session { get { return FirstMessage.Session; } }
        private ILog Log { get; set; }

        public FogBugzCaseWriter(ILog log, ErrorInfo error)
        {
            Log = log;
            Error = error;
        }

        public void Submit(FBApi api, Mapping target)
        {
            Log.Verbose(LogCategory, "Submitting new or updated error information to server", null);

            // Start by looking for the fingerprint to decide whether we need
            // to create an new case or update an existing one.
            Dictionary<string, string> args = new Dictionary<string, string>
                           {
                               {"sScoutDescription", Error.Fingerprint},
                           };

            string results = api.Cmd("listScoutCase", args);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(results);
            XmlNode caseNode = xml.SelectSingleNode("/response/case/ixBug");

            if (caseNode == null)
            {
                Log.Information(LogCategory, "Submitting as new issue because no existing Scout Case could be found with the fingerprint", "Fingerprint: {0}\r\n\r\nRaw Result:\r\n{1}", Error.Fingerprint, results);
                SubmitNew(api, target);
            }
            else
            {
                Log.Information(LogCategory, "Submitting as an update because an existing Scout Case matched the fingerprint", "Fingerprint: {0}\r\n\r\nRaw Result:\r\n{1}", Error.Fingerprint, results);
                SubmitUpdate(api, caseNode.InnerText);
            }
        }

        /// <summary>
        /// Create a new case with all available details about the error
        /// </summary>
        private void SubmitNew(FBApi api, Mapping target)
        {
            LogMessageFormatter formatter = new LogMessageFormatter(FirstMessage);
            Dictionary<string, string> args = new Dictionary<string, string>
                            {
                                {"sScoutDescription", Error.Fingerprint},
                                {"sProject", target.Project},
                                {"sArea", target.Area},
                                {"ixPriority", target.Priority.ToString()},
                                {"sTitle", formatter.GetTitle()},
                                {"sVersion", Session.Summary.ApplicationVersion.ToString()},
                                {"sEvent", formatter.GetNewEvent(Error)},
                                {"sTags", formatter.GetTags()},
                            };

            string results = api.Cmd("new", args);

            // TODO: Should check for errors here
            Log.Information(LogCategory, "Completed new issue submission", "Scout Description: {0}\r\nProject: {1}\r\nArea: {2}\r\nRaw result: {3}",Error.Fingerprint, target.Project, target.Area, results);
        }

        /// <summary>
        /// Update an existing case
        /// </summary>
        private void SubmitUpdate(FBApi api, string caseId)
        {
            Dictionary<string, string> queryArgs = new Dictionary<string, string>
                                    {
                                        {"q", caseId},
                                        {"cols", "sVersion,tags,events"}
                                    };

            string queryResults = api.Cmd("search", queryArgs);
            Log.Verbose(LogCategory, "Retrieved existing issue information from server", "Query Results:\r\n{0}", queryResults);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(queryResults);
            XmlNodeList eventList = xml.GetElementsByTagName("s");
            string sessionIdMessage = new LogMessageFormatter(FirstMessage).GetSessionIdMessage();

            // If this case already references this session, we're done
            foreach (XmlNode node in eventList)
            {
                if (node.InnerText.Contains(sessionIdMessage))
                    return;
            }

            LogMessageFormatter formatter = new LogMessageFormatter(FirstMessage);
            Dictionary<string, string> updateArgs = new Dictionary<string, string>
                                 {
                                     {"sScoutDescription", Error.Fingerprint},
                                 };

            // Compare the version of the current session with the session associated with the case.
            // The version in FogBugz is the high watermark (highest version seen to-date).
            // If this session if for a 
            XmlNode versionNode = xml.SelectSingleNode("//sVersion");
            Version previousVersion = new Version(versionNode.InnerText);
            if (Session.Summary.ApplicationVersion > previousVersion)
            {
                // If this is a new version, update the high watermark and write
                // a detailed description.
                updateArgs.Add("sVersion", Session.Summary.ApplicationVersion.ToString());
                updateArgs.Add("sEvent", formatter.GetRecurrenceLaterVersionEvent(Error));
                Log.Verbose(LogCategory, "Updating latest version information on existing issue", "This new instance has a higher version number than any previous occurence so we'll update the high water mark on the server.\r\nPrevious Version: {0}\r\nThis Version: {1}", previousVersion, Session.Summary.ApplicationVersion);
            }
            else if (Session.Summary.ApplicationVersion == previousVersion)
            {
                // If this is another instance of the same version, write a short update
                updateArgs.Add("sEvent", formatter.GetRecurrenceSameVersionEvent(Error));
            }
            else
            {
                // If the version of this session is earlier, ignore this session.
                Log.Information(LogCategory, "Skipping reporting new occurrence of issue because its from an old version", "This new instance has a lower version number than one or more previous occurrences, so we wont report it.\r\nPrevious Version: {0}\r\nThis Version: {1}", previousVersion, Session.Summary.ApplicationVersion);
                return;
            }

            // We use the "new" command rather than "edit" because FogBugz has the
            // smarts to reactivate closed cases with scout descriptions.
            string results = api.Cmd("new", updateArgs);

            // TODO: Should check for errors here
            Log.Information(LogCategory, "Completed issue update submission", "Scout Description: {0}\r\nRaw result: {1}", Error.Fingerprint, results);
        }

    }
}
