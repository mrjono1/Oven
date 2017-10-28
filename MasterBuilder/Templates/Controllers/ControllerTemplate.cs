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
        public static string FileName(string folder, Entity entity)
        {
            return Path.Combine(folder, $"{entity.InternalName}Controller.cs");
        }

        public static string Evaluate(Project project, Entity entity)
        {

            var methods = new StringBuilder();

            if (project.Screens != null)
            {
                foreach (var screen in project.Screens.Where(p => p.EntityId.HasValue && p.EntityId.Value == entity.Id))
                {
                    switch (screen.ScreenType)
                    {
                        case ScreenTypeEnum.Search:
                            methods.Append(ControllerSearchMethodTemplate.Evaluate(project, entity, screen));
                            break;
                        case ScreenTypeEnum.Edit:
                            break;
                        case ScreenTypeEnum.View:
                            break;
                        default:
                            break;
                    }
                    methods.AppendLine();
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

namespace {project.InternalName}.Controllers
{{
    [Route(""api/[controller]"")]
    public class {entity.InternalName}Controller : Controller
    {{
    
        private readonly {project.InternalName}Context _context;

        public {entity.InternalName}Controller({project.InternalName}Context context)
        {{
            _context = context;
        }}
{methods}
    }}
}}";
        }
    }
}
