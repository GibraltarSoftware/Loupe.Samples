using System;
using System.Collections.Generic;
using System.Xml;

namespace Gibraltar.AddIn.FogBugz.Internal
{
    /// <summary>
    /// Collection of FogBugzCaseInfo for the FogBugzSummaryView
    /// </summary>
    internal class FogBugzCaseList
    {
        private readonly AddInController m_Controller;
        public List<FogBugzCaseInfo> Cases { get; private set; }
       
        /// <summary>
        /// Create a collection pre-loaded with a list of FogBugz cases matching the filter string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="filter">The filter string is created by FogBugzFilter</param>
        public FogBugzCaseList(AddInController controller, string filter)
        {
            m_Controller = controller;
            Load(filter);
        }

        /// <summary>
        /// Load a list of FogBugz cases matching the filter string
        /// </summary>
        /// <param name="filter">The filter string is created by FogBugzFilter</param>
        public void Load(string filter)
        {
            Load(m_Controller.GetApi(), filter);
        }

        /// <summary>
        /// Load a list of FogBugz cases matching the filter string
        /// </summary>
        /// <param name="api">api is created by FBApi</param>
        /// <param name="filter">The filter string is created by FogBugzFilter</param>
        public void Load(FBApi api, string filter)
        {
            string query = "tag:Gibraltar " + filter;

            Dictionary<string, string> queryArgs = new Dictionary<string, string>
                                {
                                    {"q", query},
                                    {"cols", "ixBug,sTitle,fOpen,sStatus,dtLastUpdated,events"}
                                };

            // Get the list of candidate FogBugz cases
            string queryResults = api.Cmd("search", queryArgs);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(queryResults);
            XmlNodeList caseList = xml.SelectNodes("/response/cases/case");
            if (caseList == null)
                return;

            Cases = new List<FogBugzCaseInfo>();

            foreach (XmlNode caseNode in caseList)
            {
                // It shouldn't happen that no events exist, but, if so, ignore that case
                XmlNodeList eventList = caseNode.SelectNodes("./events/event/s");
                if (eventList == null)
                    continue;

                FogBugzCaseInfo caseInfo = new FogBugzCaseInfo(caseNode);

                // Search every event
                foreach (XmlNode eventNode in eventList)
                {
                    // Search every line of each event looking for session guids
                    string text = eventNode.InnerText;
                    string[] lines = text.Split('\n');
                    foreach (string line in lines)
                    {
                        if (line.StartsWith(LogMessageFormatter.SessionIdPrefix))
                        {
                            string sessionId = line.Substring(LogMessageFormatter.SessionIdPrefix.Length);
                            Guid guid = new Guid(sessionId);
                            if (!caseInfo.Sessions.Contains(guid))
                                caseInfo.Sessions.Add(guid);
                        }
                    }
                }

                if (caseInfo.Sessions.Count > 0)
                    Cases.Add(caseInfo);
            }

            // Sort in descending case id order with the default entry at the top of the list
            Cases.Sort((c1, c2) => c2.CaseId - c1.CaseId);
        }
    }
}
