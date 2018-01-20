using MasterBuilder.Request;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.EntityTypeConfigurations
{
    /// <summary>
    /// Entity Type Config Property Template
    /// </summary>
    public class EntityTypeConfigPropertyTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(Project project, Entity entity, Property property)
        {            
            var value = new StringBuilder();
            string dbColumnName = null;
            switch (property.PropertyType)
            {
                case PropertyType.ParentRelationship:
                case PropertyType.ReferenceRelationship:
                    dbColumnName = $"{property.InternalName}Id";
                    value.AppendLine($"            builder.Property(p => p.{property.InternalName}Id)");
                    break;
                case PropertyType.OneToOneRelationship:
                    // no property
                    break;
                default:
                    dbColumnName = property.InternalName;
                    value.AppendLine($"            builder.Property(p => p.{property.InternalName})");
                    break;
            }
            if (value.Length != 0)
            {
                value.Append($@"                .HasColumnName(""{(project.ImutableDatabase.Value ? property.Id.ToString() : dbColumnName)}"")");

                if (property.ValidationItems != null)
                {
                    foreach (var item in property.ValidationItems)
                    {
                        switch (item.ValidationType)
                        {
                            case ValidationType.Required:
                                value.AppendLine();
                                value.Append("                .IsRequired()");
                                break;
                            case ValidationType.MaximumLength:
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
                            case ValidationType.Unique:
                                value.AppendLine();
                                value.Append($@"            builder.HasIndex(p => p.{property.InternalName})
                  .IsUnique();");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            
            if (property.PropertyType == PropertyType.ParentRelationship ||
                property.PropertyType == PropertyType.ReferenceRelationship ||
                property.PropertyType == PropertyType.OneToOneRelationship)
            {
                var parentEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).First();
                value.AppendLine();

                if (property.PropertyType == PropertyType.ParentRelationship)
                {
                    value.Append($@"            builder.HasOne(p => p.{property.InternalName})
                    .WithMany(p => p.{entity.InternalNamePlural})
                    .HasForeignKey(p => p.{property.InternalName}Id)");
                }
                else if (property.PropertyType == PropertyType.ReferenceRelationship)
                {
                    value.Append($@"            builder.HasOne(p => p.{property.InternalName})
                    .WithMany(p => p.{property.InternalName}{entity.InternalNamePlural})
                    .HasForeignKey(p => p.{property.InternalName}Id)
                    .OnDelete(DeleteBehavior.Restrict)");
                }
                else if (property.PropertyType == PropertyType.OneToOneRelationship)
                {
                    value.Append($@"            builder.HasOne(p => p.{property.InternalName})
                    .WithOne(p => p.{property.InternalName}{entity.InternalName})
                    .HasForeignKey<Entities.{parentEntity.InternalName}>(p => p.{property.InternalName}{entity.InternalName}Id)
                    .OnDelete(DeleteBehavior.Cascade)");
                }
                value.Append(";");
            }


            return value.ToString();
        }
    }
}
