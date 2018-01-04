using MasterBuilder.Request;
using System.Linq;

namespace MasterBuilder.Templates.Controllers
{
    /// <summary>
    /// Controller Reference Method Template
    /// </summary>
    public class ControllerReferenceMethodTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        internal static string Evaluate(Project project, Entity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var doesHaveReferences = (from e in project.Entities
                                   where e.Properties != null
                                   from property in e.Properties
                                   where property.Type == PropertyTypeEnum.ReferenceRelationship &&
                                   property.ParentEntityId.HasValue &&
                                   property.ParentEntityId == entity.Id
                                   select property).Any();
            if (!doesHaveReferences)
            {
                return null;
            }
                        
            var itemClassName = $"Models.{entity.InternalName}.Reference.{entity.InternalName}ReferenceItem";
            var responseClassName = $"Models.{entity.InternalName}.Reference.{entity.InternalName}ReferenceResponse";
            var requestClassName = $"Models.{entity.InternalName}.Reference.{entity.InternalName}ReferenceRequest";
            
            return $@"
        [HttpPost(""{entity.InternalName}References"")]
        public async Task<IActionResult> {entity.InternalName}Reference([FromBody]{requestClassName} request)
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
                        where request.Query == null || request.Query == "" ||
                        item.Title.Contains(request.Query)
                        select new
                        {{
                            item.Id,
                            item.Title
                        }};            

            var totalItems = query.Count();
            int totalPages = 0;
            var items = new {itemClassName}[0];

            if (totalItems != 0 && request.PageSize != 0)
            {{
                totalPages = Convert.ToInt32(Math.Ceiling((double)totalItems / request.PageSize));

                var dbItems = await query
                    .OrderBy(p => p.Title) //TODO: From Setting
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToArrayAsync();

                // had to do the select outside EF as it was having issues with the AnonymousType
                items = dbItems
                    .Select(item => new {itemClassName}
                    {{
                        Id = item.Id,
                        Title = item.Title
                    }})
                    .ToArray();
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
