using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.React.Src.Modules
{
    /// <summary>
    /// Build Modules that include a reducer, actionTypes, and an action
    /// </summary>
    public class ModuleBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModuleBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>();

            foreach (var entity in Project.Entities)
            {
                templates.Add(new ActionTypesTemplate(Project, entity));
                templates.Add(new ActionsTemplate(Project, entity));
                templates.Add(new ReducerTemplate(Project, entity));
            }
            return templates;
        }
    }
}
