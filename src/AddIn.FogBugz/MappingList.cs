using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gibraltar.AddIn.FogBugz
{
    /// <summary>
    /// Collecting of Mapping objects
    /// </summary>
    [Serializable]
    public class MappingList : BindingList<Mapping>
    {
        public void Add(string product, string application, string versions, string project, string area, int priority)
        {
            Mapping newMapping = new Mapping(product, application, versions, project, area, priority);
            Add(newMapping);
        }

        public Mapping FindMatch(string product, string application, string versions)
        {
            foreach (Mapping mapping in this)
            {
                if (mapping.IsMatch(product, application, versions))
                    return mapping;
            }
            return null;
        }
    }
}
