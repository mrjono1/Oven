using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers.App.Styles
{
    /// <summary>
    /// font.scss Template
    /// </summary>
    public class FontScssTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public FontScssTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "font.scss";
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
            return @"/*
  Fonts
*/

@import url(//fonts.googleapis.com/css?family=Roboto:400,100,300,500,700,900&subset=latin,cyrillic-ext);";
        }
    }
}