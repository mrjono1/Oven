using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Property
    /// </summary>
    public partial class Property
    {
        /// <summary>
        /// Fills in any missing values and records it can to assist templating
        /// </summary>
        internal bool Resolve(Project project, Entity entity, out string message)
        {
            var errors = new List<string>();
            
            // TODO: below validation items are default so fix this so they can be overwritten
            if (PropertyTemplate == PropertyTemplate.ReferenceTitle)
            {
                ValidationItems = new Validation[]
                {
                    new Validation{
                        ValidationType = ValidationType.Unique
                    },
                    new Validation
                    {
                        ValidationType = ValidationType.MaximumLength,
                        IntegerValue = 200
                    },
                    new Validation{
                        ValidationType = ValidationType.Required
                    },
                };
            }

            // Check if need to enable support for spatial at project level
            if (this.PropertyType == PropertyType.Spatial && !project.IncludeSupportForSpatial)
                project.IncludeSupportForSpatial = true;

            if (ParentEntityId.HasValue)
            {
                ParentEntity = project.Entities.Single(a => a.Id == ParentEntityId.Value);
            }

            if (errors.Any())
            {
                message = string.Join(Environment.NewLine, errors);
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
