using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Entities
{
    public class EntityPropertyTemplate
    {

        public static string Evaluate(Project project, Property property)
        {
            var required = false;
            if (property.ValidationItems != null)
            {
                required = property.ValidationItems.Where(v => v.ValidationType == ValidationTypeEnum.Required).Any();
            }
            
            if (property.Type == PropertyTypeEnum.ParentRelationship || property.Type == PropertyTypeEnum.ReferenceRelationship)
            {
                var relationshipEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).First();
                return $@"        /// <summary>
        /// Foreign Key (Parent Relationship) to {relationshipEntity.InternalName}
        /// </summary>
        public Guid{(required ? "" : "?")} {property.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key navigation object (Parent Relationship) to {relationshipEntity.InternalName}
        /// </summary>
        public {relationshipEntity.InternalName} {property.InternalName} {{ get; set; }}";
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
        /// {property.Title} - Calculated
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
    }
}
