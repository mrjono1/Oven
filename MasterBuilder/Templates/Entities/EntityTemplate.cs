using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
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
                where (p.PropertyType == PropertyTypeEnum.ParentRelationship ||
                p.PropertyType == PropertyTypeEnum.ReferenceRelationship ||
                p.PropertyType == PropertyTypeEnum.OneToOneRelationship) &&
                p.ParentEntityId.Value == Entity.Id
                select new { e, p }))
            {
                // can currently only have 1 parent relationship but can have multiple reference relationships
                switch (item.p.PropertyType)
                {
                    case PropertyTypeEnum.ParentRelationship:

                        navigationProperties.Add($@"        /// <summary>
        /// Foreign Key (Via Parent Relationship) to {item.e.InternalName}.{item.p.InternalName}
        /// </summary>
        public ICollection<{item.e.InternalName}> {item.e.InternalNamePlural} {{ get; set; }}");
                        break;
                    case PropertyTypeEnum.ReferenceRelationship:

                        navigationProperties.Add($@"        /// <summary>
        /// Foreign Key (Via Reference Relationship) to {item.e.InternalName}.{item.p.InternalName}
        /// </summary>
        public ICollection<{item.e.InternalName}> {item.p.InternalName}{item.e.InternalNamePlural} {{ get; set; }}");
                        break;
                    case PropertyTypeEnum.OneToOneRelationship:

                        navigationProperties.Add($@"         /// <summary>
        /// Foreign Key (One to One Relationship)
        /// </summary>
        public Guid {item.p.InternalName}{item.e.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key (Via One to One Relationship)
        /// </summary>
        public {item.e.InternalName} {item.p.InternalName}{item.e.InternalName} {{ get; set; }}");
                        break;
                    default:
                        break;
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
