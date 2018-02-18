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
                case ScreenSectionType.Form:
                    ResolveFormSection(project, screen);
                    break;
                case ScreenSectionType.Search:
                    ResolveSearchSection(project, screen);
                    break;
                case ScreenSectionType.MenuList:
                    break;
                case ScreenSectionType.Html:
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
            Entity = project.Entities.SingleOrDefault(e => e.Id == EntityId.Value);
            if (ParentEntityPropertyId.HasValue)
            {
                ParentEntityProperty = screen.Entity.Properties.Single(p => p.Id == ParentEntityPropertyId.Value);
            }

            // Populate Property property for helper functions to work
            if (FormSection != null && FormSection.FormFields != null)
            {
                FormSection.Screen = screen;
                FormSection.ScreenSection = this;
                FormSection.Entity = Entity;

                foreach (var formField in FormSection.FormFields)
                {
                    formField.Property = Entity.Properties.SingleOrDefault(p => p.Id == formField.EntityPropertyId);
                    formField.Project = project;
                }

                // Add Primary Key field
                if (!FormSection.FormFields.Any(a => a.PropertyType == PropertyType.PrimaryKey))
                {
                    var primaryKeyFormField = new FormField
                    {
                        Property = Entity.Properties.Single(property => property.PropertyType == PropertyType.PrimaryKey),
                        Project = project
                    };
                    primaryKeyFormField.EntityPropertyId = primaryKeyFormField.Property.Id;

                    var primaryKeyformFields = new List<FormField>(FormSection.FormFields)
                    {
                        primaryKeyFormField
                    };
                    FormSection.FormFields = primaryKeyformFields;
                }

                return;
            }

            // Create Default Search Screens
            var formFields = new List<FormField>();
            foreach (var property in Entity.Properties)
            {
                switch (property.PropertyType)
                {
                    case PropertyType.OneToOneRelationship:
                        continue;
                    default:
                        formFields.Add(new FormField
                        {
                            EntityPropertyId = property.Id,
                            Property = property,
                            Project = project
                        });
                        break;
                }
            }

            FormSection = new FormSection
            {
                FormFields = formFields,
                Screen = screen,
                ScreenSection = this,
                Entity = Entity
            };
        }

        /// <summary>
        /// Resolve Search Section
        /// </summary>
        private void ResolveSearchSection(Project project, Screen screen)
        {
            Entity = project.Entities.SingleOrDefault(e => e.Id == EntityId.Value);

            // Populate Property property for helper functions to work
            if (SearchSection != null && SearchSection.SearchColumns != null)
            {
                SearchSection.Screen = screen;
                SearchSection.ScreenSection = this;
                SearchSection.Entity = Entity;
                foreach (var searchColumn in SearchSection.SearchColumns)
                {
                    searchColumn.Property = Entity.Properties.SingleOrDefault(p => p.Id == searchColumn.EntityPropertyId);
                }
                
                // Add Primary Key field
                if (!SearchSection.SearchColumns.Any(a => a.PropertyType == PropertyType.PrimaryKey))
                {
                    var primaryKeySearchColumn = new SearchColumn
                    {
                        Property = Entity.Properties.Single(property => property.PropertyType == PropertyType.PrimaryKey)
                    };
                    primaryKeySearchColumn.EntityPropertyId = primaryKeySearchColumn.Property.Id;

                    var primaryKeySearchColumns = new List<SearchColumn>(SearchSection.SearchColumns)
                    {
                        primaryKeySearchColumn
                    };
                    SearchSection.SearchColumns = primaryKeySearchColumns;
                }
                return;
            }

            // Create Default Search Screens
            var searchColumns = new List<SearchColumn>();
            foreach (var property in Entity.Properties)
            {
                switch (property.PropertyType)
                {
                    case PropertyType.OneToOneRelationship:
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
                Entity = Entity
            };
        }
    }
}
