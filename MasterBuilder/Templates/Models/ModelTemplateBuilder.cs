using MasterBuilder.Request;
using System.Linq;
using System.Collections.Generic;
using MasterBuilder.Interfaces;

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
                                   select new
                                   {
                                       ScreenSection = section,
                                       Screen = screen
                                   }
                                   ))
            {

                switch (group.ScreenSection.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        templates.Add(new ModelFormResponseTemplate(Project, group.Screen, group.ScreenSection));
                        templates.Add(new ModelFormRequestTemplate(Project, group.Screen, group.ScreenSection));
                        break;
                    case ScreenSectionTypeEnum.Search:
                        templates.Add(new ModelSearchRequestTemplate(Project, group.Screen, group.ScreenSection));
                        templates.Add(new ModelSearchResponseTemplate(Project, group.Screen, group.ScreenSection));
                        templates.Add(new ModelSearchItemTemplate(Project, group.Screen, group.ScreenSection));
                        break;
                    case ScreenSectionTypeEnum.MenuList:
                        // None
                        break;
                    case ScreenSectionTypeEnum.Html:
                        // None
                        break;
                    default:
                        break;
                }
            }
            
            var entityReferencesNeeded = (from e in Project.Entities
                                   where e.Properties != null
                                   from property in e.Properties
                                   where property.PropertyType == PropertyTypeEnum.ReferenceRelationship &&
                                   property.ParentEntityId.HasValue
                                   from entity in Project.Entities
                                   where entity.Id == property.ParentEntityId
                                   select entity).Distinct().ToArray();
            if (entityReferencesNeeded != null)
            {
                foreach (var entityLookup in entityReferencesNeeded)
                {
                    templates.Add(new Reference.ModelReferenceItemTemplate(Project, entityLookup));
                    templates.Add(new Reference.ModelReferenceRequestTemplate(Project, entityLookup));
                    templates.Add(new Reference.ModelReferenceResponseTemplate(Project, entityLookup));
                }
            }

            return templates;
        }
    }
}
