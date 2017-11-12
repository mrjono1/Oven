using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen
{
    public class ScreenTypeSearchHtmlTemplate
    {
        public static string Evaluate(Project project, Request.Screen screen)
        {
            var sections = new List<string>();
            if (screen.ScreenSections != null)
            {
                foreach (var screenSection in screen.ScreenSections)
                {
                    switch (screenSection.ScreenSectionType)
                    {
                        case ScreenSectionTypeEnum.Search:
                            sections.Add(ScreenTypeSearch.SectionTypeSearchHtmlTemplate.Evaluate(project, screen, screenSection));
                            break;
                        case ScreenSectionTypeEnum.Grid:
                            break;
                        case ScreenSectionTypeEnum.Html:
                            break;
                        default:
                            break;
                    }
                }
            }

            var navigateToScreenPath = (from s in project.Screens
                                        where s.Id == screen.NavigateToScreenId
                                        select s.Path).FirstOrDefault();
            
            return $@"<div class=""screen-type-search"">
<nav>
    <a [routerLink]=""['/{navigateToScreenPath}']"">New</a>
</nav>
{string.Join(Environment.NewLine, sections)}
</div>";

        }
    }
}