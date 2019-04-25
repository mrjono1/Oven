using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.React.ClientApp
{
    /// <summary>
    /// Client App Containers
    /// </summary>
    public class ClientAppBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientAppBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplateBuilder> GetTemplateBuilders()
        {
            var templateBuilders = new List<ITemplateBuilder>
            {
                new Src.SrcBuilder(Project),
                new Public.PublicBuilder(Project)
            };

            return templateBuilders;
        }

        public IEnumerable<ITemplate> GetTemplates() => 
            new List<ITemplate>() {
                new PackageJsonTemplate(Project),
                new TsConfigTemplate(Project)
            };
    }
}
