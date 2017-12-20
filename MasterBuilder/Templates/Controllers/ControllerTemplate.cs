using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MasterBuilder.Templates.Controllers
{
    /// <summary>
    /// Controller Template
    /// </summary>
    public class ControllerTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project">Required</param>
        /// <param name="entity">Entity or Screen must be provided</param>
        /// <param name="screen">Entity or Screen must be provided</param>
        public ControllerTemplate(Project project, Entity entity, Screen screen)
        {
            Project = project;
            Entity = entity;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            var controllerName = (Entity != null ? Entity.InternalName : Screen.InternalName);

            return $"{controllerName}Controller.cs";
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
            var controllerName = (Entity != null ? Entity.InternalName : Screen.InternalName);
            var usings = new List<string>();
            var methods = new List<string>();
            var classAttributes = @"[Route(""api/[controller]"")]";
            
            if (Entity != null && Project.Screens != null)
            {
                var sections = (from s in Project.Screens
                                where s.ScreenSections != null
                                from ss in s.ScreenSections
                                where ss.EntityId == Entity.Id
                                select new
                                {
                                    Screen = s,
                                    ScreenSection = ss
                                });
                foreach (var item in sections)
                {
                    switch (item.ScreenSection.ScreenSectionType)
                    {
                        case ScreenSectionTypeEnum.Search:
                            usings.Add("using Microsoft.AspNetCore.JsonPatch;");
                            methods.Add(ControllerSearchMethodTemplate.Evaluate(Project, Entity, item.Screen, item.ScreenSection));
                            break;
                        case ScreenSectionTypeEnum.Form:
                            methods.Add(ControllerEditMethodTemplate.Evaluate(Project, Entity, item.Screen, item.ScreenSection));
                            break;
                        default:
                            break;
                    }
                }
            }

            // TODO: Build site map
            if (Screen != null)
            {
                if (Screen.TemplateId.HasValue && Screen.TemplateId.Value == new Guid("{79FEFA81-D6F7-4168-BCAF-FE6494DC3D72}"))
                {
                    methods.Add($@"        [HttpGet]
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
    }}

    [HttpGet]
    [Route(""sitemap.xml"")]
    public IActionResult SitemapXml()
    {{
        var xml = $@""<?xml version=\""""1.0\"""" encoding=\""""utf-8\""""?>
<sitemapindex xmlns=\""""http://www.sitemaps.org/schemas/sitemap/0.9\"""">
    <sitemap>
        <loc>http://localhost:4251/home</loc>
        <lastmod>{{DateTime.Now.ToString(""yyyy-MM-dd"")}}</lastmod>
    </sitemap>
    <sitemap>
        <loc>http://localhost:4251/counter</loc>
        <lastmod>{{DateTime.Now.ToString(""yyyy-MM-dd"")}}</lastmod>
    </sitemap>
</sitemapindex>"";

        return Content(xml, ""text/xml"");

    }}

    public IActionResult Error()
    {{
        return View();
    }}");
                    usings.Add($"using {Project.InternalName}.CoreModels;");
                    usings.Add($"using {Project.InternalName}.Extensions;");
                    classAttributes = null;
                }

                if (!string.IsNullOrWhiteSpace(Screen.ControllerCode))
                {
                    methods.Add(Screen.ControllerCode);
                }
            } 

            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using {Project.InternalName}.Models;
using {Project.InternalName}.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
{string.Join(Environment.NewLine, usings.Distinct())}

namespace {Project.InternalName}.Controllers
{{

    /// <summary>
    /// Controller for the {(Entity != null ? string.Concat(Entity.InternalName, " Entity") : string.Concat(Screen.InternalName, " Screen"))}
    /// </summary>
    {classAttributes}
    public class {controllerName}Controller : Controller
    {{
        /// <summary>
        /// Database Context
        /// </summary>
        private readonly {Project.InternalName}Context _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public {controllerName}Controller({Project.InternalName}Context context)
        {{
            _context = context;
        }}
{string.Join(Environment.NewLine, methods)}
    }}
}}";
        }
    }
}
