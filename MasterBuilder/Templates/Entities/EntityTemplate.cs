using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MasterBuilder.Templates.Entities
{
    /// <summary>
    /// Entity Template
    /// </summary>
    public class EntityTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityTemplate(Project project, Entity entity)
        {
            Project = project;
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
            return new string[] { "Entities" };
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
                foreach (var item in Entity.Properties)
                {
                    properties.Add(EntityPropertyTemplate.Evaluate(Project, item));
                }
            }

            foreach (var item in (from e in Project.Entities
                where e.Properties != null
                from p in e.Properties
                where (p.Type == PropertyTypeEnum.ParentRelationship || p.Type == PropertyTypeEnum.ReferenceRelationship) &&
                p.ParentEntityId.Value == Entity.Id
                select new { e, p }))
            {
                // can currently only have 1 parent relationship but can have multiple reference relationships
                if (item.p.Type == PropertyTypeEnum.ParentRelationship)
                {
                    navigationProperties.Add($@"        /// <summary>
        /// Foreign Key (Via Parent Relationship) to {item.e.InternalName}.{item.p.InternalName}
        /// </summary>
        public ICollection<{item.e.InternalName}> {item.e.InternalNamePlural} {{ get; set; }}");
                }
                else
                {
                    navigationProperties.Add($@"        /// <summary>
        /// Foreign Key (Via Reference Relationship) to {item.e.InternalName}.{item.p.InternalName}
        /// </summary>
        public ICollection<{item.e.InternalName}> {item.p.InternalName}{item.e.InternalNamePlural} {{ get; set; }}");
                }
            }

            return $@"using System; {(navigationProperties.Any() ? string.Concat(Environment.NewLine, "using System.Collections.Generic;") : string.Empty)}

namespace {Project.InternalName}.Entities
{{
    /// <summary>
    /// {Entity.InternalName} Entity
    /// </summary>
    public class {Entity.InternalName}
    {{
        /// <summary>
        /// {Entity.InternalName} Constructor for defaulting values
        /// </summary>
        public {Entity.InternalName}()
        {{
            Id = Guid.NewGuid();
        }}

{string.Join(Environment.NewLine, properties)}
{string.Join(Environment.NewLine, navigationProperties)}
    }}
}}";
        }
    }
}
