using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.React.ClientApp.Public
{
    /// <summary>
    /// Public Builder
    /// </summary>
    public class PublicBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public PublicBuilder(Project project)
        {
            Project = project;
        }


        public IEnumerable<ITemplateBuilder> GetTemplateBuilders() => new ITemplateBuilder[] { };

        public IEnumerable<ITemplate> GetTemplates() => new ITemplate[] {
                new IndexTemplate(Project),
                new ManifestTemplate(Project)
        };
    }
}
