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
                case PropertyType.ParentRelationship:
                    IsHiddenFromUi = true;
                    break;
                case PropertyType.OneToOneRelationship:
                    break;
            }

            message = "Success";
            return true;
        }
    }
}
