using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ProjectFiles
{
    /// <summary>
    /// appsettings.json configuration
    /// </summary>
    public class AppSettingsTemplate: ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettingsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "appsettings.json";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"{{
  ""ConnectionStrings"": {{
    ""DefaultConnection"": ""{Project.DatabaseConnectionString}""
  }},
  ""Logging"": {{
    ""IncludeScopes"": false,
    ""LogLevel"": {{
      ""Default"": ""Debug"",
      ""System"": ""Information"",
      ""Microsoft"": ""Information""
    }}
  }}
}}";
        }
    }
}