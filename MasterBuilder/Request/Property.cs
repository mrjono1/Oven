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
    public partial class Property
    {
        /// <summary>
        /// Register of all Property Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, PropertyTypeEnum> PropertyTypeDictonary = new Dictionary<Guid, PropertyTypeEnum>
        {
            { new Guid("{2C1D2E2A-3531-41D9-90D3-3632C368B12A}"), PropertyTypeEnum.Boolean },
            { new Guid("{25E3A798-5F63-4A1E-93B3-A0BCE69836BC}"), PropertyTypeEnum.DateTime },
            { new Guid("{F126388B-8A6E-41DB-A98A-A0E511016441}"), PropertyTypeEnum.Integer },
            { new Guid("{B967BF8D-0722-43F0-9945-CBEB4160822F}"), PropertyTypeEnum.Double },
            { new Guid("{8BB0B472-E8C4-4DCF-9EF4-FFA088B5A175}"), PropertyTypeEnum.ParentRelationship },
            { new Guid("{B42A437F-3DED-4B5F-A573-1CCEC1B2D58E}"), PropertyTypeEnum.ReferenceRelationship },
            { new Guid("{7028DE7D-85DF-4116-8A9A-C565AFD5CE49}"), PropertyTypeEnum.OneToOneRelationship },
            { new Guid("{A05F5788-04C3-487D-92F1-A755C73230D4}"), PropertyTypeEnum.String },
            { new Guid("{4247CAB3-DA47-4921-81B4-1DFF78909859}"), PropertyTypeEnum.PrimaryKey }
        };

        /// <summary>
        /// Register of all Property Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, PropertyTemplateEnum> PropertyTemplateDictonary = new Dictionary<Guid, PropertyTemplateEnum>
        {
            { Guid.Empty, PropertyTemplateEnum.None },
            { new Guid("{1B966A14-45B9-4E34-92BB-E2D46D97C5C3}"), PropertyTemplateEnum.ReferenceTitle }
        };

        /// <summary>
        /// Uniqueidentifier of an Entity
        /// </summary>
        [RequiredNonDefault]
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
        [RequiredNonDefault]
        public Guid PropertyTypeId { get; set; }

        //TODO: Change this property to PropertyType
        /// <summary>
        /// Property Type Enum
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public PropertyTypeEnum PropertyType
        {
            get
            {
                if (PropertyTypeId == Guid.Empty)
                {
                    return PropertyTypeEnum.String;
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
        public Guid? PropertyTemplateId { get; set; }

        /// <summary>
        /// Property Template Enum
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public PropertyTemplateEnum PropertyTemplate
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
        public Guid? ParentEntityId { get; set; }

        /// <summary>
        /// Helper Function to get Required
        /// </summary>
        internal bool Required
        {
            get
            {
                var required = false;
                if (ValidationItems != null)
                {
                    required = ValidationItems.Where(v => v.ValidationType == ValidationTypeEnum.Required).Any();
                }
                return required;
            }
        }
        /// <summary>
        /// C# Data Type
        /// </summary>
        internal string CsType
        {
            get
            {

                switch (PropertyType)
                {
                    case PropertyTypeEnum.PrimaryKey:
                        return "Guid";
                    case PropertyTypeEnum.ParentRelationship:
                    case PropertyTypeEnum.ReferenceRelationship:
                        return $"Guid{(!Required ? "?": string.Empty)}";
                    case PropertyTypeEnum.String:
                        return "string";
                    case PropertyTypeEnum.Integer:
                        return $"int{(!Required ? "?" : string.Empty)}";
                    case PropertyTypeEnum.Double:
                        return $"double{(!Required ? "?" : string.Empty)}";
                    case PropertyTypeEnum.DateTime:
                        return "DateTime";
                    case PropertyTypeEnum.Boolean:
                        return $"bool{(!Required ? "?" : string.Empty)}";
                    default:
                        return "string";
                }
            }
        }

        /// <summary>
        /// Type Script Data Type
        /// </summary>
        internal string TsType
        {
            get
            {
                switch (PropertyType)
                {
                    case PropertyTypeEnum.PrimaryKey:
                        return "string";
                    case PropertyTypeEnum.String:
                        return "string";
                    case PropertyTypeEnum.Integer:
                        return "number";
                    case PropertyTypeEnum.Double:
                        return "number";
                    case PropertyTypeEnum.DateTime:
                        return "string";
                    case PropertyTypeEnum.Boolean:
                        return "boolean";
                    default:
                        return "string";
                }
            }
        }

        /// <summary>
        /// Validation Items
        /// </summary>
        public IEnumerable<Validation> ValidationItems { get; set; }
    }
}
