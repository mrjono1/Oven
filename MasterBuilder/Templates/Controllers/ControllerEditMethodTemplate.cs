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
                getPropertyMapping.Add($"                           {item.InternalName} = item.{item.InternalName}");
                postPropertyMapping.Add($"                {item.InternalName} = post.{item.InternalName}");
            }

            var get = $@"
        [HttpGet(""{screen.InternalName}/{{id}}"")]
        public async Task<IActionResult> {screen.InternalName}(Guid id)
        {{
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (id == Guid.Empty)
            {{
                return NotFound();
            }}
            
            var result = await _context.{entity.InternalNamePlural}
                            .AsNoTracking()
                            .Select(item => new Models.{screen.InternalName}Response
                            {{
{string.Join(string.Concat(",", Environment.NewLine), getPropertyMapping)}
                            }})
                            .SingleOrDefaultAsync(p => p.Id == id);
            if (result == null)
            {{
                return NotFound();
            }}

            return Ok(result);
        }}";


            var post = $@"
        [HttpPost(""{screen.InternalName}"")]
        public async Task<IActionResult> {screen.InternalName}([FromBody]{screen.InternalName}Request post)
        {{
            if (post == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
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
        [HttpPatch(""{screen.InternalName}/{{id}}"")]
        public async Task<IActionResult> {screen.InternalName}([FromRoute]Guid id, [FromBody]JsonPatchDocument<{screen.InternalName}Request> patch)
        {{
            if (patch == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            if (!patch.Operations.Any())
            {{
                return Ok();
            }}

            var entity = await _context.{entity.InternalNamePlural}.FindAsync(id);
            
            // do stuff
            foreach(var operation in path.Operations)
            {{
                
            }}

            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            await _context.SaveChangesAsync();
            
            return Ok();
        }}";

            // todo delete

            return string.Concat(get, post, patch);
        }
    }
}
