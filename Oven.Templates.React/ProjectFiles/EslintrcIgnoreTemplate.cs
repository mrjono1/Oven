using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// .eslintrcignore
    /// </summary>
    public class EslintrcIgnoreTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public EslintrcIgnoreTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return ".eslintrcignore";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"wwwroot
node_modules
bin";
        }
    }
}