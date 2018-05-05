using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Api.CoreModels
{
    /// <summary>
    /// IRequestTemplate
    /// </summary>
    public class IRequestTemplate: ITemplate
    {
        private Project Project { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public IRequestTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "IRequest.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "CoreModels" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"namespace {Project.InternalName}.CoreModels
{{
    /// <summary>
    /// IRequest
    /// </summary>
    public class IRequest
    {{
        /// <summary>
        /// Cookies
        /// </summary>
        public object Cookies {{ get; set; }}
        /// <summary>
        /// Headers
        /// </summary>
        public object Headers {{ get; set; }}
        /// <summary>
        /// Host
        /// </summary>
        public object Host {{ get; set; }}
    }}
}}";
        }
    }
}