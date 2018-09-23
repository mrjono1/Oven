using System.Collections.Generic;
using System.Linq;

namespace Oven.Request
{
    /// <summary>
    /// Database Property
    /// </summary>
    public partial class Property
    {

        /// <summary>
        /// Validate and perform data fixes
        /// </summary>
        internal bool Validate(Project project, Entity entity, out string message)
        {
            var messageList = new List<string>();
            
            if (ReferenceEntityId.HasValue)
            {
                var parentEntity = project.Entities.Any(a => a.Id == ReferenceEntityId.Value);
                if (!parentEntity)
                {
                    messageList.Add($"Entity:{entity.InternalName}, Property:{InternalName} contains an invalid ParentEntitId:{ReferenceEntityId.Value}");
                }
            }

            if (messageList.Any())
            {
                message = string.Join(", ", messageList);
                return false;
            }
            else
            {
                message = "Success";
                return true;
            }
        }
    }
}
