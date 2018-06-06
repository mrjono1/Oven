using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.React.Src.Containers
{
    /// <summary>
    /// Build Containers
    /// </summary>
    public class ContainerBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContainerBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new App.AppTemplate(Project)
            };

            foreach (var screen in Project.Screens)
            {
                templates.Add(new IndexTemplate(Project, screen));
            }

            return templates;
        }
    }
}
