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

            if (Project.WebServices != null)
            {
                foreach (var webService in Project.WebServices)
                {
                    templates.Add(new WebServiceServiceTemplate(Project, webService));
                }
            }

            return templates;
        }

        public IEnumerable<string> GetServiceNames()
        {
            var names = new List<string>
            {
                "ExportService"
            };

            // TODO: Create application setting service

            if (Project.WebServices != null)
            {
                foreach (var webService in Project.WebServices)
                {
                    names.Add(new WebServiceServiceTemplate(Project, webService).GetClassName());
                }
            }

            return names;
        }
    }
}
