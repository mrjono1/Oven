using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;

namespace MasterBuilder.Templates.EntityTypeConfigurations
{
    public class EntityTypeConfigTemplate
    {
        public static string FileName(string folder, Entity entity)
        {
            return Path.Combine(folder, $"{entity.InternalName}Config.cs");
        }

        public static string Evaluate(Project project, Entity entity)
        {
            var properties = new List<string>();
            if (entity.Properties != null)
            {
                foreach (var item in entity.Properties)
                {
                    properties.Add(EntityTypeConfigPropertyTemplate.Evaluate(project, entity, item));
                }
            }

            return $@"using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {project.InternalName}.EntityTypeConfigurations
{{
    /// <summary>
    /// Class to configure the {entity.InternalName} database settings
    /// </summary>
    public class {entity.InternalName}Config : IEntityTypeConfiguration<{entity.QualifiedInternalName}>
    {{
        /// <summary>
        /// Configure the {entity.InternalName} Entity
        /// </summary>
        /// <param name=""builder"">The injected model builder for the {entity.InternalName} entity</param>
        public void Configure(EntityTypeBuilder<{entity.QualifiedInternalName}> builder)
        {{
            builder.ToTable(""{(project.ImutableDatabase ? entity.Id.ToString() : entity.InternalName)}"");
            builder.HasKey(p => p.Id);
{string.Join(Environment.NewLine, properties)}
        }}
    }}
}}";
        }
    }
}
