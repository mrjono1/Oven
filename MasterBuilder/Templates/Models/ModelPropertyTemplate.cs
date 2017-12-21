using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Property Template
    /// </summary>
    public class ModelPropertyTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(Property property, bool includeValidation = false)
        {
            var result = new StringBuilder();
            var attributes = new List<string>
            {
                $@"/// <summary>
        /// {property.Title}
        /// </summary>
        [Display(Name = ""{property.Title}"")]"
            };

            if (includeValidation && property.ValidationItems != null){
                foreach (var item in property.ValidationItems)
                {
                    switch (item.ValidationType)
                    {
                        case ValidationTypeEnum.Required:
                            attributes.Add("[Required]");
                            break;
                        case ValidationTypeEnum.MaximumLength:
                            attributes.Add($"[MaxLength({item.IntegerValue})]");
                            break;
                        case ValidationTypeEnum.MinimumLength:
                            attributes.Add($"[MinLength({item.IntegerValue})]");
                            break;
                        case ValidationTypeEnum.MaximumValue:
                            // Min value picks this one up
                            break;
                        case ValidationTypeEnum.MinimumValue:
                            var maxValue = property.ValidationItems.SingleOrDefault(a => a.ValidationType == ValidationTypeEnum.MaximumValue);
                            if (maxValue != null)
                            {
                                if (item.IntegerValue.HasValue)
                                {
                                    attributes.Add($@"[Range({item.IntegerValue}, {maxValue.IntegerValue})]");
                                }
                                else if (item.DoubleValue.HasValue)
                                {
                                    attributes.Add($@"[Range({item.DoubleValue}, {maxValue.DoubleValue})]");
                                }
                            }
                            break;
                        case ValidationTypeEnum.Unique:
                            // TODO: server side unique, currently will fail on db upate
                            break;
                        case ValidationTypeEnum.Email:
                            attributes.Add("[EmailAddress]");
                            break;
                        case ValidationTypeEnum.RequiredTrue:
                            // TODO: custom attribute
                            break;
                        case ValidationTypeEnum.Pattern:
                            attributes.Add($@"[RegularExpression(""{item.StringValue}"")]");
                            break;
                        default:
                            break;
                    }
                }
            }

            if (property.Type == PropertyTypeEnum.ParentRelationship || property.Type == PropertyTypeEnum.ReferenceRelationship)
            {
                result.Append($@"        {string.Join(string.Concat(Environment.NewLine, "        "), attributes)}
        public {property.CsType} {property.InternalName}Id {{ get; set; }}");
            }
            else
            {
                result.Append($@"        {string.Join(string.Concat(Environment.NewLine, "        "), attributes)}
        public {property.CsType} {property.InternalName} {{ get; set; }}");
            }
            return result.ToString();
        }
    }
}
