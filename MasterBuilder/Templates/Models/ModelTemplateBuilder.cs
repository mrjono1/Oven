using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Template Builder
    /// </summary>
    public class ModelTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelTemplateBuilder(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get model templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();

            foreach (var item in Project.Entities)
            {
                foreach (var screen in Project.Screens)
                {
                    if (screen.EntityId.HasValue && screen.EntityId.Value == item.Id)
                    {
                        foreach (var screenSection in screen.ScreenSections)
                        {
                            switch (screenSection.ScreenSectionType)
                            {
                                case ScreenSectionTypeEnum.Form:
                                    templates.Add(new ModelEditResponseTemplate(Project, item, screen, screenSection));
                                    templates.Add(new ModelEditRequestTemplate(Project, item, screen, screenSection));
                                    break;
                                case ScreenSectionTypeEnum.Search:
                                    templates.Add(new ModelSearchRequestTemplate(Project, item, screen, screenSection));
                                    templates.Add(new ModelSearchResponseTemplate(Project, item, screen, screenSection));
                                    templates.Add(new ModelSearchItemTemplate(Project, item, screen, screenSection));
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
                }
            }

            return templates;
        }
    }
}
