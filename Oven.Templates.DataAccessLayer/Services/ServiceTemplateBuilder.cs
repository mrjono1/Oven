using Oven.Request;
using System.Linq;
using System.Collections.Generic;
using Oven.Interfaces;

namespace Oven.Templates.DataAccessLayer.Services
{
    /// <summary>
    /// Service Template Builder
    /// </summary>
    public class ServiceTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceTemplateBuilder(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new AdministrationServiceTemplate(Project)
            };

            // Create Entity Service
            foreach (var entity in Project.Entities)
            {
                var service = new EntityServiceTemplate(Project, entity);
                if (service.HasEntityActions)
                {
                    templates.Add(service);
                }
            }

            return templates;
        }

        public IEnumerable<ITemplateBuilder> GetTemplateBuilders() => new ITemplateBuilder[] 
        {
            new Contracts.ContractTemplateBuilder(Project)
        };
    }
}
