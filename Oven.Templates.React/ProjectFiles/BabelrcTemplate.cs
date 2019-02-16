using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// .babelrc configuration
    /// </summary>
    public class BabelrcTemplate: ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public BabelrcTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return ".babelrc";
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
            return @"{
  ""presets"": [ ""react"", ""es2015"" ],
  ""plugins"": [ ""transform-class-properties"", ""syntax-object-rest-spread"" ],
  ""env"": {
    ""development"": {
     ""presets"": [ ""react-hmre"" ]
    },
    ""production"": {
      ""presets"": [ ""react"", ""es2015"" ]
    }
  }
}";
        }
    }
}