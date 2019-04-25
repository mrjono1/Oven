using Humanizer;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;

namespace Oven.Request
{
    /// <summary>
    /// The configuration for an entity
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Entity: {Title}")]
    public partial class Entity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Entity() { }
        /// <summary>
        /// Constructor
        /// </summary>
        public Entity(string internalName)
        {
            InternalName = internalName;
        }
        /// <summary>
        /// Register of all Entity Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, EntityTemplate> EntityTemplateDictonary = new Dictionary<ObjectId, EntityTemplate>
        {
            { ObjectId.Empty, EntityTemplate.None },
            { new ObjectId("5ca86a296668b25914b67e6f"), EntityTemplate.Reference }
        };

        /// <summary>
        /// Uniqueidentifier of an Entity
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }

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
        /// Default is blank which is dbo schema
        /// </summary>
        public string DatabaseSchema { get; set; }
        /// <summary>
        /// Cached version of the internal name pluralised for use in collections
        /// </summary>
        private string internalNamePlural = null;
        /// <summary>
        /// Pluralised version of the internal name for use in collections
        /// </summary>
        [JsonIgnore]
        public string InternalNamePlural
        {
            get
            {
                if (internalNamePlural == null)
                {
                    internalNamePlural = InternalName.Pluralize();
                }
                return internalNamePlural;
            }
        }
        
        /// <summary>
        /// Optional: Contains seed data and settings
        /// </summary>
        public Seed Seed { get; set; }
        /// <summary>
        /// Optional: Entity Template Id, if specified it will define default behaviours for this entity
        /// </summary>
        [NonDefault]
        public ObjectId? EntityTemplateId { get; set; }
        /// <summary>
        /// Entity Template
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public EntityTemplate EntityTemplate
        {
            get
            {
                var id = EntityTemplateId ?? ObjectId.Empty;
                return EntityTemplateDictonary[id];
            }
            set
            {
                var id = EntityTemplateDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == ObjectId.Empty)
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
