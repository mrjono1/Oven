using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Services
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
        public string GetFileName() => $"{Entity.InternalName}Service.cs";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "Services" };

        /// <summary>
        /// Has Entity Actions
        /// </summary>
        public bool HasEntityActions => Project.Screens.Any(_ => _.EntityId == Entity.Id);

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var usings = new List<string>();
            var controllerActions = new List<string>();

            var fields = new List<string>
            {
                $@"        /// <summary>
        /// Database Context
        /// </summary>
        protected readonly IApplicationDbContext _context;"
            };
            var constructorParameters = new List<string>
            {
                $"IApplicationDbContext context"
            };
            var constructorFieldMappings = new List<string>
            {
                "_context = context;"
            };

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
                controllerActions.Add(new UpdateMethodTemplate(Project, formScreen, formSections).PutMethod());
                controllerActions.Add(new GetMethodTemplate(Project, formScreen, formSections).GetMethod());
                controllerActions.Add(new AddMethodTemplate(Project, formScreen, formSections).PostMethod());
                controllerActions.Add(new DeleteMethodTemplate(Project, Entity).DeleteMethod());
            }

            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using {Project.InternalName}.DataAccessLayer.Models;
using {Project.InternalName}.DataAccessLayer;
using {Project.InternalName}.DataAccessLayer.Entities;
using {Project.InternalName}.DataAccessLayer.Services.Contracts;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;

{string.Join(Environment.NewLine, usings.Distinct())}

namespace {Project.InternalName}.DataAccessLayer.Services
{{
    /// <summary>
    /// {Entity.Title} Service
    /// </summary>
    public class {Entity.InternalName}Service: I{Entity.InternalName}Service
    {{
{string.Join(Environment.NewLine, fields)}

        /// <summary>
        /// Constructor
        /// </summary>
        public {Entity.InternalName}Service({string.Join(string.Concat(",", Environment.NewLine, "          "), constructorParameters)})
        {{
            {string.Join(string.Concat(Environment.NewLine, "            "), constructorFieldMappings)}
        }}
{string.Join(Environment.NewLine, controllerActions)}
    }}
}}";
        }
    }
}
