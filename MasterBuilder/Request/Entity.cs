using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Entity
    {
        public string InternalName { get; set; }

        public IEnumerable<Property> Properties { get; set; }
        public string Title { get; internal set; }
        internal string InternalNamePlural
        {
            get
            {
                // TODO: improve plurlisation
                return $"{InternalName}s";
            }
        }
        internal string QualifiedInternalName { get; set; }
    }
}
