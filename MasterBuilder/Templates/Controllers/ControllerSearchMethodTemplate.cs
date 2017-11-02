using System;
using System.Collections.Generic;
using System.Text;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Controllers
{
    public class ControllerSearchMethodTemplate
    {
        internal static string Evaluate(Project project, Entity entity, Screen screen)
        {
            var method = new StringBuilder();


            var propertyMapping = new List<string>();
            foreach (var item in entity.Properties)
            {
                propertyMapping.Add($"                        {item.InternalName} = item.{item.InternalName}");
            }

            return $@"
        [HttpPost(""{screen.InternalName}"")]
        public async Task<IActionResult> {screen.InternalName}([FromBody]{screen.InternalName}Request request)
        {{
            if (request == null)
            {{
                return BadRequest();
            }}
            
            var query = from item in _context.{entity.InternalNamePlural}
                        select item;            

            var totalItems = query.Count();
            int totalPages = 0;
            var items = new {screen.InternalName}Item[0];

            if (totalItems != 0 && request.PageSize != 0)
            {{
                totalPages = Convert.ToInt32(Math.Ceiling((double)totalItems / request.PageSize));

                items = await query
                    .OrderBy(p => p.Title) //TODO: From Setting
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(item => new {screen.InternalName}Item
                    {{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
                    }})
                    .ToArrayAsync();
    
            }}
            
            var result = new {screen.InternalName}Response
            {{
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            }};
            return Ok(result);
        }}";
        }
    }
}
