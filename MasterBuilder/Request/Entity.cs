using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Entity
    {
        public string InternalName { get; set; }

        public IEnumerable<Property> Properties { get; set; }
        internal string QualifiedInternalName { get; set; }
    }
}
