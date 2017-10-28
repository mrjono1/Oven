using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelSearchResponseTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            return Path.Combine(path, $"{screen.InternalName}Response.cs");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            return $@"
using System;

namespace {project.InternalName}.Models
{{
    public class {screen.InternalName}Response
    {{
        public int TotalPages {{ get; internal set; }}
        public int TotalItems {{ get; internal set; }}
        public {screen.InternalName}Item[] Items {{ get; internal set; }}
    }}
}}";
        }


    }
}
