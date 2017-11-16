using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelTemplate
    {
        public static string FileName(string folder, Screen screen, ScreenSection screenSection)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            return Path.Combine(path, $"{screen.InternalName}.cs");
        }

        public static string Evaluate(Project project, Screen screen, ScreenSection screenSection)
        {
            return $@"
using System;

namespace {project.InternalName}.Models
{{
    public class {screen.InternalName}Model
    {{

    }}
}}";
        }


    }
}
