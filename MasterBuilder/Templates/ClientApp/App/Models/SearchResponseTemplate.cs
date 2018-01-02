using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    /// <summary>
    /// Search Response template
    /// </summary>
    public class SearchResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchResponseTemplate(Project project, Screen screen, ScreenSection screenSection)
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
            return $"{ScreenSection.InternalName}Response.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Screen.InternalName.ToLowerInvariant()}" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import {{ {ScreenSection.InternalName}Item }} from './{ScreenSection.InternalName}Item';

export interface {ScreenSection.InternalName}Response {{
    items: {ScreenSection.InternalName}Item[];
    totalPages: number;
    totalItems: number;
}}";
        }
    }
}
