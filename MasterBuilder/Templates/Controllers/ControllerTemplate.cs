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
        public static string FileName(string folder, Entity entity, Screen screen)
        {
            var controllerName = (entity != null ? entity.InternalName : screen.InternalName);

            return Path.Combine(folder, $"{controllerName}Controller.cs");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            var controllerName = (entity != null ? entity.InternalName : screen.InternalName);
            var usings = new List<string>();
            var methods = new StringBuilder();
            var classAttributes = @"[Route(""api/[controller]"")]";
                        
            if (entity != null && project.Screens != null)
            {
                foreach (var item in project.Screens.Where(p => p.EntityId.HasValue && p.EntityId.Value == entity.Id))
                {
                    switch (item.ScreenType)
                    {
                        case ScreenTypeEnum.Search:
                            if (!usings.Contains("using Microsoft.AspNetCore.JsonPatch;"))
                            {
                                usings.Add("using Microsoft.AspNetCore.JsonPatch;");
                            }
                            methods.Append(ControllerSearchMethodTemplate.Evaluate(project, entity, item));
                            break;
                        case ScreenTypeEnum.Edit:
                            methods.Append(ControllerEditMethodTemplate.Evaluate(project, entity, item));
                            break;
                        case ScreenTypeEnum.View:
                            break;
                        default:
                            break;
                    }

                    if (item.HasControllerCode)
                    {
                        methods.AppendLine(item.ControllerCode);
                    }

                    methods.AppendLine();
                }
            }

            if (screen != null)
            {
                if (screen.TemplateId.HasValue && screen.TemplateId.Value == new Guid("{79FEFA81-D6F7-4168-BCAF-FE6494DC3D72}"))
                {
                    methods.AppendLine(@"        public IActionResult Index()
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

                if (screen.HasControllerCode)
                {
                    methods.AppendLine(screen.ControllerCode);
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
{string.Join(Environment.NewLine, usings)}

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
{methods}
    }}
}}";
        }
    }
}
