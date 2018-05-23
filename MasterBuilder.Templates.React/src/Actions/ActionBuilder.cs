using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.React.Src.Actions
{
    /// <summary>
    /// Build Actions
    /// </summary>
    public class ActionBuilder : ITemplateBuilder
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionBuilder(Project project)
        {
            Project = project;
        }

        public IEnumerable<ITemplate> GetTemplates()
        {
            var templates = new List<ITemplate>
            {
                new IndexTsxTemplate(Project),
                new TodoTsxTemplate(Project)
            };

            return templates;
        }
    }
}
