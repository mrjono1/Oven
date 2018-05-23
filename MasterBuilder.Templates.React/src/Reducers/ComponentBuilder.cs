using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.React.Src.Reducers
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
                new IndexTsTemplate(Project),
                new CreateReducerTemplate(Project),
                new TodoTemplate(Project)
            };

            return templates;
        }
    }
}
