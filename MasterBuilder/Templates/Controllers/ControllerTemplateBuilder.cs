using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Controllers
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

            foreach (var entity in Project.Entities)
            {
                templates.Add(new ControllerTemplate(Project, entity, null));
            }

            // Create controllers for screens that dont have entities, they may want to do custom stuff server side
            foreach (var screen in Project.Screens.Where(p => !p.EntityId.HasValue))
            {
                // Create Controller & Models for UI
                templates.Add(new ControllerTemplate(Project, null, screen));
            }

            return templates;
        }
    }
}
