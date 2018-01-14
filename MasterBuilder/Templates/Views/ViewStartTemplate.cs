using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.Views
{
    /// <summary>
    /// View Start Template
    /// </summary>
    public class ViewStartTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "_ViewStart.cshtml";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Views" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        /// <returns></returns>
        public string GetFileContent()
        {
            return @"@{
    Layout = ""_Layout"";
}";
        }
    }
}
