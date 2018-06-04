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
                new App.IndexTemplate(Project),
                new App.Styles.AppScssTemplate(Project),
                new App.Styles.Fonts.RobotoScssTemplate(Project),
                new App.Styles.ThirdParty.NormalizeScssTemplate(Project),
                new App.Styles.FontScssTemplate(Project),
                new App.Styles.LinksScssTemplate(Project),
                new App.Styles.TypographyScssTemplate(Project),
                new App.Styles.VariablesScssTemplate(Project)
            };

            foreach (var screen in Project.Screens)
            {
                templates.Add(new IndexJsTemplate(Project, screen));
            }

            return templates;
        }
    }
}
