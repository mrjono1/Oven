using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Models
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
            
            foreach (var screen in Project.Screens)
            {
                var referenceFormFields = new List<FormField>();
                var hasFormScreenSections = false;
                foreach (var screenSection in screen.ScreenSections)
                {
                    switch (screenSection.ScreenSectionType)
                    {
                        case ScreenSectionType.Form:
                            referenceFormFields.AddRange(screenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyType.ReferenceRelationship));
                            hasFormScreenSections = true;
                            break;

                        case ScreenSectionType.Search:
                            templates.Add(new SearchRequestTemplate(Project, screen, screenSection));
                            templates.Add(new SearchResponseTemplate(Project, screen, screenSection));
                            templates.Add(new SearchItemTemplate(Project, screen, screenSection));

                            break;
                        case ScreenSectionType.MenuList:
                            // None
                            break;
                        case ScreenSectionType.Html:
                            // None
                            break;
                    }
                }


                if (hasFormScreenSections)
                {
                    var modelFormTemplateBuilder = new ModelFormTemplateBuilder(Project, screen);
                    templates.AddRange(modelFormTemplateBuilder.GetTemplates());
                }

                foreach (var referenceFormField in referenceFormFields)
                {
                    templates.Add(new Reference.ReferenceRequestTemplate(Project, screen, referenceFormField));
                    templates.Add(new Reference.ReferenceItemTemplate(Project, screen, referenceFormField));
                    templates.Add(new Reference.ReferenceResponseTemplate(Project, screen, referenceFormField));
                }
            }

            templates.Add(new OperationTemplate());
            
            return templates;
        }
    }
}
