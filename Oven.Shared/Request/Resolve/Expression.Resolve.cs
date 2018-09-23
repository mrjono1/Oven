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

            message = "Success";
            return true;
        }
    }
}
