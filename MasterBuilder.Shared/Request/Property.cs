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
        internal static readonly Dictionary<Guid, PropertyType> PropertyTypeDictonary = new Dictionary<Guid, PropertyType>
        {
            { new Guid("{2C1D2E2A-3531-41D9-90D3-3632C368B12A}"), PropertyType.Boolean },
            { new Guid("{25E3A798-5F63-4A1E-93B3-A0BCE69836BC}"), PropertyType.DateTime },
            { new Guid("{F126388B-8A6E-41DB-A98A-A0E511016441}"), PropertyType.Integer },
            { new Guid("{B967BF8D-0722-43F0-9945-CBEB4160822F}"), PropertyType.Double },
            { new Guid("{8BB0B472-E8C4-4DCF-9EF4-FFA088B5A175}"), PropertyType.ParentRelationshipOneToMany },
            { new Guid("{B42A437F-3DED-4B5F-A573-1CCEC1B2D58E}"), PropertyType.ReferenceRelationship },
            { new Guid("{7028DE7D-85DF-4116-8A9A-C565AFD5CE49}"), PropertyType.ParentRelationshipOneToOne },
            { new Guid("{A05F5788-04C3-487D-92F1-A755C73230D4}"), PropertyType.String },
            { new Guid("{4247CAB3-DA47-4921-81B4-1DFF78909859}"), PropertyType.PrimaryKey },
            { new Guid("{A3189B37-FC2D-417B-868F-61D52F4D06DC}"), PropertyType.Uniqueidentifier },
            { new Guid("{D709C3C3-E163-4C3A-A422-65DBFC87F037}"), PropertyType.Spatial },
        };

        /// <summary>
        /// Register of all Property Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, PropertyTemplate> PropertyTemplateDictonary = new Dictionary<Guid, PropertyTemplate>
        {
            { Guid.Empty, PropertyTemplate.None },
            { new Guid("{1B966A14-45B9-4E34-92BB-E2D46D97C5C3}"), PropertyTemplate.ReferenceTitle }
        };

        /// <summary>
        /// Uniqueidentifier of an Entity
        /// </summary>
        [Required]
        [NonDefault]
        public Guid Id { get; set; }
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
        public Guid PropertyTypeId { get; set; }

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
                if (PropertyTypeId == Guid.Empty)
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
        public Guid? PropertyTemplateId { get; set; }

        /// <summary>
        /// Property Template Enum
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public PropertyTemplate PropertyTemplate
        {
            get
            {
                var id = PropertyTemplateId ?? Guid.Empty;
                return PropertyTemplateDictonary[id];
            }
            set
            {
                var id = PropertyTemplateDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == Guid.Empty)
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
        public Guid? ParentEntityId { get; set; }

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
                    return "Guid";
                case PropertyType.ParentRelationshipOneToMany:
                case PropertyType.ReferenceRelationship:
                    return $"Guid{(!required ? "?" : string.Empty)}";
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
        public Entity ParentEntity { get; set; }

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
