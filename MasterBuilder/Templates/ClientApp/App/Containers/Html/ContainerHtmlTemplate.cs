using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Html
{
    public class ContainerHtmlTemplate: ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        public ContainerHtmlTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        public string GetFileName()
        {
            return $"{Screen.InternalName.ToLowerInvariant()}.component.html";
        }

        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "containers", Screen.InternalName.ToLowerInvariant() };
        }

        public string GetFileContent()
        {
            var sections = new List<string>();

            foreach (var screenSection in Screen.ScreenSections)
            {
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        var formSection = new FormSectionBuilder(Project, Screen, screenSection);
                        sections.Add(formSection.Evaluate());
                        break;
                    case ScreenSectionTypeEnum.Search:
                        var searchSection = new SearchSectionBuilder(Project, Screen, screenSection);
                        sections.Add(searchSection.Evaluate());
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        sections.Add($@"<div class=""screen-type-search"">
{screenSection.Html}
</div>");
                        break;
                    default:
                        break;
                }
            }

            return $@" <div class=""screen"">
    <h1>{Screen.Title}</h1>
{string.Join(Environment.NewLine, sections)}
</div>";
        }
    }
}
