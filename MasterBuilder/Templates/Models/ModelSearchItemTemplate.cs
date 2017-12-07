using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelSearchItemTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);

            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                return Path.Combine(path, $"{screen.InternalName}{screenSection.InternalName}Item.cs");
            }
            else
            {
                return Path.Combine(path, $"{screen.InternalName}Item.cs");
            }
        }

        public static string Evaluate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
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

            var className = $"{screen.InternalName}Item";
            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                className = $"{screen.InternalName}{screenSection.InternalName}Item";
            }
            return $@"
using System;

namespace {project.InternalName}.Models
{{
    /// <summary>
    /// {screen.InternalName} Screen Search Item
    /// </summary>
    public class {className}
    {{
{properties}
    }}
}}";
        }


    }
}
