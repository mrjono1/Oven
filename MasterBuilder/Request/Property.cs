﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Property
    {
        public string InternalName { get; set; }

        public PropertyTypeEnum Type { get; set; }
        internal bool HasCalculation
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Calculation);
            }
        }
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

        /// <summary>
        /// C# Data Type
        /// </summary>
        internal string CsType
        {
            get
            {
                switch (Type)
                {
                    case PropertyTypeEnum.Uniqueidentifier:
                        return "Guid";
                    case PropertyTypeEnum.String:
                        return "string";
                    case PropertyTypeEnum.Integer:
                        return "int";
                    case PropertyTypeEnum.DateTime:
                        return "DateTime";
                    default:
                        return "string";
                }
            }
        }

        public IEnumerable<Validation> ValidationItems { get; set; }
        public string Title { get; internal set; }

        internal bool Validate()
        {
            // Ensure only one validation method of each type
            return true;
        }
    }
}
