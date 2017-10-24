using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ProjectFiles
{
    public class AppSettingsTemplate
    {
        public static string FileName()
        {
            return "appsettings.json";
        }
        public static string Evaluate()
        {
            return $@"{{
  ""Logging"": {{
    ""IncludeScopes"": false,
    ""Debug"": {{
                ""LogLevel"": {{
                    ""Default"": ""Warning""
                }}
            }},
    ""Console"": {{
            ""LogLevel"": {{
                ""Default"": ""Warning""
            }}
        }}
    }}
}}";
        }
    }
}
