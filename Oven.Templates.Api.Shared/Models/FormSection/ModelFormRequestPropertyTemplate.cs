using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.Services.Models
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
                        case ValidationType.Required:
                            attributes.Add("[Required]");
                            break;
                        case ValidationType.MaximumLength:
                            attributes.Add($"[MaxLength({item.IntegerValue})]");
                            break;
                        case ValidationType.MinimumLength:
                            attributes.Add($"[MinLength({item.IntegerValue})]");
                            break;
                        case ValidationType.MaximumValue:
                            // Min value picks this one up
                            break;
                        case ValidationType.MinimumValue:
                            var maxValue = formField.Property.ValidationItems.SingleOrDefault(a => a.ValidationType == ValidationType.MaximumValue);
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
                        case ValidationType.Unique:
                            // TODO: server side unique, currently will fail on db upate
                            break;
                        case ValidationType.Email:
                            attributes.Add("[EmailAddress]");
                            break;
                        case ValidationType.RequiredTrue:
                            // TODO: custom attribute
                            break;
                        case ValidationType.Pattern:
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
