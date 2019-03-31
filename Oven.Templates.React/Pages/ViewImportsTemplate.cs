using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Pages
{
    /// <summary>
    /// View Imports
    /// </summary>
    public class ViewImportsTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewImportsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "_ViewImports.cshtml";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Pages" };
        }

        /// <summary>
        /// Get File Content
        /// </summary>
        public string GetFileContent()
        {
            return $@"@using {Project.InternalName}
@namespace {Project.InternalName}.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
";
        }
    }
}
