using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.EntityTypeConfigurations
{
    /// <summary>
    /// Entity Type Config
    /// </summary>
    public class EntityTypeConfigTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Coinstructor
        /// </summary>
        public EntityTypeConfigTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Entity.InternalName}Config.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        /// <returns></returns>
        public string[] GetFilePath()
        {
            return new string[] { "EntityTypeConfigurations" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var properties = new List<string>();
            if (Entity.Properties != null)
            {
                foreach (var item in Entity.Properties)
                {
                    properties.Add(EntityTypeConfigPropertyTemplate.Evaluate(Project, Entity, item));
                }
            }

            foreach (var item in (from e in Project.Entities
                                  where e.Properties != null
                                  from p in e.Properties
                                  where p.PropertyType == PropertyType.ParentRelationshipOneToOne &&
                                  p.ReferenceEntityId.Value == Entity.Id
                                  select new { e, p }))
            {

                properties.Add($@"            builder.HasOne(p => p.{item.e.InternalName})
                .WithOne(p => p.{Entity.InternalName})
                .HasForeignKey<Entities.{item.e.InternalName}>(p => p.{Entity.InternalName}Id)
                .OnDelete(DeleteBehavior.Cascade);");
            }

            var tableName = Entity.InternalName;
            if (Project.ImutableDatabase.HasValue && Project.ImutableDatabase.Value)
            {
                tableName = Entity.Id.ToString();
            }

            var toTable = $@"builder.ToTable(""{tableName}"");";
            if (!string.IsNullOrWhiteSpace(Entity.DatabaseSchema))
            {
                toTable = $@"builder.ToTable(""{tableName}"", ""{Entity.DatabaseSchema}"");";
            }

            return $@"using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {Project.InternalName}.DataAccessLayer.EntityTypeConfigurations
{{
    /// <summary>
    /// Class to configure the {Entity.InternalName} database settings
    /// </summary>
    public class {Entity.InternalName}Config : IEntityTypeConfiguration<Entities.{Entity.InternalName}>
    {{
        /// <summary>
        /// Configure the {Entity.InternalName} Entity
        /// </summary>
        /// <param name=""builder"">The injected model builder for the {Entity.InternalName} entity</param>
        public void Configure(EntityTypeBuilder<Entities.{Entity.InternalName}> builder)
        {{
            {toTable}
            builder.HasKey(p => p.Id);
{string.Join(Environment.NewLine, properties)}
        }}
    }}
}}";
        }
    }
}
