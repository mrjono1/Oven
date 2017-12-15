namespace MasterBuilder.Request
{
    /// <summary>
    /// Property Types
    /// </summary>
    public enum PropertyTypeEnum
    {
        /// <summary>
        /// Uniqueidentifier not human readable
        /// </summary>
        Uniqueidentifier,
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
        ReferenceRelationship
    }
}