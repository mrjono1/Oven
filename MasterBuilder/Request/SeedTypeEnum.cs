namespace MasterBuilder.Request
{
    /// <summary>
    /// Seed Type Enum
    /// </summary>
    internal enum SeedTypeEnum
    {
        /// <summary>
        /// Add records if none in database
        /// </summary>
        AddIfNone,
        /// <summary>
        /// Ensure all records are added to database
        /// </summary>
        EnsureAllAdded,
        /// <summary>
        /// Ensure all records are added to database and up to date with seed data
        /// </summary>
        EnsureAllUpdated
    }
}
