using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.React.Src.Containers.Sections
{
    /// <summary>
    /// Html Section Builder
    /// </summary>
    public class HtmlSectionTemplate : ISectionTemplate
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

        public IEnumerable<string> Constructor()
        {
            return new string[] { };
        }

        public IEnumerable<string> Imports()
        {
            return new string[] { };
        }

        public IEnumerable<string> Methods()
        {
            return new string[] { };
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
