using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.React.Src
{
    /// <summary>
    /// Src Components
    /// </summary>
    public class SrcBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public SrcBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new AppTemplate(Project),
                new DevIndexTemplate(Project),
                new IndexTemplate(Project),
                new LayoutTemplate(Project),
                new ManifestTemplate(Project),
                new MenuTemplate(Project),
                new RestProviderTemplate(Project),
                new RoutesTemplate(Project)
            };

            return templates;
        }
    }
}
