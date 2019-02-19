using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.Api.ProjectFiles
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

            string connectionString = $"Data Source=.\\\\SQLEXPRESS;Initial Catalog={Project.InternalName};Integrated Security=True";

            return $@"{{
  ""ConnectionStrings"": {{
    ""DefaultConnection"": ""{connectionString}""
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