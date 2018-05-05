using System;
using System.Collections.Generic;
using System.Text;
using MasterBuilder.Request;
using System.Linq;

namespace MasterBuilder.Templates.Api.Controllers
{
    /// <summary>
    /// Controller Search Method Template
    /// </summary>
    public class ControllerSearchMethodTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        internal static string Evaluate(Project project, Screen screen, ScreenSection screenSection)
        {            
            var propertyMapping = new List<string>();
            
            foreach (var searchColumn in screenSection.SearchSection.SearchColumns)
            {
                switch (searchColumn.PropertyType)
                {
                    case PropertyType.ParentRelationshipOneToMany:
                    case PropertyType.ParentRelationshipOneToOne:
                        break;
                    case PropertyType.ReferenceRelationship:
                        propertyMapping.Add($"                        {searchColumn.InternalNameCSharp} = (item.{searchColumn.Property.InternalName} != null ? item.{searchColumn.Property.InternalName}.Title : null)");
                        break;
                    default:
                        propertyMapping.Add($"                        {searchColumn.InternalNameCSharp} = item.{searchColumn.Property.InternalName}");
                        break;
                }
            }
            
            string parentPropertyWhereString = null;
            Entity parentEntity = null;

            var parentProperty = (from p in screenSection.SearchSection.Entity.Properties
                                    where p.PropertyType == PropertyType.ParentRelationshipOneToMany
                                    select p).SingleOrDefault();
            if (parentProperty != null)
            {
                parentEntity = (from s in project.Entities
                                where s.Id == parentProperty.ParentEntityId
                                select s).SingleOrDefault();
                parentPropertyWhereString = $"where request.{parentEntity.InternalName}Id == item.{parentEntity.InternalName}Id";
            }
            
            return $@"
        /// <summary>
        /// {screenSection.Title} Search
        /// </summary>
        [HttpPost(""{screen.InternalName}{screenSection.InternalName}"")]
        [ProducesResponseType(typeof({screenSection.SearchSection.SearchResponseClass}), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> {screen.InternalName}{screenSection.InternalName}([FromBody]{screenSection.SearchSection.SearchRequestClass} request)
        {{
            if (request == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            var query = from item in _context.{screenSection.SearchSection.Entity.InternalNamePlural}
            {parentPropertyWhereString}
                        select item;

            var totalItems = query.Count();
            int totalPages = 0;
            var items = new {screenSection.SearchSection.SearchItemClass}[0];

            if (totalItems != 0 && request.PageSize != 0)
            {{
                totalPages = Convert.ToInt32(Math.Ceiling((double)totalItems / request.PageSize));

                items = await query
                    .OrderBy(p => p.{screenSection.SearchSection.OrderBy})
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(item => new {screenSection.SearchSection.SearchItemClass}
                    {{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
                    }})
                    .ToArrayAsync();
    
            }}
            
            var result = new {screenSection.SearchSection.SearchResponseClass}
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
