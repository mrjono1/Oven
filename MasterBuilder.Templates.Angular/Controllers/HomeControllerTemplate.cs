using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Angular.Controllers
{
    /// <summary>
    /// Home Controller Template
    /// </summary>
    public class HomeControllerTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public HomeControllerTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"HomeController.cs";
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

            // TODO: Build site map

            var indexMethodContent = string.Empty;
            if (Project.ServerSideRendering)
            {
                indexMethodContent = $@"var prerenderResult = await Request.BuildPrerender();
        
            ViewData[""SpaHtml""] = prerenderResult.Html; // our <app-root /> from Angular
            ViewData[""Styles""] = prerenderResult.Globals[""styles""]; // put styles in the correct place
            ViewData[""Scripts""] = prerenderResult.Globals[""scripts""]; // scripts (that were in our header)
            ViewData[""Meta""] = prerenderResult.Globals[""meta""]; // set our <meta> SEO tags
            ViewData[""Links""] = prerenderResult.Globals[""links""]; // set our <link rel=""canonical""> etc SEO tags
            ViewData[""TransferData""] = prerenderResult.Globals[""transferData""]; // our transfer data set to window.TRANSFER_CACHE = {{}};
            ViewData[""Title""] = prerenderResult.Globals[""title""]; // set our <title> from Angular
            
            return View();";
            }
            else
            {
                indexMethodContent = $@"ViewData[""Title""] = ""{Project.Title}"";
            return View();";
            }

            if (Project.ServerSideRendering)
            {
                usings.Add($"using {Project.InternalName}.CoreModels;");
                usings.Add($"using {Project.InternalName}.Extensions;");
                usings.Add(string.Empty);
            }
            return $@"using Microsoft.AspNetCore.Mvc;
{string.Join(Environment.NewLine, usings.Distinct())}
namespace {Project.InternalName}.Controllers
{{

    /// <summary>
    /// Home Controller for MVC Routes
    /// </summary>
    public class HomeController : Controller
    {{
        /// <summary>
        /// Home Index
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {{
            {indexMethodContent}
        }}

        /// <summary>
        /// Sitemap Xml
        /// </summary>
        [HttpGet]
        [Route(""sitemap.xml"")]
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
        public IActionResult Error()
        {{
            return View();
        }}
    }}
}}";
        }
    }
}
