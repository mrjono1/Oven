using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Api.Controllers
{
    /// <summary>
    /// Controller Template
    /// </summary>
    public class ControllerTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ControllerTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Screen.InternalName}ApiController.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Controllers" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var usings = new List<string>();
            var methods = new List<string>();
            var hasServerFunction = false;

            var privateFields = new List<string>
            {
                $@"        /// <summary>
        /// Database Context
        /// </summary>
        private readonly ApplicationDbContext _context;"
            };
            var constructorParameters = new List<string>
            {
                $"ApplicationDbContext context"
            };
            var constructorFieldMappings = new List<string>
            {
                "_context = context;"
            };

            var referenceFormFields = new List<FormField>();
            var formSections = new List<ScreenSection>();
            foreach (var screenSection in Screen.ScreenSections)
            {
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionType.Search:
                        methods.Add(ControllerSearchMethodTemplate.Evaluate(Project, Screen, screenSection));
                        break;

                    case ScreenSectionType.Form:
                        formSections.Add(screenSection);
                        referenceFormFields.AddRange(screenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyType.ReferenceRelationship));
                        break;

                    default:
                        break;
                }

                if (screenSection.MenuItems != null)
                {
                    foreach (var menuItem in screenSection.MenuItems)
                    {
                        switch (menuItem.MenuItemType)
                        {
                            case MenuItemType.ServerFunction:
                                hasServerFunction = true;
                                methods.Add($@"        /// <summary>
        /// Menu Item {menuItem.Title} called function
        /// </summary>
        [HttpGet(""{Screen.InternalName}{screenSection.InternalName}{menuItem.InternalName}/{{id}}"")]
        public async Task<IActionResult> {Screen.InternalName}{screenSection.InternalName}{menuItem.InternalName}(Guid id)
        {{
            {menuItem.ServerCode}
        }}");
                                break;
                        }
                    }
                }
            }

            if (formSections.Any())
            {
                var controllerFormSectionMethodsPartial = new ControllerFormSectionMethodsPartial(Project, Screen, formSections);
                methods.Add(controllerFormSectionMethodsPartial.GetMethod());
                methods.Add(controllerFormSectionMethodsPartial.PostMethod());

                if (Project.UsePutForUpdate)
                {
                    methods.Add(controllerFormSectionMethodsPartial.PutMethod());
                }
                else
                {
                    usings.Add("using Microsoft.AspNetCore.JsonPatch;");
                    methods.Add(controllerFormSectionMethodsPartial.PatchMethod());
                }
            }

            foreach (var referenceFormField in referenceFormFields)
            {
                var referenceMethod = ControllerReferenceMethodTemplate.Evaluate(Project, Screen, referenceFormField);
                if (!string.IsNullOrEmpty(referenceMethod))
                {
                    methods.Add(referenceMethod);
                }
            }


            if (hasServerFunction)
            {
                var serviceNames = new Services.ServiceTemplateBuilder(Project).GetServiceNames();
                foreach (var serviceName in serviceNames)
                {
                    privateFields.Add($@"        /// <summary>
        /// {serviceName}
        /// </summary>
        private readonly I{serviceName} _{serviceName.Camelize()};");

                    constructorParameters.Add($"I{serviceName} {serviceName.Camelize()}");
                    constructorFieldMappings.Add($"_{serviceName.Camelize()} = {serviceName.Camelize()};");
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
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
{string.Join(Environment.NewLine, usings.Distinct())}

namespace {Project.InternalName}.Controllers
{{

    /// <summary>
    /// Controller for the {Screen.Title} Screen
    /// </summary>
    [Route(""api/{Screen.InternalName}"")]
    public class {Screen.InternalName}ApiController : Controller
    {{
{string.Join(Environment.NewLine, privateFields)}

        /// <summary>
        /// Constructor
        /// </summary>
        public {Screen.InternalName}ApiController({string.Join(string.Concat(",", Environment.NewLine, "          "), constructorParameters)})
        {{
            {string.Join(string.Concat(Environment.NewLine, "            "), constructorFieldMappings)}
        }}
{string.Join(Environment.NewLine, methods)}
    }}
}}";
        }
    }
}
