using MasterBuilder.Request;
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

            if (includeValidation && property.ValidationItems != null){
                foreach (var item in property.ValidationItems)
                {
                    if (item.ValidationType == ValidationTypeEnum.Required)
                    {
                        result.Append("        [Required]");
                        result.AppendLine();
                    }
                }
            }
            if (property.Type == PropertyTypeEnum.ParentRelationship || property.Type == PropertyTypeEnum.ReferenceRelationship)
            {
                result.Append($@"        public {property.CsType} {property.InternalName}Id {{ get; set; }}");
            }
            else
            {
                result.Append($@"        public {property.CsType} {property.InternalName} {{ get; set; }}");
            }
            return result.ToString();
        }
    }
}
