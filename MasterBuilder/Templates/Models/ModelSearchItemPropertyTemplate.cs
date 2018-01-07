using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Search Item Property Template
    /// </summary>
    public class ModelSearchItemPropertyTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(Property property)
        {
            var result = new StringBuilder();
            var attributes = new List<string>
            {
                $@"/// <summary>
        /// {property.Title}
        /// </summary>
        [Display(Name = ""{property.Title}"")]"
            };

            switch (property.PropertyType)
            {
                case PropertyTypeEnum.ParentRelationship:
                    result.Append($@"        {string.Join(string.Concat(Environment.NewLine, "        "), attributes)}
        public {property.CsType} {property.InternalName}Id {{ get; set; }}");
                    break;

                case PropertyTypeEnum.ReferenceRelationship:
                    // Foreign Title
                    result.Append($@"        {string.Join(string.Concat(Environment.NewLine, "        "), attributes)}
        public string {property.InternalName}Title {{ get; set; }}");
                    break;

                default:
                    result.Append($@"        {string.Join(string.Concat(Environment.NewLine, "        "), attributes)}
        public {property.CsType} {property.InternalName} {{ get; set; }}");
                    break;
            }
            
            return result.ToString();
        }
    }
}
