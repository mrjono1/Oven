using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.DataAccessLayer.EntityTypeConfigurations
{
    /// <summary>
    /// Entity Type Configuration Builder
    /// </summary>
    public class EntityTypeConfigTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityTypeConfigTemplateBuilder(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get entity type config templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();

            foreach (var entity in Project.Entities)
            {
                templates.Add(new EntityTypeConfigTemplate(Project, entity));
            }

            return templates;
        }
    }
}
