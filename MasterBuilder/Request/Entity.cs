using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Entity
    {
        public string InternalName { get; set; }

        public IEnumerable<Property> Properties { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }

        internal string InternalNamePlural
        {
            get
            {
                // TODO: improve plurlisation
                return $"{InternalName}s";
            }
        }
        internal string QualifiedInternalName { get; set; }

        internal bool Validate()
        {
            // TODO:
            // must have Id Entity
            // Internal Name must be valid
            // Id must not be empty

            return true;
        }
    }
}
