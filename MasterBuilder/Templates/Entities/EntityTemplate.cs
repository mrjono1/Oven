using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Entities
{
    public class EntityTemplate
    {
        public static string FileName(string folder, Entity entity)
        {
            return Path.Combine(folder, $"{entity.InternalName}.cs");
        }

        public static string Evaluate(Project project, Entity entity)
        {
            entity.QualifiedInternalName = $"Entities.{entity.InternalName}";

            var properties = new List<string>();
            var navigationProperties = new List<string>();

            if (entity.Properties != null)
            {
                foreach (var item in entity.Properties)
                {
                    properties.Add(EntityPropertyTemplate.Evaluate(project, item));
                }
            }

            foreach (var item in (from e in project.Entities
                where e.Properties != null
                from p in e.Properties
                where p.Type == PropertyTypeEnum.Relationship &&
                p.ParentEntityId.Value == entity.Id
                select new { e, p }))
            {
                navigationProperties.Add($"        public ICollection<{item.e.InternalName}> {item.e.InternalNamePlural} {{ get; set; }}");
            }

            return $@"using System; {(navigationProperties.Any() ? string.Concat(Environment.NewLine, "using System.Collections.Generic;") : string.Empty)}

namespace {project.InternalName}.Entities
{{
    public class {entity.InternalName}
    {{
        public {entity.InternalName}(){{
            Id = Guid.NewGuid();
        }}
{string.Join(Environment.NewLine, properties)}
{string.Join(Environment.NewLine, navigationProperties)}
    }}
}}";
        }


    }
}
