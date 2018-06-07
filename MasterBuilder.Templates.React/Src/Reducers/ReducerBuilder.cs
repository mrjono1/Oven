using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.React.Src.Reducers
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
                new IndexTemplate(Project)
            };

            foreach (var screen in Project.Screens)
            {
                templates.Add(new PageReducerTemplate(Project, screen));
            }
            return templates;
        }
    }
}
