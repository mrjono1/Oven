namespace MasterBuilder.Request
{
    /// <summary>
    /// Validation Types
    /// </summary>
    public enum ValidationTypeEnum
    {
        /// <summary>
        /// Required
        /// </summary>
        Required,
        /// <summary>
        /// Maximum Length
        /// </summary>
        MaximumLength,
        /// <summary>
        /// Minimum Length
        /// </summary>
        MinimumLength,
        /// <summary>
        /// Maximum Value
        /// </summary>
        MaximumValue,
        /// <summary>
        /// Minimum Value
        /// </summary>
        MinimumValue,
        /// <summary>
        /// Unique
        /// </summary>
        Unique,
        /// <summary>
        /// Email
        /// </summary>
        Email,
        /// <summary>
        /// Required True
        /// </summary>
        RequiredTrue,
        /// <summary>
        /// Pattern: Regular Expression
        /// </summary>
        Pattern
    }
}