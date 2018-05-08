using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers.App.Styles
{
    /// <summary>
    /// typography.scss Template
    /// </summary>
    public class TypographyScssTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public TypographyScssTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "typography.scss";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "containers", "App", "styles" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"/* Typography */

body {
  font-family: 'Roboto', sans-serif;
  font-weight: 300;
}

h1,
h2,
h3,
h4,
h5,
h6 {
  font-weight: 300;
  margin: 0 0;
  padding: 0;
}

p {
  font-size: 16px;
}";
        }
    }
}