using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Linq;

namespace MasterBuilder.Templates.Models.Reference
{
    /// <summary>
    /// Model Search Request Template
    /// </summary>
    public class ModelReferenceRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelReferenceRequestTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Entity.InternalName}ReferenceRequest.cs";
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models.{Entity.InternalName}.Reference
{{
    /// <summary>
    /// {Entity.InternalName} Reference Request
    /// </summary>
    public class {Entity.InternalName}ReferenceRequest
    {{
        [Required]
        [DefaultValue(1)]
        public int Page {{ get; set; }}
        [Required]
        [DefaultValue(10)]
        public int PageSize {{ get; set; }}
        public string Query {{ get; set; }}
    }}
}}";
        }


    }
}
