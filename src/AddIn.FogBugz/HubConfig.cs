using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gibraltar.AddIn.FogBugz
{
    /// <summary>
    /// Configuration specific to FogBugz Hub integration
    /// </summary>
    [XmlRoot("fogBugzHubConfig")]
    public class HubConfig
    {
        /// <summary>
        /// Indicates of the hub should perform automatic session analysis or not
        /// </summary>
        [XmlAttribute("enableAutomaticAnalysis")]
        public bool EnableAutomaticAnalysis { get; set; }
    }
}
