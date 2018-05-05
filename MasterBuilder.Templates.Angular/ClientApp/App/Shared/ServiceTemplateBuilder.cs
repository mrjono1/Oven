using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Shared
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
            var templates = new List<ITemplate>();

            foreach (var screen in Project.Screens)
            {
                templates.Add(new ServiceTemplate(Project, screen));
            }
            
            templates.Add(new LinkServiceTemplate());
            templates.Add(new HttpErrorServiceTemplate());
            templates.Add(new PendingChangesGuardTemplate());

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
                        case ServiceType.WebService:
                            names.Add($"{service.InternalName}Service");
                            break;
                        case ServiceType.ExportService:
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