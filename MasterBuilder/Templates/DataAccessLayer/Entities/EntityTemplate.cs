using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.DataAccessLayer.Entities
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
            var constructorDefaultValues = new List<string>
            {
                "            Id = Guid.NewGuid();"
            };
            var properties = new List<string>();
            var navigationProperties = new List<string>();

            if (Entity.Properties != null)
            {
                foreach (var property in Entity.Properties)
                {
                    properties.Add(EntityPropertyTemplate.Evaluate(Project, property));
                    
                    switch (property.PropertyType)
                    {
                        case PropertyType.String:
                            if (!string.IsNullOrEmpty(property.DefaultStringValue))
                            {
                                constructorDefaultValues.Add($@"            {property.InternalName} = ""{property.DefaultStringValue}"";");
                            }
                            break;
                        case PropertyType.Integer:
                            if (property.DefaultIntegerValue.HasValue)
                            {
                                constructorDefaultValues.Add($@"            {property.InternalName} = {property.DefaultIntegerValue.Value};");
                            }
                            break;
                        case PropertyType.DateTime:
                            break;
                        case PropertyType.Boolean:
                            if (property.DefaultBooleanValue.HasValue)
                            {
                                constructorDefaultValues.Add($@"            {property.InternalName} = {property.DefaultBooleanValue.Value.ToString().ToLowerInvariant()};");
                            }
                            break;
                        case PropertyType.Double:
                            if (property.DefaultDoubleValue.HasValue)
                            {
                                constructorDefaultValues.Add($@"            {property.InternalName} = {property.DefaultDoubleValue.Value};");
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            foreach (var item in (from e in Project.Entities
                where e.Properties != null
                from p in e.Properties
                where (p.PropertyType == PropertyType.ParentRelationshipOneToMany ||
                p.PropertyType == PropertyType.ReferenceRelationship ||
                p.PropertyType == PropertyType.ParentRelationshipOneToOne) &&
                p.ParentEntityId.Value == Entity.Id
                select new { e, p }))
            {
                // can currently only have 1 parent relationship but can have multiple reference relationships
                switch (item.p.PropertyType)
                {
                    case PropertyType.ParentRelationshipOneToMany:

                        navigationProperties.Add($@"        /// <summary>
        /// Foreign Key (Via Parent Relationship) to {item.e.InternalName}.{item.p.InternalName}
        /// </summary>
        public ICollection<{item.e.InternalName}> {item.e.InternalNamePlural} {{ get; set; }}");
                        break;
                    case PropertyType.ReferenceRelationship:

                        navigationProperties.Add($@"        /// <summary>
        /// Foreign Key (Via Reference Relationship) to {item.e.InternalName}.{item.p.InternalName}
        /// </summary>
        public ICollection<{item.e.InternalName}> {item.p.InternalName}{item.e.InternalNamePlural} {{ get; set; }}");
                        break;
                    case PropertyType.ParentRelationshipOneToOne:

                        navigationProperties.Add($@"         /// <summary>
        /// Foreign Key (One to One Relationship)
        /// </summary>
        public Guid? {item.p.InternalName}{item.e.InternalName}Id {{ get; set; }}
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

namespace {Project.InternalName}.DataAccessLayer.Entities
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
{string.Join(Environment.NewLine, constructorDefaultValues)}
        }}

{string.Join(Environment.NewLine, properties)}
{string.Join(Environment.NewLine, navigationProperties)}
    }}
}}";
        }
    }
}
