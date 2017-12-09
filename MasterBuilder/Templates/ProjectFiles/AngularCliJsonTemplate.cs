using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ProjectFiles
{
    /// <summary>
    /// angular-cli.json configration
    /// </summary>
    public class AngularCliJsonTemplate
    {
        public static string FileName()
        {
            return "angular-cli.json";
        }
        public static string Evaluate(Project project)
        {
            return $@"{{
  ""$schema"": ""./node_modules/@angular/cli/lib/config/schema.json"",
  ""project"": {{
    ""name"": ""{project.Title}""
  }},
  ""apps"": [
    {{
      ""root"": ""ClientApp""
    }}
  ],
  ""defaults"": {{
    ""styleExt"": ""scss"",
    ""component"": {{
      ""spec"": false
    }}
  }},
  ""lint"":[
    {{
      ""project"": ""ClientApp/tsconfig.app.json""
    }},
    {{
      ""project"": ""ClientApp/tsconfig.spec.json""
    }}
  ]
}}";
        }
    }
}