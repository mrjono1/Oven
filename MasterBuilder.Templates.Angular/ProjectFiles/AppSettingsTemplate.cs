using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Angular.ProjectFiles
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

            string connectionString = null;
            if (Project.UseMySql)
            {
                connectionString = string.Empty;
                // localhost = $"Server=localhost;database=MasterBuilderUi;uid=root;pwd=password;";
            }
            else
            {
                connectionString = $"Data Source=.\\\\SQLEXPRESS;Initial Catalog={Project.InternalName};Integrated Security=True";
            }

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