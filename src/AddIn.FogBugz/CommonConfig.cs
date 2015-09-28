using System;
using System.Xml.Serialization;
using Loupe.Extensibility.Data;

namespace Loupe.Extension.FogBugz
{
    /// <summary>
    /// FogBugzConfig is the serializable configuration data that is read & written to a file
    /// in the same folder as the addin.  
    /// </summary>
    [Serializable]
    [XmlRoot("fogBugzCommonConfig")]
    public class CommonConfig
    {
        [XmlElement("url")]
        public string Url { get; set; }

        public string BaseUrl { get { return Url.Substring(0, Url.LastIndexOf("/")); } }

        [XmlArray("mappings")]
        public MappingList Mappings { get; set; }

        /// <summary>
        /// Returns the mapping appropriate for a session, if any.
        /// Otherwise, return null.
        /// </summary>
        public Mapping FindTarget(ISession session)
        {
            string product = session.Summary.Product;
            string application = session.Summary.Application;
            string version = session.Summary.ApplicationVersion.ToString();

            return Mappings.FindMatch(product,application, version);
        }
    }
}
