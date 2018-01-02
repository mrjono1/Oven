using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Models.Reference
{
    /// <summary>
    /// Reference response template
    /// </summary>
    public class ReferenceResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReferenceResponseTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Entity.InternalName}ReferenceResponse.ts";
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
            return $@"import {{ {Entity.InternalName}ReferenceItem }} from './{Entity.InternalName}ReferenceItem';

export interface {Entity.InternalName}ReferenceResponse {{
    items: {Entity.InternalName}ReferenceItem[];
    totalPages: number;
    totalItems: number;
}}";
        }
    }
}
