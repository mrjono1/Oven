using Humanizer;
using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles
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
  ""DatabaseSettings"": {{
    ""ConnectionString"": ""mongodb+srv://ovenman:LwA91B2EuVHaaUsz@oven-hni60.azure.mongodb.net/test?retryWrites=true"",
    ""DatabaseName"": ""{Project.InternalName.Camelize()}""
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