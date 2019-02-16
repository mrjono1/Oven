namespace Oven.Interfaces
{
    /// <summary>
    /// Interface for templates
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// Get the template file name
        /// </summary>
        string GetFileName();
        /// <summary>
        /// Get the template file path
        /// </summary>
        string[] GetFilePath();
        /// <summary>
        /// Get the template content
        /// </summary>
        /// <returns></returns>
        string GetFileContent();
    }
}
