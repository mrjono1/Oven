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
                var formSections = new List<ScreenSection>();
                foreach (var screenSection in screen.ScreenSections)
                {
                    switch (screenSection.ScreenSectionType)
                    {
                        case ScreenSectionType.Form:

                            referenceFormFields.AddRange(screenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyType.ReferenceRelationship));
                            formSections.Add(screenSection);

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


                if (formSections.Any())
                {
                    var rootSections = (from formSection in formSections
                                        where !formSection.ParentScreenSectionId.HasValue
                                        select formSection).ToArray();
                    // TODO: Child Sections
                    //var childSections = (from formSection in formSections
                    //                     where formSection.ParentEntityPropertyId.HasValue
                    //                     select formSection).ToArray();

                    templates.Add(new FormTemplate(Project, screen, rootSections, null, null));
                    //foreach (var childItem in childSections.GroupBy(a => a.ParentEntityProperty).Select(a => new
                    //{
                    //    ParentEntityProperty = a.Key,
                    //    ChildSections = a.ToArray()
                    //}))
                    //{
                    //    templates.Add(new FormTemplate(Project, screen, childItem.ChildSections, null, childItem.ParentEntityProperty));
                    //}
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
