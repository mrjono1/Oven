using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Form Request Property Template
    /// </summary>
    public class ModelFormRequestPropertyTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(FormField formField)
        {
            var attributes = new List<string>
            {
                $@"/// <summary>
        /// {formField.TitleValue}
        /// </summary>
        [Display(Name = ""{formField.TitleValue}"")]"
            };

            if (formField.Property.ValidationItems != null){
                foreach (var item in formField.Property.ValidationItems)
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
                            var maxValue = formField.Property.ValidationItems.SingleOrDefault(a => a.ValidationType == ValidationTypeEnum.MaximumValue);
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
            
            return $@"        {string.Join(string.Concat(Environment.NewLine, "        "), attributes)}
        public {formField.TypeCSharp} {formField.InternalNameCSharp} {{ get; set; }}";
        }
    }
}
