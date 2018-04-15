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
        internal static string Evaluate(Project project, Screen screen, FormField formField)
        {

            var entity = project.Entities.SingleOrDefault(e => e.Id == formField.Property.ParentEntityId);
                        
            var itemClassName = $"Models.{screen.InternalName}.Reference.{formField.ReferenceItemClass}";
            var responseClassName = $"Models.{screen.InternalName}.Reference.{formField.ReferenceResponseClass}";
            var requestClassName = $"Models.{screen.InternalName}.Reference.{formField.ReferenceRequestClass}";

            // TODO: this url needs to be formField specific

            var whereString = $@"where request.Query == null ||
                        request.Query == """" ||
                        item.Title.Contains(request.Query)";

            if (formField.Property.FilterExpression != null)
            {
                var expressionPartial = new Helpers.CsExpressionPartial(formField.Property, formField.Property.FilterExpression);
                var expression = expressionPartial.GetExpression("item", "request");

                whereString = $@"where ({expression}) &&
                        (request.Query == null ||
                        request.Query == """" ||
                        item.Title.Contains(request.Query))";
            }

            return $@"
        /// <summary>
        /// {formField.TitleValue} Reference Search
        /// </summary>
        [HttpPost(""{formField.Property.InternalName}References"")]
        [ProducesResponseType(typeof({responseClassName}), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> {formField.Property.InternalName}Reference([FromBody]{requestClassName} request)
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
                        {whereString}
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
