using Oven.Request;
using System.Text;

namespace Oven.Templates.Services.Models
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
            var result = new StringBuilder();
            
            result.Append($@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        [Display(Name = ""{formField.TitleValue}"")]
        public {formField.TypeCSharp} {formField.InternalNameCSharp} {{ get; set; }}");

            switch (formField.PropertyType)
            {
                case PropertyType.ReferenceRelationship:
                    // Foreign Title
                    result.AppendLine();
                    result.Append($@"        /// <summary>
        /// {formField.TitleValue}
        /// </summary>
        [Display(Name = ""{formField.TitleValue}"")]
        public string {formField.InternalNameAlternateCSharp} {{ get; set; }}");
                    break;
            }
            
            return result.ToString();
        }
    }
}
