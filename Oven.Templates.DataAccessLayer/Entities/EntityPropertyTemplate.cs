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
        public string Id { get; set; }";

                case PropertyType.ParentRelationshipOneToMany:
                    var parentRelationshipEntity = project.Entities.Where(p => p.Id == property.ReferenceEntityId.Value).Single();
                    return $@"        /// <summary>
        /// Foreign Key (Parent Relationship 1:Many) to {parentRelationshipEntity.InternalName}
        /// </summary>
        public string {property.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key navigation object (Parent Relationship 1:Many) to {parentRelationshipEntity.InternalName}
        /// </summary>
        public {parentRelationshipEntity.InternalName} {property.InternalName} {{ get; set; }}";

                case PropertyType.ReferenceRelationship:
                    var referenceRelationshipEntity = project.Entities.Where(p => p.Id == property.ReferenceEntityId.Value).Single();
                    return $@"        /// <summary>
        /// Foreign Key (Reference Relationship) to {referenceRelationshipEntity.InternalName}
        /// </summary>
        public string {property.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key navigation object (Reference Relationship) to {referenceRelationshipEntity.InternalName}
        /// </summary>
        public {referenceRelationshipEntity.InternalName} {property.InternalName} {{ get; set; }}";

                case PropertyType.ParentRelationshipOneToOne:
                    var oneToOneRelationshipEntity = project.Entities.Where(p => p.Id == property.ReferenceEntityId.Value).Single();
                    return $@"        /// <summary>
        /// Foreign Key (Parent Relationship 1:1) to {oneToOneRelationshipEntity.InternalName}
        /// </summary>
        public string {property.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key navigation object (Parent Relationship 1:1) to {oneToOneRelationshipEntity.InternalName}
        /// </summary>
        public {oneToOneRelationshipEntity.InternalName} {property.InternalName} {{ get; set; }}";

                default:
                    var attributes = new List<string>
                    {
                        $@"[BsonElement(""{property.InternalNameJavaScript}"")]"
                    };
                    if (property.Required)
                    {
                        attributes.Add("[BsonRequired]");
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
