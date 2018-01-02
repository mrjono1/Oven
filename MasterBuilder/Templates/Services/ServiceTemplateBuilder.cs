using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.Services
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
        /// Get service templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new ExportServiceTemplate(Project)
            };

            // TODO: Create application setting service

            if (Project.Services != null)
            {
                foreach (var service in Project.Services)
                {
                    switch (service.ServiceType)
                    {
                        case ServiceTypeEnum.WebService:
                            templates.Add(new WebServiceServiceTemplate(Project, service, service.WebService));
                            break;
                        case ServiceTypeEnum.ExportService:
                            break;
                        default:
                            break;
                    }
                }
            }

            return templates;
        }

        /// <summary>
        /// Get service names
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetServiceNames()
        {
            var names = new List<string>
            {
                "ExportService"
            };

            // TODO: Create application setting service

            if (Project.Services != null)
            {
                foreach (var service in Project.Services)
                {
                    switch (service.ServiceType)
                    {
                        case ServiceTypeEnum.WebService:
                            names.Add(new WebServiceServiceTemplate(Project, service, service.WebService).GetClassName());
                            break;
                        case ServiceTypeEnum.ExportService:
                            break;
                        default:
                            break;
                    }
                }
            }

            return names;
        }
    }
}
