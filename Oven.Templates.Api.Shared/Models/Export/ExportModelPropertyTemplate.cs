using Humanizer;
using Oven.Request;
using System.Linq;

namespace Oven.Templates.Services.Models.Export
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
                required = property.ValidationItems.Where(v => v.ValidationType == ValidationType.Required).Any();
            }

            if (property.PropertyType == PropertyType.ReferenceRelationship)
            {
                var relationshipEntity = project.Entities.Where(p => p.Id == property.ReferenceEntityId.Value).First();
                return $@"        /// <summary>
        /// {property.Title}
        /// </summary>
        public Guid{(required ? "" : "?")} {property.InternalName}Id {{ get; set; }}";
            }
            else if (property.PropertyType == PropertyType.PrimaryKey)
            {
                return @"        /// <summary>
        /// Primary Key
        /// </summary>
        public Guid Id { get; set; }";
            }
            else
            {
                return $@"        /// <summary>
        /// {property.Title}
        /// </summary>
        public {property.CsType} {property.InternalName} {{ get; set; }}";
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
                required = property.ValidationItems.Where(v => v.ValidationType == ValidationType.Required).Any();
            }

            if (property.PropertyType == PropertyType.ReferenceRelationship)
            {
                var relationshipEntity = project.Entities.Where(p => p.Id == property.ReferenceEntityId.Value).First();
                return $@"            {property.InternalName}Id = {entity.InternalName.Camelize()}.{property.InternalName}Id;";
            }
            else if (property.PropertyType == PropertyType.PrimaryKey)
            {
                return $@"            Id = {entity.InternalName.Camelize()}.Id;";
            }
            else
            {
                return $@"            {property.InternalName} = {entity.InternalName.Camelize()}.{property.InternalName};";
            }
        }
    }
}
