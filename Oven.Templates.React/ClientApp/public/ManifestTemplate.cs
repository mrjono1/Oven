using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ClientApp.Public
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
            return new string[] { "ClientApp" , "public" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"{{
  ""short_name"": ""{Project.InternalName}"",
  ""name"": ""{Project.Title}"",
  ""icons"": [
    {{
      ""src"": ""favicon.ico"",
      ""sizes"": ""64x64 32x32 24x24 16x16"",
      ""type"": ""image/x-icon""
    }}
  ],
  ""start_url"": ""./index.html"",
  ""display"": ""standalone"",
  ""theme_color"": ""#000000"",
  ""background_color"": ""#ffffff""
}}";
        }
    }
}