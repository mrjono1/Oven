using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Linq;
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

            foreach (var group in (from screen in Project.Screens
                                   where screen.ScreenSections != null
                                   from section in screen.ScreenSections
                                   where section.EntityId != null
                                   select new
                                   {
                                       ScreenSection = section,
                                       Screen = screen,
                                       Entity = Project.Entities.SingleOrDefault(a => a.Id == section.EntityId)
                                   }
                                   ))
            {

                switch (group.ScreenSection.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        templates.Add(new ModelEditResponseTemplate(Project, group.Entity, group.Screen, group.ScreenSection));
                        templates.Add(new ModelEditRequestTemplate(Project, group.Entity, group.Screen, group.ScreenSection));
                        break;
                    case ScreenSectionTypeEnum.Search:
                        templates.Add(new ModelSearchRequestTemplate(Project, group.Entity, group.Screen, group.ScreenSection));
                        templates.Add(new ModelSearchResponseTemplate(Project, group.Entity, group.Screen, group.ScreenSection));
                        templates.Add(new ModelSearchItemTemplate(Project, group.Entity, group.Screen, group.ScreenSection));
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }
            }

            return templates;
        }
    }
}
