using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Controllers
{
    public class ControllerTemplate
    {
        public static string FileName(string folder, Screen screen)
        {
            return Path.Combine(folder, $"{screen.InternalName}Controller.cs");
        }

        public static string Evaluate(Project project, Screen screen)
        {


            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using {project.InternalName}.Models;

namespace {project.InternalName}.Controllers
{{
    [Route(""api/[controller]"")]
    public class {screen.InternalName}Controller : Controller
    {{
{(screen.HasControllerCode ? screen.ControllerCode : null)}
    }}
}}";

            /*        [HttpGet(""[action]"")]
        public IEnumerable<{screen.InternalName}Model> {screen.InternalName}s()
        {{
            return new {screen.InternalName}Model[] {{ }};
        }}*/
        }
    }
}
