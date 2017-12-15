using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MasterBuilder.Templates.Controllers
{
    public class ControllerTemplate
    {
        public static string FileName(string folder, Entity entity, Request.Screen screen)
        {
            var controllerName = (entity != null ? entity.InternalName : screen.InternalName);

            return Path.Combine(folder, $"{controllerName}Controller.cs");
        }

        public static string Evaluate(Project project, Entity entity, Request.Screen screen)
        {
            var controllerName = (entity != null ? entity.InternalName : screen.InternalName);
            var usings = new List<string>();
            var methods = new List<string>();
            var classAttributes = @"[Route(""api/[controller]"")]";
            
            if (entity != null && project.Screens != null)
            {
                var sections = (from s in project.Screens
                                where s.ScreenSections != null
                                from ss in s.ScreenSections
                                where ss.EntityId == entity.Id
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
                            methods.Add(ControllerSearchMethodTemplate.Evaluate(project, entity, item.Screen, item.ScreenSection));
                            break;
                        case ScreenSectionTypeEnum.Form:
                            methods.Add(ControllerEditMethodTemplate.Evaluate(project, entity, item.Screen, item.ScreenSection));
                            break;
                        default:
                            break;
                    }
                }
            }

            // TODO: Build site map
            if (screen != null)
            {
                if (screen.TemplateId.HasValue && screen.TemplateId.Value == new Guid("{79FEFA81-D6F7-4168-BCAF-FE6494DC3D72}"))
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
                    usings.Add($"using {project.InternalName}.CoreModels;");
                    usings.Add($"using {project.InternalName}.Extensions;");
                    classAttributes = null;
                }

                if (!string.IsNullOrWhiteSpace(screen.ControllerCode))
                {
                    methods.Add(screen.ControllerCode);
                }
            } 

            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using {project.InternalName}.Models;
using {project.InternalName}.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
{string.Join(Environment.NewLine, usings.Distinct())}

namespace {project.InternalName}.Controllers
{{

    /// <summary>
    /// Controller for the {(entity != null ? string.Concat(entity.InternalName, " Entity") : string.Concat(screen.InternalName, " Screen"))}
    /// </summary>
    {classAttributes}
    public class {controllerName}Controller : Controller
    {{
        /// <summary>
        /// Database Context
        /// </summary>
        private readonly {project.InternalName}Context _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public {controllerName}Controller({project.InternalName}Context context)
        {{
            _context = context;
        }}
{string.Join(Environment.NewLine, methods)}
    }}
}}";
        }
    }
}
