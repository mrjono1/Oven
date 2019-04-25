using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.React.Pages
{
    /// <summary>
    /// Pages Containers
    /// </summary>
    public class PagesBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public PagesBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplateBuilder> GetTemplateBuilders() => new List<ITemplateBuilder>();

        public IEnumerable<ITemplate> GetTemplates() => new ITemplate[] {
            new ViewImportsTemplate(Project),
            new ErrorTemplate(Project),
            new ErrorCsTemplate(Project)
        };
    }
}
