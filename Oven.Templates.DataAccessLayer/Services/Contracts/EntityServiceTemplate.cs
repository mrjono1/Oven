using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Services.Contracts
{
    /// <summary>
    /// Controller Template
    /// </summary>
    public class EntityServiceTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityServiceTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"I{Entity.InternalName}Service.cs";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "Services", "Contracts" };

        /// <summary>
        /// Has Entity Actions
        /// </summary>
        internal bool HasEntityActions => Project.Screens.Any(_ => _.EntityId == Entity.Id);

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var controllerActions = new List<string>();


            var referenceFormFields = new List<FormField>();
            var formSections = new List<ScreenSection>();
            Screen formScreen = null;
            var screenSectionGrouped = (from screen in Project.Screens
                                  from screenSection in screen.ScreenSections
                                  where screenSection.EntityId == Entity.Id
                                  select new { Screen = screen, ScreenSection = screenSection });

            foreach (var screenSectionGroup in screenSectionGrouped)
            {
                switch (screenSectionGroup.ScreenSection.ScreenSectionType)
                {
                    case ScreenSectionType.Search:
                        controllerActions.Add(SearchMethodTemplate.Evaluate(Project, Entity, screenSectionGroup.Screen, screenSectionGroup.ScreenSection));
                        break;

                    case ScreenSectionType.Form:
                        // TODO: Support more than one Form Screen
                        formScreen = screenSectionGroup.Screen;
                        formSections.Add(screenSectionGroup.ScreenSection);
                        break;

                    default:
                        break;
                }
            }

            if (formSections.Any())
            {
                var controllerFormSectionMethodsPartial = new AddUpdateMethodTemplate(Project, formScreen);
                controllerActions.Add(new GetMethodTemplate(Project, formScreen, formSections).GetMethod());
                controllerActions.Add(controllerFormSectionMethodsPartial.AddMethod());
                controllerActions.Add(new DeleteMethodTemplate(Project, Entity).DeleteMethod());
                controllerActions.Add(controllerFormSectionMethodsPartial.UpdateMethod());
            }

            return $@"using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using {Project.InternalName}.DataAccessLayer.Models;

namespace {Project.InternalName}.DataAccessLayer.Services.Contracts
{{
    /// <summary>
    /// {Entity.Title} Service Interface
    /// </summary>
    public interface I{Entity.InternalName}Service
    {{
{string.Join(Environment.NewLine, controllerActions)}
    }}
}}";
        }
    }
}
