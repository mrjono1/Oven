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
            return $@"
using System;

namespace {project.InternalName}.Models
{{
    public class {screen.InternalName}Request
    {{
        public int Page {{ get; set; }}
        public int PageSize {{ get; set; }}
        
        // TODO: Search fields
    }}
}}";
        }


    }
}
