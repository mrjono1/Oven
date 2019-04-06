using MongoDB.Bson;
using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Models.Export
{
    /// <summary>
    /// Model Template Builder
    /// </summary>
    public class ModelTemplateBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelTemplateBuilder(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get model templates
        /// </summary>
        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();

            if (Project.Id == Project.KitchenId)
            {
                var projectEntity = Project.Entities.SingleOrDefault(a => a.Id == new ObjectId("{5ca869596668b25914b67e6e}"));
                templates.AddRange(new ExportModelTemplateBuilder(Project, projectEntity, projectEntity).GetTemplates());
            }

            return templates;
        }

        public IEnumerable<ITemplateBuilder> GetTemplateBuilders() => new ITemplateBuilder[] { };
    }
}
