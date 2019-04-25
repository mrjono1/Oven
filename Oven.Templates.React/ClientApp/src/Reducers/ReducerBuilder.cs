using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.React.ClientApp.Src.Reducers
{
    /// <summary>
    /// Build Reducers
    /// </summary>
    public class ReducerBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ReducerBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new IndexTemplate(Project),
                new EntityReducerTemplate(Project)
            };

            // TODO: Screen Reducers
            //foreach (var screen in Project.Screens)
            //{
            //    templates.Add(new PageReducerTemplate(Project, screen));
            //}
            return templates;
        }

        public IEnumerable<ITemplateBuilder> GetTemplateBuilders() => new ITemplateBuilder[] { };
    }
}
