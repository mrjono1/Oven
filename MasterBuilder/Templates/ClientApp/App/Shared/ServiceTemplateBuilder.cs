using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Shared
{
    public class ServiceTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        public ServiceTemplateBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();

            foreach (var entity in Project.Entities)
            {
                templates.Add(new ServiceTemplate(Project, entity));
            }

            // Link Service template currently added elsewhere for speed reasons
            //templates.Add(new LinkServiceTemplate());

            return templates;
        }
    }
}
