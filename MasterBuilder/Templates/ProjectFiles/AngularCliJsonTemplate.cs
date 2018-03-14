using System;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ProjectFiles
{
    /// <summary>
    /// angular-cli.json configration
    /// </summary>
    public class AngularCliJsonTemplate: ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AngularCliJsonTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "angular-cli.json";
        }

        /// <summary>
        /// Get file path
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
  ""$schema"": ""./node_modules/@angular/cli/lib/config/schema.json"",
  ""project"": {{
    ""name"": ""{Project.Title}""
  }},
  ""apps"": [
    {{
      ""root"": ""ClientApp"",
      ""serviceWorker"": true
    }}
  ],
  ""defaults"": {{
    ""styleExt"": ""scss"",
    ""component"": {{
      ""spec"": false
    }}
  }},{(Project.IncludeSupportForSpatial ? $@"""scripts"": [
    ""./node_modules/openlayers/dist/ol.js"",
    ""./node_modules/proj4/dist/proj4.js"",
    ""./node_modules/jspdf/dist/jspdf.min.js"",
    ""./node_modules/jquery/dist/jquery.min.js""
  ]," : String.Empty)}
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