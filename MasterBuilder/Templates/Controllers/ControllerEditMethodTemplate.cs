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
            var getPropertyMapping = new List<string>();
            var postPropertyMapping = new List<string>();
            foreach (var item in entity.Properties)
            {
                getPropertyMapping.Add($"                          {item.InternalName} = item.{item.InternalName}");
                postPropertyMapping.Add($"              {item.InternalName} = post.{item.InternalName}");
            }

            var get = $@"
        [HttpGet]
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
{string.Join(string.Concat(",", Environment.NewLine), getPropertyMapping)}
                            }})
                            .SingleOrDefaultAsync(p => p.Id == id.Value);
            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";


            var post = $@"
        [HttpPost]
        public async Task<IActionResult> {screen.InternalName}([FromBody]{screen.InternalName}Request post)
        {{
            if (post == null)
            {{
                return BadRequest();
            }}
            
            var newRecord = new {entity.InternalName}{{
{string.Join(string.Concat(",", Environment.NewLine), postPropertyMapping)}
            }};            

            _context.{entity.InternalNamePlural}.Add(newRecord);
            await _context.SaveChangesAsync();

            return Ok();
        }}";

            //http://benfoster.io/blog/aspnet-core-json-patch-partial-api-updates
            var patch = $@"
        [HttpPatch]
        public async Task<IActionResult> {screen.InternalName}([FromBody]JsonPatchDocument<{screen.InternalName}Request> patch)
        {{
            if (patch == null)
            {{
                return BadRequest();
            }}
            
            return Ok();
        }}";

            // todo delete

            return string.Concat(get, post, patch);
        }
    }
}
