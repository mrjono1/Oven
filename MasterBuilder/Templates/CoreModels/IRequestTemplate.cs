using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.CoreModels
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
    public class IRequest
    {{
        public object cookies {{ get; set; }}
        public object headers {{ get; set; }}
        public object host {{ get; set; }}
    }}
}}";
        }
    }
}