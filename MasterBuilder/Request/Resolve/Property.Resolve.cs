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
            if (PropertyTemplate == PropertyTemplateEnum.ReferenceTitle)
            {
                ValidationItems = new Validation[]
                {
                    new Validation{
                        ValidationType = ValidationTypeEnum.Unique
                    },
                    new Validation
                    {
                        ValidationType = ValidationTypeEnum.MaximumLength,
                        IntegerValue = 200
                    },
                    new Validation{
                        ValidationType = ValidationTypeEnum.Required
                    },
                };
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
