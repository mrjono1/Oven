using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelSearchItemTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            return Path.Combine(path, $"{screen.InternalName}Item.cs");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            StringBuilder properties = null;
            if (entity.Properties != null)
            {
                properties = new StringBuilder();
                foreach (var item in entity.Properties)
                {
                    properties.AppendLine(ModelPropertyTemplate.Evaluate(item));
                }
            }

            return $@"
using System;

namespace {project.InternalName}.Models
{{
    public class {screen.InternalName}Item
    {{
{properties}
    }}
}}";
        }


    }
}
