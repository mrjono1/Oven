using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

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

            return templates;
        }
    }
}
