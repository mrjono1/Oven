using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Models.Reference
{
    /// <summary>
    /// Reference Item Template
    /// </summary>
    public class ReferenceItemTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceItemTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Entity.InternalName}ReferenceItem.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Entity.InternalName.ToLowerInvariant()}" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"export interface {Entity.InternalName}ReferenceItem {{
    id: string;
    title: string;
}}";
        }
    }
}
