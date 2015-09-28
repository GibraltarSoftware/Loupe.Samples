using System;
using System.Collections.Generic;
using System.Xml;

namespace Loupe.Extension.FogBugz.Internal
{
    internal class FogBugzCaseReader
    {
        private readonly RepositoryController m_Controller;
        public int CaseId { get; private set; }
        public List<Guid> SessionIds { get; private set; }
        public string Title { get; private set; }
        public string Project { get; private set; }
        public string Area { get; private set; }
        public string Status { get; private set; }
        public string Priority { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public string LastUpdatedBy { get; private set; }
        public string LatestSummary { get; private set; }

        public FogBugzCaseReader(RepositoryController controller, int caseId)
        {
            m_Controller = controller;
            Load(m_Controller.GetApi(), caseId);
        }

        private void Load(FBApi api, int caseId)
        {
            Dictionary<string, string> queryArgs = new Dictionary<string, string>
                                {
                                    {"q", caseId.ToString()},
                                    {"cols", "ixBug,sTitle,sProject,sArea,sStatus,sPriority,events"}
                                };

            // Get the list of candidate FogBugz cases
            string queryResults = api.Cmd("search", queryArgs);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(queryResults);
            XmlNodeList caseList = xml.SelectNodes("/response/cases/case");

            CaseId = caseId;
            Project = string.Empty;
            Area = string.Empty;
            Status = string.Empty;
            Priority = string.Empty;
            LastUpdated = DateTime.MinValue;
            SessionIds = new List<Guid>();

            if (caseList == null || caseList.Count <= 0)
                return;

            XmlNode caseNode = caseList[0];
            // It shouldn't happen that no events exist, but, if so, ignore that case
            XmlNodeList eventList = caseNode.SelectNodes("./events/event");
            if (eventList == null || eventList.Count <= 0)
                return;

            Project = caseNode.SelectSingleNode("./sProject").InnerText;
            Area = caseNode.SelectSingleNode("./sArea").InnerText;
            Title = caseNode.SelectSingleNode("./sTitle").InnerText;
            Status = caseNode.SelectSingleNode("./sStatus").InnerText;
            Priority = caseNode.SelectSingleNode("./sPriority").InnerText;

            XmlNode lastEvent = eventList[eventList.Count - 1];
            LastUpdated = DateTime.Parse(lastEvent.SelectSingleNode("./dt").InnerText);
            LastUpdatedBy = lastEvent.SelectSingleNode("./sVerb").InnerText;

            foreach (XmlNode eventNode in eventList)
            {
                string text = eventNode.SelectSingleNode("./s").InnerText;

                // Skip events with no text change
                if (string.IsNullOrEmpty(text))
                    continue;

                // Avoid showing SessionIdPrefix
                string rawText = eventNode.SelectSingleNode("./s").InnerText;
                int maxLength = rawText.LastIndexOf(LogMessageFormatter.SessionIdPrefix);
                LatestSummary = maxLength > 0 ? rawText.Substring(0, maxLength) : rawText;

                string[] lines = text.Split('\n');
                foreach (string line in lines)
                {
                    if (line.StartsWith(LogMessageFormatter.SessionIdPrefix))
                    {
                        string sessionId = line.Substring(LogMessageFormatter.SessionIdPrefix.Length);
                        Guid guid = new Guid(sessionId);
                        if (!SessionIds.Contains(guid))
                            SessionIds.Add(guid);
                    }
                }
            }
        }
    }
}