using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Containers.Html
{
    /// <summary>
    /// Container Html Template Builder
    /// </summary>
    public class ContainerHtmlTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constuctor
        /// </summary>
        public ContainerHtmlTemplateBuilder(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get Templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();

            foreach (var screen in Project.Screens)
            {
                templates.Add(new ContainerHtmlTemplate(Project, screen));
            }

            return templates;
        }
    }
}
