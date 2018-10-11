using Oven.Request;
using System.Linq;
using System.Collections.Generic;
using Oven.Interfaces;

namespace Oven.Templates.Services.Models
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

            foreach (var screen in Project.Screens)
            {
                var referenceFormFields = new List<FormField>();
                var hasFormSections = false;
                foreach (var screenSection in screen.ScreenSections)
                {
                    switch (screenSection.ScreenSectionType)
                    {
                        case ScreenSectionType.Form:
                            referenceFormFields.AddRange(screenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyType.ReferenceRelationship));
                            hasFormSections = true;
                            break;
                        case ScreenSectionType.Search:
                            templates.Add(new ModelSearchRequestTemplate(Project, screen, screenSection));
                            templates.Add(new ModelSearchResponseTemplate(Project, screen, screenSection));
                            templates.Add(new ModelSearchItemTemplate(Project, screen, screenSection));
                            break;
                        case ScreenSectionType.MenuList:
                            // None
                            break;
                        case ScreenSectionType.Html:
                            // None
                            break;
                        default:
                            break;
                    }
                }

                foreach (var referenceFormField in referenceFormFields)
                {
                    templates.Add(new Reference.ModelReferenceItemTemplate(Project, screen, referenceFormField));
                    templates.Add(new Reference.ModelReferenceRequestTemplate(Project, screen, referenceFormField));
                    templates.Add(new Reference.ModelReferenceResponseTemplate(Project, screen, referenceFormField));
                }

                if (hasFormSections)
                {
                    var modelFormSectionTemplateBuilder = new ModelFormSectionTemplateBuilder(Project, screen);
                    templates.AddRange(modelFormSectionTemplateBuilder.GetTemplates());
                }
            }

            return templates;
        }
    }
}
