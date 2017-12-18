using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Views.Home
{
    /// <summary>
    /// Index
    /// </summary>
    public class IndexTemplate :ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project"></param>
        public IndexTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Index.cshtml";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Views", "Home" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
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