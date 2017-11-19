using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    public class Property
    {
        public string InternalName { get; set; }

        public PropertyTypeEnum Type { get; set; }
        public string Calculation { get; set; }
        public Guid Id { get; set; }

        public Guid? PropertyTemplateId { get; set; }

        internal PropertyTemplateEnum PropertyTemplate
        {
            get
            {
                if (PropertyTemplateId.HasValue)
                {
                    var propertyTemplates = new Dictionary<Guid, PropertyTemplateEnum>
                {
                    {new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"), PropertyTemplateEnum.PrimaryKey }
                };

                    return propertyTemplates.GetValueOrDefault(PropertyTemplateId.Value, PropertyTemplateEnum.None);
                }
                else
                {
                    return PropertyTemplateEnum.None;
                }
            }
        }

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

                switch (Type)
                {
                    case PropertyTypeEnum.ParentRelationship:
                    case PropertyTypeEnum.Uniqueidentifier:
                        return $"Guid{(!Required ? "?": string.Empty)}";
                    case PropertyTypeEnum.String:
                        return "string";
                    case PropertyTypeEnum.Integer:
                        return $"int{(!Required ? "?" : string.Empty)}";
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
                switch (Type)
                {
                    case PropertyTypeEnum.Uniqueidentifier:
                        return "string";
                    case PropertyTypeEnum.String:
                        return "string";
                    case PropertyTypeEnum.Integer:
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

        public IEnumerable<Validation> ValidationItems { get; set; }
        public string Title { get; set; }
        public Guid? ParentEntityId { get; set; }

        internal bool Validate()
        {
            // Ensure only one validation method of each type
            return true;
        }
    }
}
