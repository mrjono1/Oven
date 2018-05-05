using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Api.Models
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
            return $"{ScreenSection.SearchSection.SearchResponseClass}.cs";
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
            return $@"using System;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Search Response
    /// </summary>
    public class {ScreenSection.SearchSection.SearchResponseClass}
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
        /// {ScreenSection.SearchSection.SearchItemClass}
        /// </summary>
        public {ScreenSection.SearchSection.SearchItemClass}[] Items {{ get; internal set; }}
    }}
}}";
        }
    }
}
