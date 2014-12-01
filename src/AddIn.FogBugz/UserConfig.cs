using System.Xml.Serialization;

namespace Gibraltar.AddIn.FogBugz
{
    /// <summary>
    /// Configuration specific to a single user on a computer
    /// </summary>
    [XmlRoot("fogBugzUserConfig")]
    public class UserConfig
    {
        public UserConfig()
        {
            //apply some handy defaults
            CaseStatusFilter = CaseStatus.Active;
            LastUpdatedFilter = LastUpdatedFilter.OneYear;
        }

        /// <summary>
        /// Indicates of the hub should perform automatic session analysis or not
        /// </summary>
        [XmlAttribute("enableAutomaticAnalysis")]
        public bool EnableAutomaticAnalysis { get; set; }

        /// <summary>
        /// Only look at sessions with the specified status
        /// </summary>
        [XmlAttribute("caseStatusFilter")]
        public CaseStatus CaseStatusFilter { get; set; }

        /// <summary>
        /// Only look at sessions that were updated within the last interval
        /// </summary>
        [XmlAttribute("lastUpdatedFilter")]
        public LastUpdatedFilter LastUpdatedFilter { get; set; }
    }
}
