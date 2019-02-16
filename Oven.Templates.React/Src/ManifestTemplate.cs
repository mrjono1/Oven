using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Src
{
    /// <summary>
    /// manifest.json configuration
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
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"{{
  ""short_name"": ""{Project.InternalName}"",
  ""name"": ""{Project.Title}"",
  ""start_url"": ""./?utm_source=web_app_manifest"",
  ""icons"": [
    {{
      ""src"": ""/images/chrome-touch-icon-192x192.png"",
      ""sizes"": ""192x192"",
      ""type"": ""image/png""
    }},
    {{
      ""src"": ""/images/chrome-touch-icon-384x384.png"",
      ""sizes"": ""384x384"",
      ""type"": ""image/png""
    }}
  ],
  ""background_color"": ""#00BCD4"",
  ""theme_color"": ""#00BCD4"",
  ""display"": ""standalone"",
  ""orientation"": ""portrait""
}}";
        }
    }
}