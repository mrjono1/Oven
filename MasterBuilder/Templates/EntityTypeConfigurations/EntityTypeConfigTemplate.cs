using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;

namespace MasterBuilder.Templates.EntityTypeConfigurations
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

            return $@"using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {Project.InternalName}.EntityTypeConfigurations
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
            builder.ToTable(""{(Project.ImutableDatabase ? Entity.Id.ToString() : Entity.InternalName)}"");
            builder.HasKey(p => p.Id);
{string.Join(Environment.NewLine, properties)}
        }}
    }}
}}";
        }
    }
}
