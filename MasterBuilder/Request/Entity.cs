using Humanizer;
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

        private string _internalNamePlural = null;
        internal string InternalNamePlural
        {
            get
            {
                if (_internalNamePlural == null)
                {
                    _internalNamePlural = InternalName.Pluralize();
                }
                return _internalNamePlural;
            }
        }
        internal string QualifiedInternalName { get; set; }

        internal bool Validate(out string messages)
        {
            // TODO:
            // must have Id Entity
            // Internal Name must be valid
            // Id must not be empty
            messages = "Success";
            return true;
        }
    }
}
