using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace Gibraltar.AddIn.FogBugz.Internal
{
    /// <summary>
    /// This holds the information for one case in a FogBugzCaseList
    /// </summary>
    [DebuggerDisplay("{Caption}")]
    internal class FogBugzCaseInfo
    {
        public string Caption
        {
            get
            {
                string caption = string.Format("{0} ({1})", CaseId == 0 ? "All" : CaseId.ToString(), Sessions.Count);
                return caption;
            }
        }

        public int CaseId { get; set; }
        public List<Guid> Sessions { get; private set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public DateTime LastUpdated { get; set; }

        public FogBugzCaseInfo ()
        {
            Sessions = new List<Guid>();
        }

        public FogBugzCaseInfo(XmlNode caseNode)
        {
            Sessions = new List<Guid>();
            Import(caseNode);
        }

        public void Import(XmlNode caseNode)
        {
            XmlNode idNode = caseNode.SelectSingleNode("./ixBug");
            CaseId = int.Parse(idNode.InnerText);

            XmlNode statusNode = caseNode.SelectSingleNode("./sStatus");
            Status = statusNode.InnerText;

            XmlNode titleNode = caseNode.SelectSingleNode("./sTitle");
            Title = titleNode.InnerText;

            XmlNode lastUpdatedNode = caseNode.SelectSingleNode("./dtLastUpdated");
            LastUpdated = DateTime.Parse(lastUpdatedNode.InnerText);
        }
    }
}
