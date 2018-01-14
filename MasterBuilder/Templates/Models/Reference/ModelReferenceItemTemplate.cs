using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Models.Reference
{
    /// <summary>
    /// Model Reference Item Template
    /// </summary>
    public class ModelReferenceItemTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelReferenceItemTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Entity.InternalName}ReferenceItem.cs";
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
            return $@"using System;

namespace {Project.InternalName}.Models.{Entity.InternalName}.Reference
{{
    /// <summary>
    /// {Entity.InternalName} Reference Item
    /// </summary>
    public class {Entity.InternalName}ReferenceItem
    {{
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id {{ get; set; }}
        /// <summary>
        /// Title
        /// </summary>
        public string Title {{ get; set; }}
    }}
}}";
        }


    }
}
