using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Ts
{
    /// <summary>
    /// Form Section Partail
    /// </summary>
    public class FormSectionPartial
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormSectionPartial(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Get Imports
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<string> GetImports()
        {
            var imports = new List<string>
            {
                $"import {{ {ScreenSection.InternalName} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{ScreenSection.InternalName}';",
                $"import {{ {Screen.InternalName}Service }} from '../../shared/{Screen.InternalName.ToLowerInvariant()}.service';",
                "import { Operation } from '../../models/Operation';",
                "import { FormControl, FormGroup, Validators } from '@angular/forms';",
                "import { Observable } from 'rxjs/Observable';"
            };

            var hasReferenceFormField = false;
            foreach (var screenSection in Screen.ScreenSections)
            {
                if (screenSection.ScreenSectionType == ScreenSectionType.Form)
                {
                    foreach (var referenceFormField in screenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyType.ReferenceRelationship))
                    {
                        hasReferenceFormField = true;
                        imports.Add($"import {{ {referenceFormField.ReferenceItemClass} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{referenceFormField.ReferenceItemClass}';");
                        imports.Add($"import {{ {referenceFormField.ReferenceResponseClass} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{referenceFormField.ReferenceResponseClass}';");
                    }
                }
            }

            if (hasReferenceFormField)
            {
                imports.Add("import { ReferenceRequest } from '../../models/ReferenceRequest';");
                imports.Add($"import {{ {Screen.InternalName}Service }} from '../../shared/{Screen.InternalName.ToLowerInvariant()}.service';");
            }

            return imports;
        }

        /// <summary>
        /// Get constructor parameters
        /// </summary>
        internal IEnumerable<string> GetConstructorParameters()
        {
            return new string[]
            {
                $"private {Screen.InternalName.Camelize()}Service: {Screen.InternalName}Service"
            };
        }

        /// <summary>
        /// Get Constructor Body Sections
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<string> GetConstructorBodySections()
        {
            return new string[]
            {
            };
        }

        /// <summary>
        /// Get Class Properties
        /// </summary>
        internal IEnumerable<string> GetClassProperties()
        {
            var classProperties = new List<string>
            {
                $"public {ScreenSection.InternalName.Camelize()}: {ScreenSection.InternalName};",
                $"public {ScreenSection.InternalName.Camelize()}Form: FormGroup;",
                "public new: boolean;"
            };

            foreach (var referenceFormField in ScreenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyType.ReferenceRelationship))
            {
                classProperties.Add($"public {referenceFormField.Property.InternalName.Camelize()}Reference: {referenceFormField.ReferenceResponseClass} = new {referenceFormField.ReferenceResponseClass}();");
            }
            
            return classProperties;
        }

        /// <summary>
        /// Get on Ng Init Body Sections
        /// </summary>
        internal IEnumerable<string> GetOnNgInitBodySections()
        {
            var lines = new List<string>();

            Property parentProperty = null;
            parentProperty = (from p in ScreenSection.FormSection.Entity.Properties
                              where p.PropertyType == PropertyType.ParentRelationship
                              select p).SingleOrDefault();
            string setParentProperty = null;
            if (parentProperty != null)
            {
                var parentEnitity = (from e in Project.Entities
                                     where e.Id == parentProperty.ParentEntityId
                                     select e).SingleOrDefault();
                setParentProperty = $"this.{ScreenSection.InternalName.Camelize()}.{parentProperty.InternalName.Camelize()}Id = params['{parentEnitity.InternalName.Camelize()}Id'];";
            }

            lines.Add($@"        this.route.params.subscribe(params => {{
            if (params['{Screen.InternalName.Camelize()}Id']) {{
                this.new = false;
                this.{Screen.InternalName.Camelize()}Service.get{Screen.InternalName}(params['{ScreenSection.FormSection.Entity.InternalName.Camelize()}Id']).subscribe(result => {{
                    this.{ScreenSection.InternalName.Camelize()} = result;
                    this.setupForm();
                }}, error => console.error(error));
            }} else {{
                this.new = true;
                this.{ScreenSection.InternalName.Camelize()} = new {ScreenSection.InternalName}();
                {setParentProperty}
                this.setupForm();
            }}
        }});");

            foreach (var screenSection in Screen.ScreenSections)
            {
                if (screenSection.ScreenSectionType == ScreenSectionType.Form)
                {
                    foreach (var referenceFormField in screenSection.FormSection.FormFields.Where(a => a.PropertyType == PropertyType.ReferenceRelationship))
                    {
                        lines.Add($@"        this.{Screen.InternalName.Camelize()}Service.get{referenceFormField.Property.InternalName}References(null , 1, 100).subscribe((result: any) => {{
            if (result != null) {{
                this.{referenceFormField.Property.InternalName.Camelize()}Reference.items = result.items;
            }}
        }});");
                    }
                }
            }

            return lines;
        }

        /// <summary>
        /// Get Functions
        /// </summary>
        internal string[] GetFunctions()
        {
            var methods = new string[]
            {
                @"    referenceCompare(referenceItem1: any, referenceItem2: any): boolean {{
        return referenceItem1 === referenceItem2;
    }}",
                $@"    private getPatchOperations(): Operation[] {{
        let operations: Operation[] = [];

        Object.keys(this.{Screen.InternalName.Camelize()}Form.controls).forEach((name) => {{
            let currentControl = this.{Screen.InternalName.Camelize()}Form.controls[name];

            if (currentControl.dirty) {{
                let operation = new Operation();
                operation.op = 'replace';
                operation.path = '/' + name;
                if (currentControl.value instanceof Object) {{
                    operation.value = currentControl.value.id;
                }} else {{
                    operation.value = currentControl.value;
                }}
                operations.push(operation);
            }}
        }});
        return operations;
    }}",
                $@"    onSubmit() {{ 
        // Don't submit if nothing has changed
        if (this.{Screen.InternalName.Camelize()}Form.pristine || !this.{Screen.InternalName.Camelize()}Form.valid) {{
            return;
        }}
        
        if (this.new){{
            // Post new
            this.{Screen.InternalName.Camelize()}Service.add{Screen.InternalName}(this.{Screen.InternalName.Camelize()}Form.getRawValue()).subscribe( id => {{
                this.router.navigate([this.router.url + '/' + id], {{ replaceUrl: true }});
            }});
        }} else {{
            // Patch existing
            let operations = this.getPatchOperations();
            this.{Screen.InternalName.Camelize()}Service.update{Screen.InternalName}(this.{Screen.InternalName.Camelize()}.id, operations).subscribe( result => {{
                this.{Screen.InternalName.Camelize()}Form.markAsPristine({{ onlySelf: false }});
            }});
        }}
    }}"
            };

            return methods;
        }

        /// <summary>
        /// Get Properties
        /// </summary>
        internal IEnumerable<string> GetProperties()
        {
            var properties = new List<string>();

            foreach (var formField in ScreenSection.FormSection.FormFields)
            {
                switch (formField.PropertyType)
                {
                    case PropertyType.PrimaryKey:
                        break;
                    default:
                        properties.Add($@"    get {formField.InternalNameCSharp.Camelize()}() {{ return this.{Screen.InternalName.Camelize()}Form.get('{formField.InternalNameCSharp.Camelize()}'); }}");
                        break;
                }
            }
            
            return properties;
        }

        internal IEnumerable<string> GetFormControls()
        {
            var formControls = new List<string>();

            foreach (var formField in ScreenSection.FormSection.FormFields)
            {
                if (formField.PropertyType == PropertyType.PrimaryKey)
                {
                    continue;
                }

                var propertyValidators = new List<string>();
                if (formField.Property.ValidationItems != null)
                {
                    foreach (var validationItem in formField.Property.ValidationItems)
                    {
                        switch (validationItem.ValidationType)
                        {
                            case ValidationType.Required:
                                propertyValidators.Add("            Validators.required");
                                break;
                            case ValidationType.MaximumLength:
                                propertyValidators.Add($"            Validators.maxLength({validationItem.IntegerValue.Value})");
                                break;
                            case ValidationType.MinimumLength:
                                propertyValidators.Add($"            Validators.minLength({validationItem.IntegerValue.Value})");
                                break;
                            case ValidationType.MaximumValue:
                                propertyValidators.Add($"            Validators.max({validationItem.IntegerValue.Value})");
                                break;
                            case ValidationType.MinimumValue:
                                propertyValidators.Add($"            Validators.min({validationItem.IntegerValue.Value})");
                                break;
                            case ValidationType.Unique:
                                // TODO async validator or just let database let you know of the error?
                                break;
                            case ValidationType.Email:
                                propertyValidators.Add($"            Validators.email");
                                break;
                            case ValidationType.RequiredTrue:
                                propertyValidators.Add($"            Validators.requiredTrue");
                                break;
                            case ValidationType.Pattern:
                                propertyValidators.Add($"            Validators.pattern({validationItem.StringValue})");
                                break;
                            default:
                                break;
                        }
                    }
                }

                var propertyValidatorsString = (propertyValidators.Any() ?
                    $",[{Environment.NewLine}{string.Join(string.Concat(",", Environment.NewLine), propertyValidators)}]" :
                    string.Empty);

                switch (formField.PropertyType)
                {
                    case PropertyType.ReferenceRelationship:
                        formControls.Add($@"        let {formField.InternalNameCSharp.Camelize()}Control: FormControl = new FormControl(this.{Screen.InternalName.Camelize()}.{formField.InternalNameCSharp.Camelize()}{propertyValidatorsString});
        this.{Screen.InternalName.Camelize()}Form.addControl('{formField.InternalNameCSharp.Camelize()}', {formField.InternalNameCSharp.Camelize()}Control);");
                        break;

                    default:
                        formControls.Add($@"        this.{Screen.InternalName.Camelize()}Form.addControl('{formField.InternalNameCSharp.Camelize()}', new FormControl(this.{Screen.InternalName.Camelize()}.{formField.InternalNameCSharp.Camelize()}{propertyValidatorsString}));");
                        break;
                }
            }

            return formControls;
        }
    }
}
