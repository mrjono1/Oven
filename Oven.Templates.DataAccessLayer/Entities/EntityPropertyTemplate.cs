using Oven.Request;
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
        public Guid Id { get; set; }";

                case PropertyType.ParentRelationshipOneToMany:
                    var parentRelationshipEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).Single();
                    return $@"        /// <summary>
        /// Foreign Key (Parent Relationship 1:Many) to {parentRelationshipEntity.InternalName}
        /// </summary>
        public Guid{(property.Required ? "" : "?")} {property.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key navigation object (Parent Relationship 1:Many) to {parentRelationshipEntity.InternalName}
        /// </summary>
        public {parentRelationshipEntity.InternalName} {property.InternalName} {{ get; set; }}";

                case PropertyType.ReferenceRelationship:
                    var referenceRelationshipEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).Single();
                    return $@"        /// <summary>
        /// Foreign Key (Reference Relationship) to {referenceRelationshipEntity.InternalName}
        /// </summary>
        public Guid{(property.Required ? "" : "?")} {property.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key navigation object (Reference Relationship) to {referenceRelationshipEntity.InternalName}
        /// </summary>
        public {referenceRelationshipEntity.InternalName} {property.InternalName} {{ get; set; }}";

                case PropertyType.ParentRelationshipOneToOne:
                    var oneToOneRelationshipEntity = project.Entities.Where(p => p.Id == property.ParentEntityId.Value).Single();
                    return $@"        /// <summary>
        /// Foreign Key (Parent Relationship 1:1) to {oneToOneRelationshipEntity.InternalName}
        /// </summary>
        public Guid{(property.Required ? "" : "?")} {property.InternalName}Id {{ get; set; }}
        /// <summary>
        /// Foreign Key navigation object (Parent Relationship 1:1) to {oneToOneRelationshipEntity.InternalName}
        /// </summary>
        public {oneToOneRelationshipEntity.InternalName} {property.InternalName} {{ get; set; }}";

                default:
                    return $@"        /// <summary>
        /// {property.Title}
        /// </summary>
        public {property.CsType} {property.InternalName} {{ get; set; }}";
            }
        }
    }
}
