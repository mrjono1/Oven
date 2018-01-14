using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Search Response Template
    /// </summary>
    public class ModelSearchResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelSearchResponseTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            if (Screen.ScreenType == ScreenTypeEnum.Search)
            {
                return $"{Screen.InternalName}Response.cs";
            }
            else
            {
                return $"{Screen.InternalName}{ScreenSection.InternalName}Response.cs";
            }
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Models", Screen.InternalName };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var className = $"{Screen.InternalName}Response";
            var itemClassName = $"{Screen.InternalName}Item";
            if (Screen.ScreenType != ScreenTypeEnum.Search)
            {
                className = $"{Screen.InternalName}{ScreenSection.InternalName}Response";
                itemClassName = $"{Screen.InternalName}{ScreenSection.InternalName}Item";
            }

            return $@"using System;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Search Response
    /// </summary>
    public class {className}
    {{
        /// <summary>
        /// Total Pages
        /// </summary>
        public int TotalPages {{ get; internal set; }}
        /// <summary>
        /// Total Items
        /// </summary>
        public int TotalItems {{ get; internal set; }}
        /// <summary>
        /// {itemClassName}
        /// </summary>
        public {itemClassName}[] Items {{ get; internal set; }}
    }}
}}";
        }
    }
}
