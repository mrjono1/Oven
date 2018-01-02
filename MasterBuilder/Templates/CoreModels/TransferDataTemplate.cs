using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.CoreModels
{
    /// <summary>
    /// Transfer Data Template
    /// </summary>
    public class TransferDataTemplate: ITemplate
    {
        private Project Project { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TransferDataTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "TransferData.cs";
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
    public class TransferData
    {{
        public dynamic request {{ get; set; }}
      
        // Your data here ?
        public object thisCameFromDotNET {{ get; set; }}
    }}
}}";
        }
    }
}
