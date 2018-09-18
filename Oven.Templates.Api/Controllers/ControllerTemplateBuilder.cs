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
            var templates = new List<ITemplate>();
            
            // Create Enity Controllers
            foreach (var entity in Project.Entities)
            {
                var controller = new EntityControllerTemplate(Project, entity);
                if (controller.HasEntityActions)
                {
                    templates.Add(controller);
                }
            }

            // Create Controller & Models for UI
            // Non entity controller actions
            //foreach (var screen in Project.Screens)
            //{
            //    templates.Add(new ControllerTemplate(Project, screen));
            //}

            return templates;
        }
    }
}
