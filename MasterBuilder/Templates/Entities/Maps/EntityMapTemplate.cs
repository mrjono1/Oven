using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;


namespace MasterBuilder.Templates.Entities.Maps
{
    public class EntityMapTemplate
    {
        public static string Evaluate(Project project, Entity entity)
        {
            StringBuilder properties = null;
            if (entity.Properties != null)
            {
                properties = new StringBuilder();
                foreach (var item in entity.Properties)
                {
                    properties.AppendLine(EntityPropertyMapTemplate.Evaluate(item));
                }
            }

            return $@"
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {project.InternalName}.Entities.Maps
{{
    public class {entity.InternalName}Map : EntityTypeConfiguration<{entity.InternalName}>
    {{
        public override void Map(EntityTypeBuilder<{entity.InternalName}> builder)
        {{
            builder.ToTable(""{entity.Id}"");
{properties}
        }}
    {{
}}";
        }


    }
}
