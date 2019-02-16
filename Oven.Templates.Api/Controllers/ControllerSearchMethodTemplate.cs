using System;
using System.Collections.Generic;
using System.Text;
using Oven.Request;
using System.Linq;
using Humanizer;

namespace Oven.Templates.Api.Controllers
{
    /// <summary>
    /// Controller Search Method Template
    /// </summary>
    public class ControllerSearchMethodTemplate
    {
        /// <summary>
        /// Evaluate
        /// </summary>
        internal static string Evaluate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
        {
            string actionName = null;
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
                                where s.Id == parentProperty.ReferenceEntityId
                                select s).SingleOrDefault();
                parentPropertyWhereString = $"where request.{parentEntity.InternalName}Id == item.{parentEntity.InternalName}Id";
            }
            
            if (entity.Id != screen.EntityId)
            {
                actionName = $"{screen.InternalName}{screenSection.InternalName}";
            }

            return $@"
        /// <summary>
        /// {screenSection.Title} Search
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<{screenSection.SearchSection.SearchItemClass}>), 200)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), 400)]
        public async Task<IActionResult> Search{actionName}Async([FromServices] I{screenSection.Entity.InternalName}Service {screenSection.Entity.InternalName.Camelize()}Service, [FromQuery]{screenSection.SearchSection.SearchRequestClass} request)
        {{
            if (request == null)
            {{
                return BadRequest();
            }}
            
            if (!ModelState.IsValid)
            {{
                return new BadRequestObjectResult(ModelState);
            }}

            var response = await {screenSection.Entity.InternalName.Camelize()}Service.Search{actionName}Async(request);
            
            Response.Headers.Add(""Content-Range"", $@""{screenSection.Entity.InternalNamePlural} {{request.start}}-{{request.start + response.Items.Count()}}/{{response.TotalItems}}"");
            Response.Headers.Add(""X-Total-Count"", response.TotalItems.ToString());
            return Ok(response.Items);
        }}";
        }
    }
}
