using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    public class ModelTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        public ModelTemplateBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();
            
            foreach (var entity in Project.Entities)
            {
                foreach (var screen in Project.Screens.Where(s => s.EntityId == entity.Id))
                {
                    foreach (var screenSection in screen.ScreenSections)
                    {
                        switch (screenSection.ScreenSectionType)
                        {
                            case ScreenSectionTypeEnum.Form:
                                
                                templates.Add(new FormTemplate(Project, screen, screenSection));

                                break;
                            case ScreenSectionTypeEnum.Search:

                                templates.Add(new SearchRequestTemplate(Project, screen, screenSection));
                                templates.Add(new SearchResponseTemplate(Project, screen, screenSection));
                                templates.Add(new SearchItemTemplate(Project, screen, screenSection));

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

            templates.Add(new OperationTemplate());

            return templates;
        }
    }
}
