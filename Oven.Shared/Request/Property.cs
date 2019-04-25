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
    /// Property
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Property: {Title}")]
    public partial class Property
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Property() { }
        /// <summary>
        /// Constructor
        /// </summary>
        public Property(string internalName)
        {
            InternalName = internalName;
        }
        /// <summary>
        /// Register of all Property Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, PropertyType> PropertyTypeDictonary = new Dictionary<ObjectId, PropertyType>
        {
            { new ObjectId("5ca86b146668b25914b67e74"), PropertyType.Boolean },
            { new ObjectId("5ca86b266668b25914b67e75"), PropertyType.DateTime },
            { new ObjectId("5ca86b506668b25914b67e76"), PropertyType.Integer },
            { new ObjectId("5ca86b656668b25914b67e77"), PropertyType.Double },
            { new ObjectId("5ca86c626668b25914b67e78"), PropertyType.ParentRelationshipOneToMany },
            { new ObjectId("5ca86c956668b25914b67e79"), PropertyType.ReferenceRelationship },
            { new ObjectId("5ca877064a73264e4c06dfda"), PropertyType.ParentRelationshipOneToOne },
            { new ObjectId("5ca877044a73264e4c06dfc1"), PropertyType.String },
            { new ObjectId("5ca877094a73264e4c06dff8"), PropertyType.PrimaryKey },
            { new ObjectId("5ca8770e4a73264e4c06e030"), PropertyType.Uniqueidentifier },
            { new ObjectId("5ca8770e4a73264e4c06e031"), PropertyType.Spatial },
        };

        /// <summary>
        /// Register of all Property Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, PropertyTemplate> PropertyTemplateDictonary = new Dictionary<ObjectId, PropertyTemplate>
        {
            { ObjectId.Empty, PropertyTemplate.None },
            { new ObjectId("5ca877094a73264e4c06dff5"), PropertyTemplate.ReferenceTitle }
        };

        /// <summary>
        /// Uniqueidentifier of an Entity
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Title { get; set; }
        /// <summary>
        /// Internal Name
        /// </summary>
        [MinLength(2)]
        [MaxLength(100)]
        [PascalString]
        public string InternalName { get; set; }

        /// <summary>
        /// Property Type Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId PropertyTypeId { get; set; }

        //TODO: Change this property to PropertyType
        /// <summary>
        /// Property Type Enum
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public PropertyType PropertyType
        {
            get
            {
                if (PropertyTypeId == ObjectId.Empty)
                {
                    return PropertyType.String;
                }
                return PropertyTypeDictonary[PropertyTypeId];
            }
            set
            {
                PropertyTypeId = PropertyTypeDictonary.SingleOrDefault(v => v.Value == value).Key;
            }
        }

        /// <summary>
        /// Property Template Identifier, setting this will case future upgrades to update this field
        /// </summary>
        [NonDefault]
        public ObjectId? PropertyTemplateId { get; set; }

        /// <summary>
        /// Property Template Enum
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public PropertyTemplate PropertyTemplate
        {
            get
            {
                var id = PropertyTemplateId ?? ObjectId.Empty;
                return PropertyTemplateDictonary[id];
            }
            set
            {
                var id = PropertyTemplateDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == ObjectId.Empty)
                {
                    PropertyTemplateId = null;
                }
                else
                {
                    PropertyTemplateId = id;
                }
            }
        }

        /// <summary>
        /// Parent Entity Identifier
        /// </summary>
        [NonDefault]
        public ObjectId? ReferenceEntityId { get; set; }

        /// <summary>
        /// Helper Function to get Required
        /// </summary>
        [JsonIgnore]
        public bool Required
        {
            get
            {
                var required = false;
                if (ValidationItems != null)
                {
                    required = ValidationItems.Where(v => v.ValidationType == ValidationType.Required).Any();
                }
                return required;
            }
        }
        /// <summary>
        /// C# Data Type
        /// </summary>
        [JsonIgnore]
        public string CsType
        {
            get
            {
                return GetTypeCs(Required);
            }
        }

        public string GetTypeCs(bool required)
        {
            switch (PropertyType)
            {
                case PropertyType.PrimaryKey:
                    return "ObjectId";
                case PropertyType.ParentRelationshipOneToMany:
                case PropertyType.ReferenceRelationship:
                    return $"ObjectId{(!required ? "?" : string.Empty)}";
                case PropertyType.String:
                    return "string";
                case PropertyType.Integer:
                    return $"int{(!required ? "?" : string.Empty)}";
                case PropertyType.Double:
                    return $"double{(!required ? "?" : string.Empty)}";
                case PropertyType.DateTime:
                    return "DateTime";
                case PropertyType.Boolean:
                    return $"bool{(!required ? "?" : string.Empty)}";
                case PropertyType.Spatial:
                    return $"string";
                default:
                    return "string";
            }
        }

        /// <summary>
        /// Type Script Data Type
        /// </summary>
        [JsonIgnore]
        public string TsType
        {
            get
            {
                switch (PropertyType)
                {
                    case PropertyType.PrimaryKey:
                        return "string";
                    case PropertyType.String:
                        return "string";
                    case PropertyType.Integer:
                        return "number";
                    case PropertyType.Double:
                        return "number";
                    case PropertyType.DateTime:
                        return "string";
                    case PropertyType.Boolean:
                        return "boolean";
                    default:
                        return "string";
                }
            }
        }

        /// <summary>
        /// Internal Name Type Script
        /// </summary>
        [JsonIgnore]
        public string InternalNameJavaScript
        {
            get
            {
                switch (PropertyType)
                {
                    case PropertyType.ParentRelationshipOneToMany:
                    case PropertyType.ReferenceRelationship:
                        return $"{InternalName}Id".Camelize();
                    default:
                        return InternalName.Camelize();
                }
            }
        }

        /// <summary>
        /// Validation Items
        /// </summary>
        public IEnumerable<Validation> ValidationItems { get; set; }
        /// <summary>
        /// Default String Value
        /// </summary>
        public string DefaultStringValue { get; set; }
        /// <summary>
        /// Default Integer Value
        /// </summary>
        public int? DefaultIntegerValue { get; set; }
        /// <summary>
        /// Default Double Value
        /// </summary>
        public double? DefaultDoubleValue { get; set; }
        /// <summary>
        /// Default Boolean Value
        /// </summary>
        public bool? DefaultBooleanValue { get; set; }
        /// <summary>
        /// Server Side Filter Expression
        /// </summary>
        public Expression FilterExpression { get; set; }

        #region Internal Helper Properties
        /// <summary>
        /// The entity that this property is on
        /// </summary>
        [JsonIgnore]
        public Entity Entity { get; set; }
        /// <summary>
        /// Parent Entity
        /// </summary>
        [JsonIgnore]
        public Entity ReferenceEntity { get; set; }

        /// <summary>
        /// Internal Name C#
        /// </summary>
        [JsonIgnore]
        public string InternalNameCSharp
        {
            get
            {
                switch (PropertyType)
                {
                    case PropertyType.ParentRelationshipOneToMany:
                    case PropertyType.ReferenceRelationship:
                        return $"{InternalName}Id";
                    default:
                        return InternalName;
                }
            }
        }
        #endregion
    }
}
