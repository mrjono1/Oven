using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelSearchRequestTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            return Path.Combine(path, $"{screen.InternalName}Request.cs");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {project.InternalName}.Models
{{
    public class {screen.InternalName}Request
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
