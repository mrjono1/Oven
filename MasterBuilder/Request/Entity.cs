using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        /// Optional: Contains seed data and settings
        /// </summary>
        public Seed Seed { get; set; }

        /// <summary>
        /// Validate this object and reslove any issues if possible
        /// </summary>
        /// <param name="message">Issue messages</param>
        /// <returns>'true' for success or 'false' for failing to validate</returns>
        internal bool Validate(out string message)
        {
            var messageList = new List<string>();

            // Id must not be empty
            if (Id == Guid.Empty)
            {
                messageList.Add("Entity must have an Id");
            }
            
            // Title
            if (string.IsNullOrWhiteSpace(Title))
            {
                messageList.Add("Entity must have a title");
            }

            // Internal Name must be valid
            if (!Regex.IsMatch(InternalName, @"^[a-zA-Z]+$"))
            {
                messageList.Add("Entity Internal Name must only contain letters");
            }
            // Ensure Internal Name is standard caseing
            InternalName = InternalName.Pascalize();

            // TODO: move validation to model validation

            if (Properties != null)
            {
                foreach (var property in Properties)
                {
                    if (!property.Validate(out string propertyMessage)) {
                        messageList.Add(propertyMessage);
                    }
                }
            }

            if (messageList.Any())
            {
                message = string.Join(", ", messageList);
                return false;
            }
            else
            {
                message = "Success";
                return true;
            }
        }
    }
}
