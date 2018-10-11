using Oven.Request;
using System.Linq;
using System.Collections.Generic;
using Oven.Interfaces;

namespace Oven.Templates.Services.Models
{
    /// <summary>
    /// Model Form Section Template Builder
    /// </summary>
    public class ModelFormSectionTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelFormSectionTemplateBuilder(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get model templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();
            var defaultScreenSection = new ScreenSection();

            var groupedFormScreenSections = (from ss in Screen.ScreenSections
                                   where ss.ScreenSectionType == ScreenSectionType.Form
                                   select ss)
                                   .GroupBy(ss => new
                                   {
                                       ParentSection = ss.ParentScreenSection,
                                       ss.Entity
                                   })
                                   .Select(a => new { a.Key.ParentSection, Values = a.ToArray() });

            foreach (var group in groupedFormScreenSections)
            {
                var children = new List<ScreenSection>();


                foreach (var item in group.Values)
                {
                    var childSections = (from child in groupedFormScreenSections
                                         where child.ParentSection == item
                                         select child.Values).ToList();
                    if (childSections.Any())
                    {
                        childSections.ForEach(cs => children.AddRange(cs));
                        break;
                    }
                }

                templates.Add(new ModelFormResponseTemplate(Project, Screen, group.Values, children));
                templates.Add(new ModelFormRequestTemplate(Project, Screen, group.Values, children));
            }

            return templates;
        }
    }
}
