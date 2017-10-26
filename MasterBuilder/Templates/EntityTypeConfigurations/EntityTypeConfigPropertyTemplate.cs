using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.EntityTypeConfigurations
{
    public class EntityTypeConfigPropertyTemplate
    {

        public static string Evaluate(Property property)
        {
            if (property.HasCalculation)
            {
                return $"            builder.Ignore(p => p.{property.InternalName});";
            }
            
            var value = new StringBuilder();
            
            value.AppendLine($"            builder.Property(p => p.{property.InternalName})");
            value.Append($@"                .HasColumnName(""{property.Id}"")");

            if (property.ValidationItems != null)
            {
                foreach (var item in property.ValidationItems)
                {
                    switch (item.ValidationType)
                    {
                        case ValidationTypeEnum.Required:
                            value.AppendLine();
                            value.Append("              .IsRequired()");
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

            return value.ToString();
        }
    }
}
