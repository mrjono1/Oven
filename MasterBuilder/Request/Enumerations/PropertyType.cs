namespace MasterBuilder.Request
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
        /// Relationship to a parent entity
        /// </summary>
        ParentRelationship,
        /// <summary>
        /// Relatoinship to a reference entity
        /// </summary>
        ReferenceRelationship,
        /// <summary>
        /// Double
        /// </summary>
        Double,
        /// <summary>
        /// One to One Relationship to another entity
        /// </summary>
        OneToOneRelationship,
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        Uniqueidentifier
    }
}