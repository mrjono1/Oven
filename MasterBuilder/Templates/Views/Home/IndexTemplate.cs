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
            return $@"@Html.Raw(ViewData[""SpaHtml""])

<script src=""~/dist/vendor.js"" asp-append-version=""true""></script>
@section scripts {{
    <!-- Our webpack bundle -->
    <script src=""~/dist/main-client.js"" asp-append-version=""true""></script>
}}";
        }
    }
}