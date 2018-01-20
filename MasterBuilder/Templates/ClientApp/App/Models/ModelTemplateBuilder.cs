using MasterBuilder.Interfaces;
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
            var hasReference = false;
            foreach (var screen in Project.Screens)
            {
                var referenceFormFields = new List<FormField>();
                foreach (var screenSection in screen.ScreenSections)
                {
                    switch (screenSection.ScreenSectionType)
                    {
                        case ScreenSectionTypeEnum.Form:

                            referenceFormFields.AddRange(screenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyTypeEnum.ReferenceRelationship));
                            templates.Add(new FormTemplate(Project, screen, screenSection));

                            break;
                        case ScreenSectionTypeEnum.Search:

                            templates.Add(new SearchRequestTemplate(Project, screen, screenSection));
                            templates.Add(new SearchResponseTemplate(Project, screen, screenSection));
                            templates.Add(new SearchItemTemplate(Project, screen, screenSection));

                            break;
                        case ScreenSectionTypeEnum.MenuList:
                            // None
                            break;
                        case ScreenSectionTypeEnum.Html:
                            // None
                            break;
                    }
                }

                foreach (var referenceFormField in referenceFormFields)
                {
                    hasReference = true;
                    templates.Add(new Reference.ReferenceItemTemplate(Project, screen, referenceFormField));
                    templates.Add(new Reference.ReferenceResponseTemplate(Project, screen, referenceFormField));
                }
            }

            if (hasReference)
            {
                templates.Add(new Reference.ReferenceRequestTemplate());
            }
            templates.Add(new OperationTemplate());
            
            return templates;
        }
    }
}
