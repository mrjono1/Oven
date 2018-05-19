using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.ProjectFiles
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
  ""presets"": [
    [""env"", {""modules"": false}],
    ""react""
  ],
  ""plugins"": [
    ""react-hot-loader/babel""
  ],
  ""env"": {
    ""production"": {
      ""presets"": [""minify""]
    },
    ""test"": {
      ""presets"": [""env"", ""react""]
    }
  }
}";
        }
    }
}