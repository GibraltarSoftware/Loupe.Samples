using System.Xml.Serialization;

namespace Loupe.Extension.FogBugz
{
    /// <summary>
    /// Configuration specific to FogBugz Hub integration
    /// </summary>
    [XmlRoot("fogBugzHubConfig")]
    public class ServerConfig
    {
        /// <summary>
        /// Indicates of the hub should perform automatic session analysis or not
        /// </summary>
        [XmlAttribute("enableAutomaticAnalysis")]
        public bool EnableAutomaticAnalysis { get; set; }
    }
}
