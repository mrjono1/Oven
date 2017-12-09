using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ProjectFiles
{
    /// <summary>
    /// appsettings.json configuration
    /// </summary>
    public class AppSettingsTemplate
    {
        public static string FileName()
        {
            return "appsettings.json";
        }
        public static string Evaluate(Project project)
        {
            return $@"{{
  ""ConnectionStrings"": {{
    ""DefaultConnection"": ""{project.DatabaseConnectionString}""
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