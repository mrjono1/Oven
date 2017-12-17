using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Html
{
    public class ContainerHtmlTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        public ContainerHtmlTemplateBuilder(Project project)
        {
            Project = project;
        }

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
