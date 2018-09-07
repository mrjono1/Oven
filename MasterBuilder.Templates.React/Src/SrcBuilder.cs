using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.React.Src
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
                new IndexTemplate(Project),
                new LayoutTemplate(Project),
                new ManifestTemplate(Project)
            };

            return templates;
        }
    }
}
