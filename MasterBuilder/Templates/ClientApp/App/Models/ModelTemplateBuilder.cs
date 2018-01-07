using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    /// <summary>
    /// Client app model template builder
    /// </summary>
    public class ModelTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project"></param>
        public ModelTemplateBuilder(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get templates
        /// </summary>
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
                    templates.Add(new Reference.ReferenceItemTemplate(Project, entityLookup));
                    templates.Add(new Reference.ReferenceResponseTemplate(Project, entityLookup));
                }

                templates.Add(new Reference.ReferenceRequestTemplate());
            }

            return templates;
        }
    }
}
