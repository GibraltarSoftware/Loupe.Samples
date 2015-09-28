using System;
using System.Xml.Serialization;

namespace Loupe.Extension.FogBugz
{
    /// <summary>
    /// Part of the config that defines a single mapping from
    /// a set of Loupe sessions to a FogBugz project
    /// </summary>
    [Serializable]
    [XmlRoot("mapping")]
    public class Mapping
    {
        private const bool IgnoreCase = true;

        public Mapping()
        {
        }

        public Mapping(string product, string application, string versions, string project, string area, int priority)
        {
            Product = product;
            Application = application;
            Versions = versions;
            Project = project;
            Area = area;
            Priority = priority;
        }

        /// <summary>
        /// Must match Product reported to Loupe for issues to be reported to FogBugz or null to match all products.
        /// </summary>
        [XmlAttribute("product")]
        public string Product { get; set; }

        /// <summary>
        /// Must match Application reported to Loupe for issues to be reported to FogBugz or null to match all applications.
        /// </summary>
        [XmlAttribute("application")]
        public string Application { get; set; }

        /// <summary>
        /// Pattern specifying application versions for issues to be reported to FogBugz.  If null, all versions match.
        /// </summary>
        /// <example>
        /// 4                   // matches any version 4
        /// 4.*.*.*             // matches any version 4
        /// 4.1                 // matches any version 4.1
        /// 4.1.*.3             // matches 4.1 only when last component is 3
        /// *.*.99.*            // matches build 99 regardless of other components
        /// </example>
        [XmlAttribute("versions")]
        public string Versions { get; set; }


        /// <summary>
        /// FogBugz Project for reported issues
        /// </summary>
        [XmlAttribute("project")]
        public string Project { get; set; }

        /// <summary>
        /// FogBugz Area for reported issues
        /// </summary>
        [XmlAttribute("area")]
        public string Area { get; set; }

        /// <summary>
        /// Initial priority for reported issues
        /// </summary>
        [XmlAttribute("priority")]
        public int Priority { get; set; }

        /// <summary>
        /// Determines if this mapping is a match for the product / application / version information provided.
        /// </summary>
        /// <returns></returns>
        public bool IsMatch(string product, string application, string versions)
        {
            if (!string.IsNullOrEmpty(Product)
                && !string.IsNullOrEmpty(product)
                && string.Compare(Product, product, IgnoreCase) != 0)
                return false;

            if (!string.IsNullOrEmpty(Application)
                && !string.IsNullOrEmpty(application)
                && string.Compare(Application, application, IgnoreCase) != 0)
                return false;

            return VersionHelper.Equivalent(Versions, versions);
        }
    }
}
