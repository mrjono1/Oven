using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Views.Home
{
    public class IndexTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, "Home"), "Index.cshtml");
        }

        public static string Evaluate(Project project)
        {
            return $@"@{{
    ViewData[""Title""] = ""{project.Title}"";
}}

<app asp-prerender-module=""ClientApp/dist/main-server"">Loading...</app>

<script src=""~/dist/vendor.js"" asp-append-version=""true""></script>
@section scripts {{
    <script src=""~/dist/main-client.js"" asp-append-version=""true""></script>
}}
";
        }
    }
}