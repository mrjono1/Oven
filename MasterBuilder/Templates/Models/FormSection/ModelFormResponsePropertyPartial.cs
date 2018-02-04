using MasterBuilder.Request;
using System;
using System.Collections.Generic;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Form Response Property Template
    /// </summary>
    public class ModelFormResponsePropertyPartial
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static IEnumerable<string> Evaluate(FormField formField)
        {
            var properties = new List<string>
            {
                $@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        [Display(Name = ""{formField.TitleValue}"")]
        public {formField.TypeCSharp} {formField.InternalNameCSharp} {{ get; set; }}"
            };

            switch (formField.PropertyType)
            {
                case PropertyType.ReferenceRelationship:
                    // Foreign Title
                    properties.Add($@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        [Display(Name = ""{formField.TitleValue}"")]
        public string {formField.InternalNameAlternateCSharp} {{ get; set; }}");
                    break;
            }
            
            return properties;
        }
    }
}
