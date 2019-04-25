using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.React.ClientApp.Src.Components
{
    /// <summary>
    /// Build Components
    /// </summary>
    public class ComponentBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new CreateButtonTemplate(Project)
            };

            return templates;
        }

        public IEnumerable<ITemplateBuilder> GetTemplateBuilders() => new ITemplateBuilder[] { };
    }
}
