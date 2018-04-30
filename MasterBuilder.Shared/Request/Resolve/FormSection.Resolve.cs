using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    public partial class FormSection
    {
        /// <summary>
        /// Resolve Form Section
        /// </summary>
        internal bool Resolve(Project project, Screen screen, ScreenSection screenSection, out string message)
        {
            var errors = new List<string>();
            Screen = screen;
            ScreenSection = screenSection;
            Entity = screenSection.Entity;
            
            // Populate Property property for helper functions to work
            if (FormFields != null)
            {
                foreach (var formField in FormFields)
                {

                    if (!formField.Resolve(project, screen, screenSection, out string formFieldMessage)){
                        errors.Add(formFieldMessage);
                    }
                }

                // Add Primary Key field
                if (!FormFields.Any(a => a.PropertyType == PropertyType.PrimaryKey))
                {
                    var primaryKeyProperty = Entity.Properties.Single(property => property.PropertyType == PropertyType.PrimaryKey);

                    var primaryKeyFormField = new FormField
                    {
                        EntityPropertyId = primaryKeyProperty.Id,
                        IsHiddenFromUi = true
                    };
                    if (!primaryKeyFormField.Resolve(project, screen, screenSection, out string formFieldMessage))
                    {
                        errors.Add(formFieldMessage);
                    }

                    var primaryKeyformFields = new List<FormField>(FormFields)
                    {
                        primaryKeyFormField
                    };
                    FormFields = primaryKeyformFields;
                }

                // Add Parent Relationship
                if (ScreenSection.ParentScreenSection == null)
                {
                    Property parentProperty = (from p in ScreenSection.Entity.Properties
                                               where p.PropertyType == PropertyType.ParentRelationshipOneToMany ||
                                                     p.PropertyType == PropertyType.ParentRelationshipOneToOne
                                               select p).SingleOrDefault();

                    if (parentProperty != null)
                    {

                        var parentFormField = new FormField
                        {
                            EntityPropertyId = parentProperty.Id,
                            IsHiddenFromUi = true
                        };
                        if (!parentFormField.Resolve(project, screen, screenSection, out string formFieldMessage))
                        {
                            errors.Add(formFieldMessage);
                        }

                        var parentFormFields = new List<FormField>(FormFields)
                    {
                        parentFormField
                    };
                        FormFields = parentFormFields;
                    }
                }
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