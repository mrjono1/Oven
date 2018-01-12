using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Models.Reference
{
    /// <summary>
    /// Model Reference Response Template
    /// </summary>
    public class ModelReferenceResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelReferenceResponseTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Entity.InternalName}ReferenceResponse.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Models", Entity.InternalName, "Reference" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var itemClassName = $"{Entity.InternalName}.Reference.{Entity.InternalName}ReferenceItem";

            return $@"using System;

namespace {Project.InternalName}.Models.{Entity.InternalName}.Reference
{{
    /// <summary>
    /// {Entity.InternalName} Reference Response
    /// </summary>
    public class {Entity.InternalName}ReferenceResponse
    {{
        /// <summary>
        /// Total Pages
        /// </summary>
        public int TotalPages {{ get; internal set; }}
        /// <summary>
        /// Total Items
        /// </summary>
        public int TotalItems {{ get; internal set; }}
        /// <summary>
        /// {itemClassName}
        /// </summary>
        public {itemClassName}[] Items {{ get; internal set; }}
    }}
}}";
        }
    }
}
