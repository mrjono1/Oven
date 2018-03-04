using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Services
{
    /// <summary>
    /// Export Function Template
    /// </summary>
    public class ExportFunctionTemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExportFunctionTemplate(Project project, Entity entity)
        {
            Project = project;
            Entity = entity;
        }

        /// <summary>
        /// Get the export function for this entity
        /// </summary>
        /// <returns></returns>
        public string Function()
        {
            var children = GetIncludePaths(Entity);

            var includes = new List<string>();
            foreach (var childEntity in children)
            {
                includes.Add($@"                          .Include(""{string.Join(".", childEntity)}"")");
            }

            return $@"        /// <summary>
        /// Export Full {Entity.Title}
        /// </summary>
        public async Task<Models.{Entity.InternalName}.Export.{Entity.InternalName}> Export{Entity.InternalName}Async(Guid id)
        {{
            var data = await _context.{Entity.InternalNamePlural}.Where(p => p.Id == id)
{string.Join(Environment.NewLine, includes)}
                          .SingleOrDefaultAsync();

            var result = new Models.{Entity.InternalName}.Export.{Entity.InternalName}(data);
            return result;
        }}";
        }

        /// <summary>
        /// Warning recursive
        /// </summary>
        public IEnumerable<IEnumerable<string>> GetIncludePaths(Entity entity)
        {
            //var childEntities = new List<Entity>();

            // Parent relationships
            var childEntities = (from e in Project.Entities
                                    from property in e.Properties
                                    where property.PropertyType == PropertyType.ParentRelationshipOneToMany &&
                                    property.ParentEntityId.HasValue &&
                                    property.ParentEntityId == entity.Id
                                    select new
                                    {
                                        Entity = e,
                                        Collection = true
                                    }).ToList();

            // One to One relationships
            childEntities.AddRange((from property in entity.Properties
                                    where property.PropertyType == PropertyType.ParentRelationshipOneToOne
                                    from e in Project.Entities
                                    where e.Id == property.ParentEntityId.Value
                                    select new
                                    {
                                        Entity = e,
                                        Collection = false
                                    }).ToList());

            var paths = new List<List<string>>();
            foreach (var childEntity in childEntities)
            {
                var subPath = new List<string>
                {
                    (childEntity.Collection ? childEntity.Entity.InternalNamePlural : childEntity.Entity.InternalName)
                };
                paths.Add(subPath);

                var returnedPaths = GetIncludePaths(childEntity.Entity);
                foreach (var returnedPath in returnedPaths)
                {
                    var subSubPath = new List<string>
                    {
                        (childEntity.Collection ? childEntity.Entity.InternalNamePlural : childEntity.Entity.InternalName)
                    };
                    subSubPath.AddRange(returnedPath);
                    paths.Add(subSubPath);
                }
            }

            return paths;
        }

    }
}
