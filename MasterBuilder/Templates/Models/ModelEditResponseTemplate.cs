using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    public class ModelEditResponseTemplate
    {
        public static string FileName(string folder, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var path = FileHelper.CreateFolder(folder, screen.InternalName);
            return Path.Combine(path, $"{screen.InternalName}Response.cs");
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

            return $@"
using System;

namespace {project.InternalName}.Models
{{
    /// <summary>
    /// {screen.InternalName} Screen Load
    /// </summary>
    public class {screen.InternalName}Response
    {{
{properties}
    }}
}}";
        }
    }
}
