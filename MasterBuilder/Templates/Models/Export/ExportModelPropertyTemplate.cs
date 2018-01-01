using Humanizer;
using MasterBuilder.Request;
using System.Linq;

namespace MasterBuilder.Templates.Models.Export
{
    /// <summary>
    /// Export Model Property Template
    /// </summary>
    public class ExportModelPropertyTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(Project project, Property property)
        {
            var required = false;
            if (property.ValidationItems != null)
            {
                required = property.ValidationItems.Where(v => v.ValidationType == ValidationTypeEnum.Required).Any();
            }

            if (property.Type == PropertyTypeEnum.ReferenceRelationship)
            {
                var relationshipEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).First();
                return $@"        public Guid{(required ? "" : "?")} {property.InternalName}Id {{ get; set; }}";
            }
            else if (property.PropertyTemplate == PropertyTemplateEnum.PrimaryKey)
            {
                return @"        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid Id { get; set; }";
            }
            else if (string.IsNullOrWhiteSpace(property.Calculation))
            {
                return $@"        /// <summary>
        /// {property.Title}
        /// </summary>
        public {property.CsType} {property.InternalName} {{ get; set; }}";
            }
            else
            {
                return $@"        /// <summary>
        /// {property.Title}
        /// </summary>
        public {property.CsType} {property.InternalName} 
        {{ 
             get
             {{
                 return {property.Calculation};
             }}
        }}";
            }
        }

        /// <summary>
        /// Property Mapping
        /// </summary>
        public static string Mapping(Project project, Entity entity, Property property)
        {
            var required = false;
            if (property.ValidationItems != null)
            {
                required = property.ValidationItems.Where(v => v.ValidationType == ValidationTypeEnum.Required).Any();
            }

            if (property.Type == PropertyTypeEnum.ReferenceRelationship)
            {
                var relationshipEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).First();
                return $@"            {property.InternalName}Id = {entity.InternalName.Camelize()}.{property.InternalName}Id;";
            }
            else if (property.PropertyTemplate == PropertyTemplateEnum.PrimaryKey)
            {
                return $@"            Id = {entity.InternalName.Camelize()}.Id;";
            }
            else if (string.IsNullOrWhiteSpace(property.Calculation))
            {
                return $@"            {property.InternalName} = {entity.InternalName.Camelize()}.{property.InternalName};";
            }
            return null;
        }
    }
}
