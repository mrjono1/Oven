using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Src.Components.TextField
{
    /// <summary>
    /// styles.scss Template
    /// </summary>
    public class StylesScssTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public StylesScssTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "styles.scss";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "components", "TextField" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @":local(.styles) {

}";
        }
    }
}