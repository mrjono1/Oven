﻿using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.EntityTypeConfigurations
{
    public class EntityTypeConfigPropertyTemplate
    {

        public static string Evaluate(Project project, Entity entity, Property property)
        {
            if (!string.IsNullOrWhiteSpace(property.Calculation))
            {
                return $"            builder.Ignore(p => p.{property.InternalName});";
            }
            
            var value = new StringBuilder();

            if (property.Type == PropertyTypeEnum.ParentRelationship || property.Type == PropertyTypeEnum.ReferenceRelationship)
            {
                value.AppendLine($"            builder.Property(p => p.{property.InternalName}Id)");
            }
            else
            {
                value.AppendLine($"            builder.Property(p => p.{property.InternalName})");
            }
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

            if (property.Type == PropertyTypeEnum.ParentRelationship || property.Type == PropertyTypeEnum.ReferenceRelationship)
            {
                var parentEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).First();
                value.AppendLine();
                value.Append($@"            builder.HasOne(p => p.{property.InternalName})
                .WithMany(p => p.{property.InternalName}{entity.InternalNamePlural})
                .HasForeignKey(p => p.{property.InternalName}Id)");

                if (property.Type == PropertyTypeEnum.ReferenceRelationship)
                {
                    value.Append(@"
                 .OnDelete(DeleteBehavior.Restrict)");
                }
                value.Append(";");
            }


            return value.ToString();
        }
    }
}
