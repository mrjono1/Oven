using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Views
{
    public class ViewImportsTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(folder, "_ViewImports.cshtml");
        }

        public static string Evaluate(Project project)
        {
            return $@"@using {project.InternalName}
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Microsoft.AspNetCore.SpaServices";
        }
    }
}
