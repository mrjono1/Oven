using System;
using System.Collections.Generic;
using System.Text;
using MasterBuilder.Request;
using System.Linq;

namespace MasterBuilder.Templates.Controllers
{
    public class ControllerSearchMethodTemplate
    {
        internal static string Evaluate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
        {
            var method = new StringBuilder();


            var propertyMapping = new List<string>();
            var sectionEntity = project.Entities.FirstOrDefault(e => e.Id == screenSection.EntityId);
            foreach (var item in sectionEntity.Properties)
            {
                if (item.Type == PropertyTypeEnum.Relationship)
                {
                    continue;
                }
                propertyMapping.Add($"                        {item.InternalName} = item.{item.InternalName}");
            }

            var itemClassName = $"{screen.InternalName}Item";
            var responseClassName = $"{screen.InternalName}Response";
            var requestClassName = $"{screen.InternalName}Request";
            if (screen.EntityId.HasValue && screenSection.EntityId.HasValue && screen.EntityId != screenSection.EntityId)
            {
                itemClassName = $"{screen.InternalName}{screenSection.InternalName}Item";
                responseClassName = $"{screen.InternalName}{screenSection.InternalName}Response";
                requestClassName = $"{screen.InternalName}{screenSection.InternalName}Request";
            }

            return $@"
        [HttpPost(""{screen.InternalName}{screenSection.InternalName}"")]
        public async Task<IActionResult> {screen.InternalName}{screenSection.InternalName}([FromBody]{requestClassName} request)
        {{
            if (request == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            var query = from item in _context.{entity.InternalNamePlural}
                        select item;            

            var totalItems = query.Count();
            int totalPages = 0;
            var items = new {itemClassName}[0];

            if (totalItems != 0 && request.PageSize != 0)
            {{
                totalPages = Convert.ToInt32(Math.Ceiling((double)totalItems / request.PageSize));

                items = await query
                    .OrderBy(p => p.Title) //TODO: From Setting
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(item => new {itemClassName}
                    {{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
                    }})
                    .ToArrayAsync();
    
            }}
            
            var result = new {responseClassName}
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
