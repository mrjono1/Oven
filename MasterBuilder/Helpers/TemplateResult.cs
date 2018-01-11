namespace MasterBuilder.Helpers
{
    /// <summary>
    /// Template Result
    /// </summary>
    internal class TemplateResult
    {
        /// <summary>
        /// Result File Path
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Error Message
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Has Error
        /// </summary>
        public bool HasError { get; set; }
    }
}
