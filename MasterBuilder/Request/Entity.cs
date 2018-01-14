using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;

namespace MasterBuilder.Request
{
    /// <summary>
    /// The configuration for an entity
    /// </summary>
    public partial class Entity
    {
        /// <summary>
        /// Register of all Entity Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, EntityTemplateEnum> EntityTemplateDictonary = new Dictionary<Guid, EntityTemplateEnum>
        {
            { Guid.Empty, EntityTemplateEnum.None },
            { new Guid("{B79D1C90-6320-4A07-9753-2A41110611C8}"), EntityTemplateEnum.Reference }
        };

        /// <summary>
        /// Uniqueidentifier of an Entity
        /// </summary>
        [RequiredNonDefault]
        public Guid Id { get; set; }

        /// <summary>
        /// Title of and Entity
        /// </summary>
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// Internal name of an Entity
        /// </summary>
        [MinLength(2)]
        [MaxLength(100)]
        [PascalString]
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
        /// Optional: Entity Template Id, if specified it will define default behaviours for this entity
        /// </summary>
        public Guid? EntityTemplateId { get; set; }
        /// <summary>
        /// Entity Template
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public EntityTemplateEnum EntityTemplate
        {
            get
            {
                var id = EntityTemplateId ?? Guid.Empty;
                return EntityTemplateDictonary[id];
            }
            set
            {
                var id = EntityTemplateDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == Guid.Empty)
                {
                    EntityTemplateId = null;
                }
                else
                {
                    EntityTemplateId = id;
                }
            }
        }
    }
}
