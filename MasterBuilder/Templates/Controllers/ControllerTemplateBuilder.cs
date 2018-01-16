using MasterBuilder.Interfaces;
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
            
            foreach (var screen in Project.Screens)
            {
                // Create Controller & Models for UI
                templates.Add(new ControllerTemplate(Project, screen));
            }

            return templates;
        }
    }
}
