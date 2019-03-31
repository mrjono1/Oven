using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Pages
{
    /// <summary>
    /// Error Cs Template
    /// </summary>
    public class ErrorCsTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ErrorCsTemplate(Project project)
        {
            Project = project;
        }
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Error.cshtml.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Pages" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        /// <returns></returns>
        public string GetFileContent()
        {
            return $@"using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace {Project.InternalName}.Pages
{{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {{
        public string RequestId {{ get; set; }}

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {{
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }}
    }}
}}
";
        }
    }
}
