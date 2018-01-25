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
            ""src"": ""/android-chrome-192x192.png"",
            ""sizes"": ""192x192"",
            ""type"": ""image/png""
        }},
        {{
            ""src"": ""/android-chrome-256x256.png"",
            ""sizes"": ""256x256"",
            ""type"": ""image/png""
        }},
        {{
            ""src"": ""/android-chrome-512x512.png"",
            ""sizes"": ""512x512"",
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