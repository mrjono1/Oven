using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.React.Src.Containers
{
    /// <summary>
    /// Build Containers
    /// </summary>
    public class ContainerBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContainerBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                //new AdminMenuTemplate(Project)
            };

            return templates;
        }
    }
}
