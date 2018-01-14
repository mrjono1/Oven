using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Request
{
    public partial class ScreenSection
    {
        /// <summary>
        /// Fills in any missing values and records it can to assist templating
        /// </summary>
        internal bool Resolve(Project project, Screen screen, out string message)
        {
            var errors = new List<string>();

            switch (ScreenSectionType)
            {
                case ScreenSectionTypeEnum.Form:
                    break;
                case ScreenSectionTypeEnum.Search:
                    ResolveSearchSection(project);
                    break;
                case ScreenSectionTypeEnum.MenuList:
                    break;
                case ScreenSectionTypeEnum.Html:
                    break;
                default:
                    break;
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

        private void ResolveSearchSection(Project project)
        {
            var entity = project.Entities.SingleOrDefault(e => e.Id == EntityId.Value);

            // Populate Property property for helper functions to work
            if (SearchSection != null && SearchSection.SearchColumns != null)
            {
                foreach (var searchColumn in SearchSection.SearchColumns)
                {
                    searchColumn.Property = entity.Properties.SingleOrDefault(p => p.Id == searchColumn.EntityPropertyId);
                }

                return;
            }

            // Create Default Search Screens
            var searchColumns = new List<SearchColumn>();
            foreach (var property in entity.Properties)
            {
                switch (property.PropertyType)
                {
                    case PropertyTypeEnum.OneToOneRelationship:
                        continue;
                    default:
                        searchColumns.Add(new SearchColumn
                        {
                            EntityPropertyId = property.Id,
                            Property = property
                        });
                        break;
                }
            }

            SearchSection = new SearchSection
            {
                SearchColumns = searchColumns
            };
        }
    }
}
