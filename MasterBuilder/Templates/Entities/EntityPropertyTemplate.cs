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

            if (property.Type == PropertyTypeEnum.Relationship)
            {
                var parentEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).First();
                return $@"        public Guid{(required ? "?" : "")} {property.InternalName}Id {{ get; set; }}
        public {parentEntity.InternalName} {property.InternalName} {{ get; set; }}";
            }
            else if (string.IsNullOrWhiteSpace(property.Calculation))
            {
                return $@"        public {property.CsType} {property.InternalName} {{ get; set; }}";
            }
            else
            {
                return $@"        public {property.CsType} {property.InternalName} 
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
