using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.Src.Resources
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

                        var searchSections = (from screenSection in screen.ScreenSections
                                              where screenSection.ScreenSectionType == ScreenSectionType.Search
                                              select screenSection);
                        foreach (var searchSection in searchSections)
                        {
                            templates.Add(new ListPartialTemplate(Project, screen, searchSection));
                        }
                        break;

                    //case ScreenType.View:
                    //    templates.Add(new ShowTemplate(Project, screen));
                    //    break;
                }
            }

            return templates;
        }
    }
}
