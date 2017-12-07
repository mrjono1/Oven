using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelSearchResponseTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                return Path.Combine(path, $"{screen.InternalName}{screenSection.InternalName}Response.cs");
            }
            else
            {
                return Path.Combine(path, $"{screen.InternalName}Response.cs");
            }
        }

        public static string Evaluate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var className = $"{screen.InternalName}Response";
            var itemClassName = $"{screen.InternalName}Item";
            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                className = $"{screen.InternalName}{screenSection.InternalName}Response";
                itemClassName = $"{screen.InternalName}{screenSection.InternalName}Item";
            }

            return $@"
using System;

namespace {project.InternalName}.Models
{{
    /// <summary>
    /// {screen.InternalName} Screen Search Response
    /// </summary>
    public class {className}
    {{
        public int TotalPages {{ get; internal set; }}
        public int TotalItems {{ get; internal set; }}
        public {itemClassName}[] Items {{ get; internal set; }}
    }}
}}";
        }


    }
}
