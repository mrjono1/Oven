using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    partial class FormField
    {
        internal bool Resolve(Project project, Screen screen, ScreenSection screenSection, out string message)
        {
            Project = project;
            Property = screenSection.Entity.Properties.SingleOrDefault(p => p.Id == EntityPropertyId);

            if (Property == null)
            {
                message = $"PropertyId:{EntityPropertyId} does not exisit. So on ScreenId:{screen.Id}";
                return false;
            }

            switch (PropertyType)
            {
                case PropertyType.PrimaryKey:
                case PropertyType.ParentRelationshipOneToMany:
                    IsHiddenFromUi = true;
                    break;
                case PropertyType.ParentRelationshipOneToOne:
                    break;
            }

            message = "Success";
            return true;
        }
    }
}
