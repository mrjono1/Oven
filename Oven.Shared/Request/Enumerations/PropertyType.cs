namespace Oven.Request
{
    /// <summary>
    /// Property Types
    /// </summary>
    public enum PropertyType
    {
        /// <summary>
        /// Uniqueidentifier not human readable
        /// </summary>
        PrimaryKey,
        /// <summary>
        /// String
        /// </summary>
        String,
        /// <summary>
        /// Integer
        /// </summary>
        Integer,
        /// <summary>
        /// Date Time
        /// </summary>
        DateTime,
        /// <summary>
        /// Boolean
        /// </summary>
        Boolean,
        /// <summary>
        /// Relationship to a parent entity, can be zero, one or many
        /// </summary>
        ParentRelationshipOneToMany,
        /// <summary>
        /// Relatoinship to a reference entity
        /// </summary>
        ReferenceRelationship,
        /// <summary>
        /// Double
        /// </summary>
        Double,
        /// <summary>
        /// Relationship to a parent entity, can be zero or one
        /// </summary>
        ParentRelationshipOneToOne,
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        Uniqueidentifier,
        /// <summary>
        /// Spatial (geography)
        /// </summary>
        Spatial,
    }
}