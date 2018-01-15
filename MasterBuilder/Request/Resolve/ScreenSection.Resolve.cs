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
                    ResolveFormSection(project, screen);
                    break;
                case ScreenSectionTypeEnum.Search:
                    ResolveSearchSection(project, screen);
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

        /// <summary>
        /// Resolve Form Section
        /// </summary>
        private void ResolveFormSection(Project project, Screen screen)
        {
            var entity = project.Entities.SingleOrDefault(e => e.Id == EntityId.Value);

            // Populate Property property for helper functions to work
            if (FormSection != null && FormSection.FormFields != null)
            {
                FormSection.Screen = screen;
                FormSection.ScreenSection = this;
                FormSection.Entity = entity;
                foreach (var formField in FormSection.FormFields)
                {
                    formField.Property = entity.Properties.SingleOrDefault(p => p.Id == formField.EntityPropertyId);
                }

                return;
            }

            // Create Default Search Screens
            var formFields = new List<FormField>();
            foreach (var property in entity.Properties)
            {
                switch (property.PropertyType)
                {
                    case PropertyTypeEnum.OneToOneRelationship:
                        continue;
                    default:
                        formFields.Add(new FormField
                        {
                            EntityPropertyId = property.Id,
                            Property = property
                        });
                        break;
                }
            }

            FormSection = new FormSection
            {
                FormFields = formFields,
                Screen = screen,
                ScreenSection = this,
                Entity = entity
            };
        }

        /// <summary>
        /// Resolve Search Section
        /// </summary>
        private void ResolveSearchSection(Project project, Screen screen)
        {
            var entity = project.Entities.SingleOrDefault(e => e.Id == EntityId.Value);

            // Populate Property property for helper functions to work
            if (SearchSection != null && SearchSection.SearchColumns != null)
            {
                SearchSection.Screen = screen;
                SearchSection.ScreenSection = this;
                SearchSection.Entity = entity;
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
                SearchColumns = searchColumns,
                Screen = screen,
                ScreenSection = this,
                Entity = entity
            };
        }
    }
}
