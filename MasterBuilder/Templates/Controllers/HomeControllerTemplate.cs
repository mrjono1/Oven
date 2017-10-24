using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Controllers
{
    public class HomeControllerTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(folder, "HomeController.cs");
        }

        public static string Evaluate(Project project)
        {
            return $@"using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace {project.InternalName}.Controllers
{{
    public class HomeController : Controller
    {{
        public IActionResult Index()
        {{
            return View();
        }}

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
