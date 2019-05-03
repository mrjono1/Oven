using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.Api.Controllers
{
    /// <summary>
    /// Controler Template Builder
    /// </summary>
    public class ControllerTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControllerTemplateBuilder(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get entity templates
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new AdministrationControllerTemplate(Project)
            };

            // Create Enity Controllers
            foreach (var entity in Project.Entities)
            {
                var controller = new EntityControllerTemplate(Project, entity);
                if (controller.HasEntityActions)
                {
                    templates.Add(controller);
                }
            }

            return templates;
        }

        public IEnumerable<ITemplateBuilder> GetTemplateBuilders() => new ITemplateBuilder[]{};
    }
}
