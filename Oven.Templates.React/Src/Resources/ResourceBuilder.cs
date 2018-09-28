using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.React.Src.Resources
{
    /// <summary>
    /// Build Resources
    /// </summary>
    public class ResourceBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();

            foreach (var screen in Project.Screens)
            {
                switch (screen.ScreenType)
                {
                    case ScreenType.Search:
                        templates.Add(new ListTemplate(Project, screen));
                        break;

                    case ScreenType.Form:
                        templates.Add(new CreateTemplate(Project, screen));
                        templates.Add(new EditTemplate(Project, screen));

                        var rootFormSection = (from screenSection in screen.ScreenSections
                                                 where screenSection.ScreenSectionType == ScreenSectionType.Form
                                                 select screenSection).FirstOrDefault();

                        foreach (var screenSection in screen.ScreenSections.Where(a => a != rootFormSection))
                        {
                            switch (screenSection.ScreenSectionType)
                            {
                                case ScreenSectionType.Form:
                                    templates.Add(new FormPartialTemplate(Project, screen, screenSection, true));
                                    templates.Add(new FormPartialTemplate(Project, screen, screenSection, false));
                                    break;
                                case ScreenSectionType.Search:
                                    templates.Add(new ListPartialTemplate(Project, screen, screenSection));
                                    break;
                                case ScreenSectionType.MenuList:
                                    break;
                                case ScreenSectionType.Html:
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case ScreenType.Html:
                        foreach (var screenSection in screen.ScreenSections)
                        {
                            switch (screenSection.ScreenSectionType)
                            {
                                case ScreenSectionType.Form:
                                    break;
                                case ScreenSectionType.Search:
                                    break;
                                case ScreenSectionType.MenuList:
                                    templates.Add(new MenuListTemplate(Project, screen, screenSection));
                                    break;
                                case ScreenSectionType.Html:
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                }
            }

            return templates;
        }
    }
}
