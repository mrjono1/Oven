using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelSearchRequestTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                return Path.Combine(path, $"{screen.InternalName}{screenSection.InternalName}Request.cs");
            }
            else
            {
                return Path.Combine(path, $"{screen.InternalName}Request.cs");
            }
        }

        public static string Evaluate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var className = $"{screen.InternalName}Request";
            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                className = $"{screen.InternalName}{screenSection.InternalName}Request";
            }

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {project.InternalName}.Models
{{
    public class {className}
    {{
        [Required]
        [DefaultValue(1)]
        public int Page {{ get; set; }}
        [Required]
        [DefaultValue(10)]
        public int PageSize {{ get; set; }}
        
        // TODO: Search fields
    }}
}}";
        }


    }
}
