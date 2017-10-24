using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
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

            StringBuilder properties = null;
            if (entity.Properties != null)
            {
                properties = new StringBuilder();
                foreach (var item in entity.Properties)
                {
                    properties.AppendLine(EntityPropertyTemplate.Evaluate(item));
                }
            }

            return $@"
using System;

namespace {project.InternalName}.Entities
{{
    public class {entity.InternalName}
    {{
{properties}
    }}
}}";
        }


    }
}
