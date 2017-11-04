using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.EntityTypeConfigurations
{
    public class EntityTypeConfigPropertyTemplate
    {

        public static string Evaluate(Project project, Property property)
        {
            if (property.HasCalculation)
            {
                return $"            builder.Ignore(p => p.{property.InternalName});";
            }
            
            var value = new StringBuilder();
            
            value.AppendLine($"            builder.Property(p => p.{property.InternalName})");
            value.Append($@"                .HasColumnName(""{(project.ImutableDatabase ? property.Id.ToString() : property.InternalName)}"")");

            if (property.ValidationItems != null)
            {
                foreach (var item in property.ValidationItems)
                {
                    switch (item.ValidationType)
                    {
                        case ValidationTypeEnum.Required:
                            value.AppendLine();
                            value.Append("                .IsRequired()");
                            break;
                        case ValidationTypeEnum.MaximumLength:
                            value.AppendLine();
                            value.Append($"             .HasMaxLength({item.IntegerValue.Value})");
                            break;
                        default:
                            break;
                    }
                }
            }

            value.Append(";");

            if (property.ValidationItems != null)
            {
                foreach (var item in property.ValidationItems)
                {
                    switch (item.ValidationType)
                    {
                        case ValidationTypeEnum.Unique:
                            value.AppendLine();
                            value.Append($@"            builder.HasIndex(p => p.{property.InternalName})
                  .IsUnique();");
                            break;
                        default:
                            break;
                    }
                }
            }

            

            return value.ToString();
        }
    }
}
