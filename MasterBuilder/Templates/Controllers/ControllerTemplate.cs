using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            if (screen != null)
            {
                if (screen.TemplateId.HasValue && screen.TemplateId.Value == new Guid("{79FEFA81-D6F7-4168-BCAF-FE6494DC3D72}"))
                {
                    methods.Add(@"        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData[""RequestId""] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }");
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
{classAttributes}
    public class {controllerName}Controller : Controller
    {{
        private readonly {project.InternalName}Context _context;

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
