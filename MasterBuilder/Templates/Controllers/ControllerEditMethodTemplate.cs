using System;
using System.Collections.Generic;
using System.Text;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Controllers
{
    public class ControllerEditMethodTemplate
    {
        internal static string Evaluate(Project project, Entity entity, Screen screen)
        {
            var propertyMapping = new List<string>();
            foreach (var item in entity.Properties)
            {
                propertyMapping.Add($"                          {item.InternalName} = item.{item.InternalName}");
            }

            return $@"
        [HttpGet(""[action]"")]
        public async Task<IActionResult> {screen.InternalName}(Guid? id)
        {{
            if (!id.HasValue)
            {{
                return NotFound();
            }}
            
            var result = await _context.{entity.InternalNamePlural}
                            .AsNoTracking()
                            .Select(item => new Models.{screen.InternalName}Response
                            {{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
                            }})
                            .SingleOrDefaultAsync(p => p.Id == id.Value);
            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";
        }
    }
}
