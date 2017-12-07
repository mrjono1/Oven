using Humanizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// The configuration for an entity
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Uniqueidentifier of an Entity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of and Entity
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Internal name of an Entity
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// List of Properties
        /// </summary>
        public IEnumerable<Property> Properties { get; set; }

        /// <summary>
        /// Cached version of the internal name pluralised for use in collections
        /// </summary>
        private string _internalNamePlural = null;
        /// <summary>
        /// Pluralised version of the internal name for use in collections
        /// </summary>
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
        
        /// <summary>
        /// JSON Array of JSON objects that represent seed data for this entity
        /// </summary>
        public string SeedData { get; set; }

        /// <summary>
        /// Validate this object and reslove any issues if possible
        /// </summary>
        /// <param name="messages">Issue messages</param>
        /// <returns>'true' for success or 'false' for failing to validate</returns>
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
