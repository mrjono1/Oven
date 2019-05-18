using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Entities
{
    /// <summary>
    /// Entity Property Template
    /// </summary>
    public class EntityPropertyTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        public static string Evaluate(Project project, Property property)
        {
            switch (property.PropertyType)
            {
                case PropertyType.PrimaryKey:
                    return @"        /// <summary>
        /// Primary Key
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }";

                case PropertyType.ParentRelationshipOneToMany:
                    var parentRelationshipEntity = project.Entities.Where(p => p.Id == property.ReferenceEntityId.Value).Single();
                    return $@"        /// <summary>
        /// Foreign Key (Parent Relationship 1:Many) to {parentRelationshipEntity.InternalName}
        /// </summary>
        public {property.CsType} {property.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key navigation object (Parent Relationship 1:Many) to {parentRelationshipEntity.InternalName}
        /// </summary>
        public {parentRelationshipEntity.InternalName} {property.InternalName} {{ get; set; }}";

                case PropertyType.ReferenceRelationship:
                    var referenceRelationshipEntity = project.Entities.Where(p => p.Id == property.ReferenceEntityId.Value).Single();
                    return $@"        /// <summary>
        /// Foreign Key (Reference Relationship) to {referenceRelationshipEntity.InternalName}
        /// </summary>
        public {property.CsType} {property.InternalName}Id {{ get; set; }}";
        
                default:
                    var attributes = new List<string>
                    {
                        $@"[BsonElement(""{property.InternalNameJavaScript}"")]"
                    };
                    if (property.Required)
                    {
                        attributes.Add("[BsonRequired]");
                    }

                    switch (property.PropertyType)
                    {
                        case PropertyType.String:
                            if (property.DefaultStringValue != null)
                            {
                                attributes.Add($@"[BsonDefaultValue(""{property.DefaultStringValue}"")]");
                            }
                            break;
                        case PropertyType.Integer:
                            if (property.DefaultIntegerValue != null)
                            {
                                attributes.Add($@"[BsonDefaultValue({property.DefaultIntegerValue.Value})]");
                            }
                            break;
                        case PropertyType.Boolean:
                            if (property.DefaultBooleanValue != null)
                            {
                                attributes.Add($@"[BsonDefaultValue({property.DefaultBooleanValue.Value.ToString().ToLower()})]");
                            }
                            break;
                        case PropertyType.Double:
                            if (property.DefaultDoubleValue != null)
                            {
                                attributes.Add($@"[BsonDefaultValue({property.DefaultDoubleValue.Value})]");
                            }
                            break;
                    }


                    return $@"        /// <summary>
        /// {property.Title}
        /// </summary>
{attributes.IndentLines(2)}
        public {property.CsType} {property.InternalName} {{ get; set; }}";
            }
        }
    }
}
