using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.Api.Controllers
{
    /// <summary>
    /// Controller Template
    /// </summary>
    public class EntityControllerTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityControllerTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"{Entity.InternalNamePlural}ApiController.cs";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "Controllers" };

        /// <summary>
        /// Has Entity Actions
        /// </summary>
        internal bool HasEntityActions => Project.Screens.Any(_ => _.EntityId == Entity.Id);

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var usings = new List<string>();
            var controllerActions = new List<string>();

            var privateFields = new List<string>();
            var constructorParameters = new List<string>();
            var constructorFieldMappings = new List<string>();
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
                        controllerActions.Add(ControllerSearchMethodTemplate.Evaluate(Project, Entity, screenSectionGroup.Screen, screenSectionGroup.ScreenSection));
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
                var controllerFormSectionMethodsPartial = new ControllerFormSectionMethodsPartial(Project, formScreen, formSections);
                controllerActions.Add(controllerFormSectionMethodsPartial.GetMethod());
                controllerActions.Add(controllerFormSectionMethodsPartial.PostMethod());
                controllerActions.Add(controllerFormSectionMethodsPartial.DeleteMethod());

                if (Project.UsePutForUpdate)
                {
                    controllerActions.Add(controllerFormSectionMethodsPartial.PutMethod());
                }
                else
                {
                    usings.Add("using Microsoft.AspNetCore.JsonPatch;");
                    controllerActions.Add(controllerFormSectionMethodsPartial.PatchMethod());
                }
            }

            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using {Project.InternalName}.Models;
using {Project.InternalName}.DataAccessLayer;
using {Project.InternalName}.DataAccessLayer.Entities;
using {Project.InternalName}.Services.Contracts;
using System.Diagnostics;
{string.Join(Environment.NewLine, usings.Distinct())}

namespace {Project.InternalName}.Controllers
{{
    /// <summary>
    /// Controller for the {Entity.Title} Entity
    /// </summary>
    [Route(""api/{Entity.InternalNamePlural}"")]
    public class {Entity.InternalNamePlural}ApiController : ControllerBase
    {{
{string.Join(Environment.NewLine, privateFields)}

        /// <summary>
        /// Constructor
        /// </summary>
        public {Entity.InternalNamePlural}ApiController({string.Join(string.Concat(",", Environment.NewLine, "          "), constructorParameters)})
        {{
            {string.Join(string.Concat(Environment.NewLine, "            "), constructorFieldMappings)}
        }}
{string.Join(Environment.NewLine, controllerActions)}
    }}
}}";
        }
    }
}
