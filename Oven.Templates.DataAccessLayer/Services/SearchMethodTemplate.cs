using System;
using System.Collections.Generic;
using System.Text;
using Oven.Request;
using System.Linq;
using Humanizer;

namespace Oven.Templates.DataAccessLayer.Services
{
    /// <summary>
    /// Controller Search Method Template
    /// </summary>
    public class SearchMethodTemplate
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
                    case PropertyType.PrimaryKey:
                        propertyMapping.Add($"                        {searchColumn.InternalNameCSharp} = item.{searchColumn.Property.InternalName}");
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
        public virtual async Task<{screenSection.SearchSection.SearchResponseClass}> Search{actionName}Async({screenSection.SearchSection.SearchRequestClass} request)
        {{
            if (request == null)
            {{
                throw new NullReferenceException(); 
            }}

            var query = from item in _context.{screenSection.SearchSection.Entity.InternalNamePlural}.AsQueryable()
            {parentPropertyWhereString}
                        select item;

            var totalItems = query.Count();
            var items = new List<{screenSection.SearchSection.SearchItemClass}>();

            if (totalItems != 0 && request.end != 0)
            {{
                items = await query
                    .OrderBy(p => p.{screenSection.SearchSection.OrderBy})
                    .Skip(request.start)
                    .Take(request.end - request.start)
                    .Select(item => new {screenSection.SearchSection.SearchItemClass}
                    {{
{string.Join(string.Concat(",", Environment.NewLine), propertyMapping)}
                    }})
                    .ToListAsync();
            }}
            
            return new {screenSection.SearchSection.SearchResponseClass} {{
                TotalItems = totalItems,
                Items = items.ToArray(),
            }};
        }}";
        }
    }
}
