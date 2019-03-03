using System.Collections.Generic;
using Oven.Request;

namespace Oven.Templates.DataAccessLayer.Services.Contracts
{
    /// <summary>
    /// Get Method Template
    /// </summary>
    public class GetMethodTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public GetMethodTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
        {
            Project = project;
            Screen = screen;
            ScreenSections = screenSections;
        }

        /// <summary>
        /// Get
        /// </summary>
        internal string GetMethod()
        {
            return $@"
        /// <summary>
        /// {Screen.Title} Get
        /// </summary>
        Task<{Screen.FormResponseClass}> GetAsync(string id);";
        }
    }
}
