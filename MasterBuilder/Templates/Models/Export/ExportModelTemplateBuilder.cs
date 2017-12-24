using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Models.Export
{
    /// <summary>
    /// Export Model Template Builder (recursive)
    /// </summary>
    public class ExportModelTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;
        private readonly Entity Entity;
        private readonly Entity RootEntity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExportModelTemplateBuilder(Project project, Entity rootEntity, Entity entity)
        {
            Project = project;
            RootEntity = rootEntity;
            Entity = entity;
        }

        /// <summary>
        /// Get templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new ExportModelTemplate(Project, RootEntity, Entity)
            };

            var childEntities = (from entity in Project.Entities
                                 from property in entity.Properties
                                 where property.Type == PropertyTypeEnum.ParentRelationship &&
                                 property.ParentEntityId.HasValue &&
                                 property.ParentEntityId == Entity.Id
                                 select entity).ToArray();

            foreach (var childEntity in childEntities)
            {
                templates.AddRange(new ExportModelTemplateBuilder(Project, RootEntity, childEntity).GetTemplates());
            }            

            return templates;
        }
    }
}
