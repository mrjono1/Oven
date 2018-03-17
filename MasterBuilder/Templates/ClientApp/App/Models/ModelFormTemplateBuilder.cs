using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    /// <summary>
    /// Client app model template builder
    /// </summary>
    public class ModelFormTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelFormTemplateBuilder(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();
            var defaultScreenSection = new ScreenSection();

            var groupedFormScreenSections = (from ss in Screen.ScreenSections
                                             where ss.ScreenSectionType == ScreenSectionType.Form
                                             select ss)
                                   .GroupBy(ss => ss.ParentScreenSection ?? defaultScreenSection)
                                   .Select(a => new { a.Key, Values = a.ToArray() }).ToDictionary(t => t.Key, t => t.Values);

            foreach (var group in groupedFormScreenSections)
            {
                var children = new List<ScreenSection>();

                foreach (var item in group.Value)
                {
                    if (groupedFormScreenSections.ContainsKey(item))
                    {
                        children.AddRange(groupedFormScreenSections[item]);
                        break;
                    }
                }

                templates.Add(new FormTemplate(Project, Screen, group.Value, children));
            }

            return templates;
        }
    }
}