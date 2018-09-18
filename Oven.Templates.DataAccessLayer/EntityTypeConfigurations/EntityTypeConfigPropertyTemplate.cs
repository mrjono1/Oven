using Oven.Request;
using System.Linq;
using System.Text;

namespace Oven.Templates.DataAccessLayer.EntityTypeConfigurations
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
                case PropertyType.ParentRelationshipOneToMany:
                case PropertyType.ReferenceRelationship:
                case PropertyType.ParentRelationshipOneToOne:
                    dbColumnName = $"{property.InternalName}Id";
                    value.AppendLine($"            builder.Property(p => p.{property.InternalName}Id)");
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
                                value.Append($"                .HasMaxLength({item.IntegerValue.Value})");
                                break;
                            default:
                                break;
                        }
                    }
                }

                // Default Values
                // TODO: support more database default values
                switch (property.PropertyType)
                {
                    case PropertyType.String:
                        if (!string.IsNullOrEmpty(property.DefaultStringValue))
                        {
                            value.AppendLine();
                            value.Append($@"                .HasDefaultValue(""{property.DefaultStringValue}"")");
                        }
                        break;
                    case PropertyType.Integer:
                        if (property.DefaultIntegerValue.HasValue)
                        {
                            value.AppendLine();
                            value.Append($@"                .HasDefaultValue({property.DefaultIntegerValue})");
                        }
                        break;
                    case PropertyType.Boolean:
                        if (property.DefaultBooleanValue.HasValue)
                        {
                            value.AppendLine();
                            value.Append($@"                .HasDefaultValue({(property.DefaultBooleanValue.Value ? "true": "false")})");
                        }
                        break;
                    case PropertyType.Double:
                        if (property.DefaultDoubleValue.HasValue)
                        {
                            value.AppendLine();
                            value.Append($@"                .HasDefaultValue({property.DefaultDoubleValue})");
                        }
                        break;
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
            
            if (property.PropertyType == PropertyType.ParentRelationshipOneToMany ||
                property.PropertyType == PropertyType.ReferenceRelationship)
            {
                var parentEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).Single();
                value.AppendLine();

                switch (property.PropertyType)
                {
                    case PropertyType.ParentRelationshipOneToMany:
                        value.Append($@"            builder.HasOne(p => p.{property.InternalName})
                 .WithMany(p => p.{entity.InternalNamePlural})
                 .HasForeignKey(p => p.{property.InternalName}Id)");
                        break;

                    case PropertyType.ReferenceRelationship:
                        value.Append($@"            builder.HasOne(p => p.{property.InternalName})
                 .WithMany(p => p.{property.InternalName}{entity.InternalNamePlural})
                 .HasForeignKey(p => p.{property.InternalName}Id)
                 .OnDelete(DeleteBehavior.Restrict)");
                        break;
                }
                
                value.Append(";");
            }
            
            return value.ToString();
        }
    }
}
