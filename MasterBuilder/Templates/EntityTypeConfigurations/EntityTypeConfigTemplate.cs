using MasterBuilder.Request;
using System.IO;
using System.Text;


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
            StringBuilder properties = null;
            if (entity.Properties != null)
            {
                properties = new StringBuilder();
                foreach (var item in entity.Properties)
                {
                    properties.AppendLine(EntityTypeConfigPropertyTemplate.Evaluate(item));
                }
            }

            return $@"
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {project.InternalName}.EntityTypeConfigurations
{{
    public class {entity.InternalName}Config : IEntityTypeConfiguration<{entity.QualifiedInternalName}>
    {{
        public void Configure(EntityTypeBuilder<{entity.QualifiedInternalName}> builder)
        {{
            builder.ToTable(""{entity.Id}"");
            builder.HasKey(p => p.Id);
{properties}
        }}
    }}
}}";
        }


    }
}
