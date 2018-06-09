using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Controllers
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
            return $@"using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Jono.Controllers
{{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {{
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {{
            return View();
        }}

        /// <summary>
        /// Error
        /// </summary>
        public IActionResult Error()
        {{
            ViewData[""RequestId""] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }}
    }}
}}";
        }
    }
}
