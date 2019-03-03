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
            var properties = new List<string>() { $@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        [Display(Name = ""{formField.TitleValue}"")]
        public {formField.TypeCSharp} {formField.InternalNameCSharp} {{ get; set; }}" };

            switch (formField.PropertyType)
            {
                case PropertyType.PrimaryKey:
                case PropertyType.ParentRelationshipOneToMany:
                        properties.Add($@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        internal ObjectId Object{formField.InternalNameCSharp}
        {{
            get
            {{
                return ObjectId.Parse({formField.InternalNameCSharp});
            }}
            set
            {{
                {formField.InternalNameCSharp} = value.ToString();
            }}
        }}");
                    break;
                case PropertyType.ReferenceRelationship:
                    // Foreign Title
                    properties.Add($@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        [Display(Name = ""{formField.TitleValue}"")]
        public string {formField.InternalNameAlternateCSharp} {{ get; set; }}");
                    properties.Add($@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        internal ObjectId Object{formField.InternalNameCSharp}
        {{
            get
            {{
                return ObjectId.Parse({formField.InternalNameCSharp});
            }}
            set
            {{
                {formField.InternalNameCSharp} = value.ToString();
            }}
        }}");
                    break;
            }

            return string.Join(Environment.NewLine, properties);
        }
    }
}
