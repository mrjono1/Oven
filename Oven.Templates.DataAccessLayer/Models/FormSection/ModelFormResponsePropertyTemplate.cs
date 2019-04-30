using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oven.Templates.DataAccessLayer.Models
{
    /// <summary>
    /// Model Form Response Property Template
    /// </summary>
    public class ModelFormResponsePropertyTemplate
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

            var property = $@"        {string.Join(string.Concat(Environment.NewLine, "        "), attributes)}
        public {formField.TypeCSharp} {formField.InternalNameCSharp} {{ get; set; }}";


            var properties = new List<string>{ property };

            switch (formField.PropertyType)
            {
                case PropertyType.PrimaryKey:
                case PropertyType.ParentRelationshipOneToMany:
                    break;
                case PropertyType.ReferenceRelationship:
                    // Foreign Title
                    properties.Add($@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        [Display(Name = ""{formField.TitleValue}"")]
        public string {formField.InternalNameAlternateCSharp} {{ get; set; }}");
                    break;
            }

            return string.Join(Environment.NewLine, properties);

        }
    }
}
