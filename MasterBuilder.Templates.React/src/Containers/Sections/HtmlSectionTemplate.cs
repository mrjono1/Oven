using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.React.src.Containers.Sections
{
    /// <summary>
    /// Html Section Builder
    /// </summary>
    public class HtmlSectionTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlSectionTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        internal string Evaluate()
        {
            // TODO: validate html, no scripting
            return $@"        <div>
{ScreenSection.Html}
        </div>";
        }
    }
}
