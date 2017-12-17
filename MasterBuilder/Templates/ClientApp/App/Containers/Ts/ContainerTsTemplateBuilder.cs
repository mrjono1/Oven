using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Ts
{
    public class ContainerTsTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        public ContainerTsTemplateBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();

            foreach (var screen in Project.Screens)
            {
                templates.Add(new ContainerTsTemplate(Project, screen));
            }

            return templates;
        }
    }
}
