using MasterBuilder.Interfaces;
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
    /// <summary>
    /// Transfer Data
    /// </summary>
    public class TransferData
    {{
        /// <summary>
        /// Request
        /// </summary>
        public dynamic Request {{ get; set; }}
      
        /// <summary>
        /// Your data here ?
        /// </summary>
        public object ThisCameFromDotNET {{ get; set; }}
    }}
}}";
        }
    }
}
