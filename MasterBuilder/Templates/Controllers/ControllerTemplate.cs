using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Controllers
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
            return $"{Screen.InternalName}Controller.cs";
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

            // TODO: Build site map
            if (Screen != null)
            {
                if (Screen.Template == ScreenTemplate.Home)
                {
                    if (Project.ServerSideRendering)
                    {
                        methods.Add($@"                /// <summary>
        /// Home Index
        /// </summary>
        [HttpGet]
        [Route(""/"")]
        [Route(""/Home"")]
        public async Task<IActionResult> Index()
        {{
            
            var prerenderResult = await Request.BuildPrerender();
        
            ViewData[""SpaHtml""] = prerenderResult.Html; // our <app-root /> from Angular
            ViewData[""Styles""] = prerenderResult.Globals[""styles""]; // put styles in the correct place
            ViewData[""Scripts""] = prerenderResult.Globals[""scripts""]; // scripts (that were in our header)
            ViewData[""Meta""] = prerenderResult.Globals[""meta""]; // set our <meta> SEO tags
            ViewData[""Links""] = prerenderResult.Globals[""links""]; // set our <link rel=""canonical""> etc SEO tags
            ViewData[""TransferData""] = prerenderResult.Globals[""transferData""]; // our transfer data set to window.TRANSFER_CACHE = {{}};
            ViewData[""Title""] = prerenderResult.Globals[""title""]; // set our <title> from Angular
            
            return View();
        }}");
                    }
                    else
                    {
                        methods.Add($@"        /// <summary>
        /// Home Index
        /// </summary>
        [HttpGet]
        [Route(""/"")]
        [Route(""/Home"")]
        public IActionResult Index()
        {{
            ViewData[""Title""] = ""{Project.Title}"";
            return View();
        }}");
                    }

                     methods.Add($@"
        /// <summary>
        /// Sitemap Xml
        /// </summary>
        [HttpGet]
        [Route(""/sitemap.xml"")]
        public IActionResult SitemapXml()
        {{
            var xml = $@""<?xml version=\""""1.0\"""" encoding=\""""utf-8\""""?>
<sitemapindex xmlns=\""""http://www.sitemaps.org/schemas/sitemap/0.9\"""">
</sitemapindex>"";

            return Content(xml, ""text/xml"");
        }}
        
        /// <summary>
        /// Error page
        /// </summary>
        [Route(""/Error"")]
        public IActionResult Error()
        {{
            return View();
        }}");
                    if (Project.ServerSideRendering)
                    {
                        usings.Add($"using {Project.InternalName}.CoreModels;");
                        usings.Add($"using {Project.InternalName}.Extensions;");
                    }
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
    [Route(""api/[controller]"")]
    public class {Screen.InternalName}Controller : Controller
    {{
{string.Join(Environment.NewLine, privateFields)}

        /// <summary>
        /// Constructor
        /// </summary>
        public {Screen.InternalName}Controller({string.Join(string.Concat(",", Environment.NewLine, "          "), constructorParameters)})
        {{
            {string.Join(string.Concat(Environment.NewLine, "            "), constructorFieldMappings)}
        }}
{string.Join(Environment.NewLine, methods)}
    }}
}}";
        }
    }
}
