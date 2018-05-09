using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Views.Home
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
            return $@"@{{
    ViewData[""Title""] = ""Home Page"";
}}

<div id=""root"">Loading...</div>";
        }
    }
}