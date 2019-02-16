using Oven.Helpers;
using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;

namespace Oven.Templates.Api.Services.Contracts
{
    /// <summary>
    /// Service Contract Template Builder
    /// </summary>
    public class ServiceContractTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceContractTemplateBuilder(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get service contract templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new ExportServiceContractTemplate(Project)
            };

            // TODO: Create application setting service

            if (Project.Services != null)
            {
                foreach (var service in Project.Services)
                {
                    switch (service.ServiceType)
                    {
                        case ServiceType.WebService:
                            templates.Add(new WebServiceServiceContractTemplate(Project, service, service.WebService));
                            break;
                        case ServiceType.ExportService:
                            break;
                        default:
                            break;
                    }
                }
            }

            return templates;
        }
    }
}
