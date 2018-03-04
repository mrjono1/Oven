using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    public partial class FormField
    {
        internal bool Resolve(Project project, Screen screen, ScreenSection screenSection, out string message)
        {
            Project = project;
            Property = screenSection.Entity.Properties.SingleOrDefault(p => p.Id == EntityPropertyId);

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
