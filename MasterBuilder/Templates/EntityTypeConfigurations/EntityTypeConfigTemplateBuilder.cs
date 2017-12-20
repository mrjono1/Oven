using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Collections.Generic;

namespace MasterBuilder.Templates.EntityTypeConfigurations
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
        /// Get entity templates
        /// </summary>
        /// <returns></returns>
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
