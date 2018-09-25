using Oven.Request;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Shared.Extensions
{
    public static class EntityExtensions
    {
        public static IEnumerable<Entity> GetParentEntites(this Entity entity, Project project)
        {
            var parentEnities = new List<Entity>();
            Entity parentEntity = null;

            var parentProperty = (from p in entity.Properties
                                  where p.PropertyType == PropertyType.ParentRelationshipOneToMany
                                  select p).SingleOrDefault();
            if (parentProperty != null)
            {
                parentEntity = (from s in project.Entities
                                where s.Id == parentProperty.ReferenceEntityId
                                select s).SingleOrDefault();

                if (parentEntity != null)
                {
                    parentEnities.Add(parentEntity);
                    parentEnities.AddRange(parentEntity.GetParentEntites(project));
                }
            }
            return parentEnities;
        }
    }
}
