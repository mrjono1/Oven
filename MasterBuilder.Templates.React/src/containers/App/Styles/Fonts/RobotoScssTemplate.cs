using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers.App.Styles.Fonts
{
    /// <summary>
    /// roboto.scss Template
    /// </summary>
    public class RobotoScssTemplate: ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public RobotoScssTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "roboto.scss";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "containers", "App", "styles", "fonts" };
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