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

            return $@"        public async Task<Models.{Entity.InternalName}.Export.{Entity.InternalName}> Export{Entity.InternalName}Async(Guid id)
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
            var childEntities = (from e in Project.Entities
                                 from property in e.Properties
                                 where property.Type == PropertyTypeEnum.ParentRelationship &&
                                 property.ParentEntityId.HasValue &&
                                 property.ParentEntityId == entity.Id
                                 select e).Distinct().ToArray();

            var paths = new List<List<string>>();
            foreach (var childEntity in childEntities)
            {
                var subPath = new List<string>
                {
                    childEntity.InternalNamePlural
                };
                paths.Add(subPath);

                var returnedPaths = GetIncludePaths(childEntity);
                foreach (var returnedPath in returnedPaths)
                {
                    var subSubPath = new List<string>
                    {
                        childEntity.InternalNamePlural
                    };
                    subSubPath.AddRange(returnedPath);
                    paths.Add(subSubPath);
                }
            }

            return paths;
        }

    }
}
