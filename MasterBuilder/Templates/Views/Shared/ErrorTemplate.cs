using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Views.Shared
{
    /// <summary>
    /// Error
    /// </summary>
    public class ErrorTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ErrorTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Error.cshtml";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Views", "Shared" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"@{{
    ViewData[""Title""] = ""Error"";
}}

<h1 class=""text-danger"">Error.</h1>
<h2 class=""text-danger"">An error occurred while processing your request.</h2>";
        }
    }
}