using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Views.Shared
{
    public class ErrorTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, "Shared"), "Error.cshtml");
        }

        public static string Evaluate(Project project)
        {
            return $@"@{{
    ViewData[""Title""] = ""Error"";
}}

<h1 class=""text-danger"">Error.</h1>
<h2 class=""text-danger"">An error occurred while processing your request.</h2>";
        }
    }
}