using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.Angular.WwwRoot.i18n
{
    /// <summary>
    /// Language Template
    /// </summary>
    public class LanguageTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "en.json";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "wwwroot", "assets", "i18n" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"{
    ""HOME"":""Home"",
    ""ENGLISH"":""English""
}";
        }
    }
}
