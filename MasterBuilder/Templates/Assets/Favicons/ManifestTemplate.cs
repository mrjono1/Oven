using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Assets.Favicons
{
    /// <summary>
    /// manifest.json template configration
    /// </summary>
    public class ManifestTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ManifestTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "manifest.json";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "wwwroot", "assets", "favicons" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"{{
    ""name"": ""{Project.Title}"",
    ""short_name"": ""{Project.Title}"",
    ""icons"": [
        {{
            ""src"": ""/android-chrome-72x72.png"",
            ""sizes"": ""72x72"",
            ""type"": ""image/png""
        }}
    ],
    ""theme_color"": ""#ffffff"",
    ""background_color"": ""#ffffff"",
    ""display"": ""standalone"",
    ""short_url"": ""/""
}}";
        }
    }
}