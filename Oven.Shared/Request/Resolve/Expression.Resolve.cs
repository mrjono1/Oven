using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Oven.Request
{
    /// <summary>
    /// Logic Rule
    /// </summary>
    public partial class Expression
    {
        /// <summary>
        /// Fills in any missing values and records it can to assist templating
        /// </summary>
        internal bool Resolve(Project project, out string message)
        {
            if (EntityId.HasValue)
            {
                Entity = project.Entities.Single(e => e.Id == EntityId.Value);
            }

            if (PropertyId.HasValue)
            {
                Property = (from projectEntity in project.Entities
                            from entityProperty in projectEntity.Properties
                            where entityProperty.Id == PropertyId.Value
                            select entityProperty).SingleOrDefault();
            }

            message = "Success";
            return true;
        }
    }
}
