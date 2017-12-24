using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MasterBuilder.Templates.Models.Export
{
    /// <summary>
    /// Export Model Template
    /// </summary>
    public class ExportModelTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity RootEntity;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExportModelTemplate(Project project, Entity rootEntity, Entity entity)
        {
            Project = project;
            RootEntity = rootEntity;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Entity.InternalName}.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Models", RootEntity.InternalName, "Export" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var properties = new List<string>();
            var navigationProperties = new List<string>();

            if (Entity.Properties != null)
            {
                foreach (var item in Entity.Properties.Where(p => p.Type != PropertyTypeEnum.ParentRelationship))
                {
                    properties.Add(ExportModelPropertyTemplate.Evaluate(Project, item));
                }
            }

            foreach (var item in (from e in Project.Entities
                where e.Properties != null
                from p in e.Properties
                where (p.Type == PropertyTypeEnum.ParentRelationship || p.Type == PropertyTypeEnum.ReferenceRelationship) &&
                p.ParentEntityId.Value == Entity.Id
                select new { e, p }))
            {
                navigationProperties.Add($"        public {item.e.InternalName}[] {item.p.InternalName}{item.e.InternalNamePlural} {{ get; set; }}");
            }

            return $@"using System; {(navigationProperties.Any() ? string.Concat(Environment.NewLine, "using System.Collections.Generic;") : string.Empty)}

namespace {Project.InternalName}.Models.{RootEntity.InternalName}.Export
{{
    /// <summary>
    /// {Entity.InternalName} Export Model
    /// </summary>
    public class {Entity.InternalName}
    {{

{string.Join(Environment.NewLine, properties)}
{string.Join(Environment.NewLine, navigationProperties)}
    }}
}}";
        }
    }
}
