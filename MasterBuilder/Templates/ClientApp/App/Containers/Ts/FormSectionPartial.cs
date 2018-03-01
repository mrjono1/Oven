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
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormSectionPartial(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
        {
            Project = project;
            Screen = screen;
            ScreenSections = screenSections;
        }

        /// <summary>
        /// Get Imports
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<string> GetImports()
        {
            var imports = new List<string>
            {
                $"import {{ {Screen.Entity.InternalName} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{Screen.Entity.InternalName}';",
                $"import {{ {Screen.InternalName}Service }} from '../../shared/{Screen.InternalName.ToLowerInvariant()}.service';",
                "import { Operation } from '../../models/Operation';",
                "import { FormControl, FormGroup, Validators } from '@angular/forms';",
                "import { Observable } from 'rxjs/Observable';",
                "import { HttpErrorService } from '../../shared/httperror.service';",
                "import { ComponentCanDeactivate } from '../../shared/pending.changes.guard';"
            };

            // TODO: implement child sections
            // Convert child properties to objects with properties
            //var childSections = (from formSection in ScreenSections
            //                     where formSection.ParentEntityPropertyId.HasValue
            //                     select formSection).ToArray();

            //foreach (var childItem in childSections.GroupBy(a => a.ParentEntityProperty))
            //{
            //    imports.Add($"import {{ {childItem.Key.ParentEntity.InternalName} }} from '../../models/{Screen.InternalName.ToLowerInvariant()}/{childItem.Key.ParentEntity.InternalName}';");
            //}

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
                $"private {Screen.InternalName.Camelize()}Service: {Screen.InternalName}Service",
                "private httpErrorService: HttpErrorService"
            };
        }

        /// <summary>
        /// Get Constructor Body Sections
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<string> GetConstructorBodySections()
        {
            var sections = new List<string>();

            // TODO: implement child sections
            //var childSections = (from formSection in ScreenSections
            //                     where formSection.ParentEntityPropertyId.HasValue
            //                     select formSection).ToArray();

            //foreach (var childItem in childSections.GroupBy(a => a.ParentEntityProperty))
            //{
            //    sections.Add($"this.serverErrorMessages.{childItem.Key.InternalName.Camelize()} = {{}};");
            //}

            return sections;
        }

        /// <summary>
        /// Get Class Properties
        /// </summary>
        internal IEnumerable<string> GetClassProperties()
        {
            var classProperties = new List<string>
            {
                $"public {Screen.InternalName.Camelize()}: {Screen.InternalName};",
                $"public {Screen.InternalName.Camelize()}Form: FormGroup;",
                "public new: boolean;",
                "public serverErrorMessages: any = {};"
            };

            foreach (var referenceFormField in (from screenSection in ScreenSections
                                                from formField in screenSection.FormSection.FormFields
                                                where formField.PropertyType == PropertyType.ReferenceRelationship
                                                select formField))
            {
                classProperties.Add($"public {referenceFormField.Property.InternalName.Camelize()}Reference: {referenceFormField.ReferenceResponseClass} = new {referenceFormField.ReferenceResponseClass}();");
            }

            return classProperties;
        }

        internal IEnumerable<string> GetVisibilityFunctions()
        {
            var functions = new List<string>();

            foreach (var formField in (from screenSection in ScreenSections
                                       from formField in screenSection.FormSection.FormFields
                                       where formField.PropertyType != PropertyType.PrimaryKey
                                       select formField))
            {
                if (formField.VisibilityExpression == null) {
                    functions.Add($@"    {formField.InternalNameTypeScript}Visible() {{
        return this.{Screen.InternalName.Camelize()} && this.{Screen.InternalName.Camelize()}Form;
    }}");
                }
                else {
                    var expressionPartial = new Evaluate.TsExpressionPartial(Screen, ScreenSections);
                    var expression = expressionPartial.GetExpression(formField.VisibilityExpression);

                    var property = (from screenSection in ScreenSections
                                    from ff in screenSection.FormSection.FormFields
                                    where ff.EntityPropertyId == formField.VisibilityExpression.PropertyId
                                    select ff).Single();
                    functions.Add($@"    {formField.InternalNameTypeScript}Visible() {{
        if (this.{Screen.InternalName.Camelize()} &&
            this.{Screen.InternalName.Camelize()}Form &&
            {expression}) {{
                return true;
        }}
        return false;
    }}");
                }
            }

            return functions;
        }

        /// <summary>
        /// Get on Ng Init Body Sections
        /// </summary>
        internal IEnumerable<string> GetOnNgInitBodySections()
        {
            var lines = new List<string>();
            var initSections = new List<string> {
                $@"                this.{Screen.InternalName.Camelize()} = new {Screen.InternalName}();"
            };

            Property parentProperty = null;
            parentProperty = (from p in Screen.Entity.Properties
                              where p.PropertyType == PropertyType.ParentRelationship
                              select p).SingleOrDefault();
            
            if (parentProperty != null)
            {
                var parentEnitity = (from e in Project.Entities
                                     where e.Id == parentProperty.ParentEntityId
                                     select e).SingleOrDefault();
                initSections.Add($"                this.{Screen.InternalName.Camelize()}.{parentProperty.InternalName.Camelize()}Id = params['{parentEnitity.InternalName.Camelize()}Id'];");
            }

            // TODO: implement child sections
            //var childSections = (from formSection in ScreenSections
            //                     where formSection.ParentEntityPropertyId.HasValue
            //                     select formSection).ToArray();

            //foreach (var childItem in childSections.GroupBy(a => a.ParentEntityProperty))
            //{
            //    initSections.Add($"                this.{Screen.InternalName.Camelize()}.{childItem.Key.InternalName.Camelize()} = new {childItem.Key.ParentEntity.InternalName}();");
            //}

            lines.Add($@"        this.route.params.subscribe(params => {{
            if (params['{Screen.InternalName.Camelize()}Id']) {{
                this.new = false;
                this.{Screen.InternalName.Camelize()}Service.get{Screen.InternalName}(params['{Screen.Entity.InternalName.Camelize()}Id']).subscribe(result => {{
                    this.{Screen.InternalName.Camelize()} = result;
                    this.setupForm();
                }}, error => console.error(error));
            }} else {{
                this.new = true;
{string.Join(Environment.NewLine, initSections)}
                this.setupForm();
            }}
        }});");

            foreach (var screenSection in ScreenSections)
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

            return lines;
        }

        /// <summary>
        /// Get Functions
        /// </summary>
        internal IEnumerable<string> GetFunctions()
        {
            var methods = new List<string>
            {
                @"    referenceCompare(referenceItem1: any, referenceItem2: any): boolean {{
        return referenceItem1 === referenceItem2;
    }}",
                $@"    canDeactivate(): Observable<boolean> | boolean {{
        // check if there are pending changes
        if (this.{Screen.InternalName.Camelize()}Form.pristine || !this.{Screen.InternalName.Camelize()}Form.valid) {{
            return true;
        }}
        return false;
    }}"
            };

            if (Project.UsePutForUpdate)
            {
                methods.Add($@"    onSubmit() {{ 
        // Don't submit if nothing has changed
        if (this.{Screen.InternalName.Camelize()}Form.pristine || !this.{Screen.InternalName.Camelize()}Form.valid) {{
            return;
        }}
        
        if (this.new){{
            // Post new
            this.{Screen.InternalName.Camelize()}Service.add{Screen.InternalName}(this.{Screen.InternalName.Camelize()}Form.getRawValue()).subscribe( id => {{
                this.router.navigate([this.router.url + '/' + id], {{ replaceUrl: true }});
            }},
                error => this.httpErrorService.handleError(this.{Screen.InternalName.Camelize()}Form, this.serverErrorMessages, error)
            );
        }} else {{
            // Put existing
            this.{Screen.InternalName.Camelize()}Service.update{Screen.InternalName}(this.{Screen.InternalName.Camelize()}.id, this.{Screen.InternalName.Camelize()}Form.getRawValue()).subscribe( result => {{
                this.{Screen.InternalName.Camelize()}Form.markAsPristine({{ onlySelf: false }});
            }},
                error => this.httpErrorService.handleError(this.{Screen.InternalName.Camelize()}Form, this.serverErrorMessages, error)
            );
        }}
    }}");
            }
            else
            {
                // Patch on submit
                methods.Add($@"    onSubmit() {{ 
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
    }}");

                // Create patch object
                methods.Add($@"    private getPatchOperations(): Operation[] {{
        let operations: Operation[] = [];

        Object.keys(this.{Screen.InternalName.Camelize()}Form.controls).forEach((name) => {{
            let currentControl = this.{Screen.InternalName.Camelize()}Form.controls[name];

            if (currentControl.dirty) {{
                let operation = new Operation();
                operation.op = 'replace';
                operation.path = '/' + name;
                if (currentControl.value instanceof Object) {{
                    if (currentControl.value._isAMomentObject) {{
                        operation.value = currentControl.value.toISOString();
                    }} else {{
                        operation.value = currentControl.value.id;
                    }}
                }} else {{
                    operation.value = currentControl.value;
                }}
                operations.push(operation);
            }}
        }});
        return operations;
    }}");
            }

            return methods;
        }

        /// <summary>
        /// Get Properties
        /// </summary>
        internal IEnumerable<string> GetProperties()
        {
            return new string[0];
        }
        
        private string GetValidationArray(FormField formField, int level = 0)
        {
            var propertyValidators = new List<string>();
            if (formField.Property.ValidationItems != null)
            {
                foreach (var validationItem in formField.Property.ValidationItems)
                {
                    switch (validationItem.ValidationType)
                    {
                        case ValidationType.Required:
                            propertyValidators.Add($"            {new string(' ', 4 * level)}Validators.required");
                            break;
                        case ValidationType.MaximumLength:
                            propertyValidators.Add($"            {new string(' ', 4 * level)}Validators.maxLength({validationItem.IntegerValue.Value})");
                            break;
                        case ValidationType.MinimumLength:
                            propertyValidators.Add($"            {new string(' ', 4 * level)}Validators.minLength({validationItem.IntegerValue.Value})");
                            break;
                        case ValidationType.MaximumValue:
                            propertyValidators.Add($"            {new string(' ', 4 * level)}Validators.max({validationItem.IntegerValue.Value})");
                            break;
                        case ValidationType.MinimumValue:
                            propertyValidators.Add($"            {new string(' ', 4 * level)}Validators.min({validationItem.IntegerValue.Value})");
                            break;
                        case ValidationType.Unique:
                            // TODO async validator or just let database let you know of the error?
                            break;
                        case ValidationType.Email:
                            propertyValidators.Add($"            {new string(' ', 4 * level)}Validators.email");
                            break;
                        case ValidationType.RequiredTrue:
                            propertyValidators.Add($"            {new string(' ', 4 * level)}Validators.requiredTrue");
                            break;
                        case ValidationType.Pattern:
                            propertyValidators.Add($@"            {new string(' ', 4 * level)}Validators.pattern(""{validationItem.StringValue}"")");
                            break;
                        default:
                            break;
                    }
                }
            }

            return (propertyValidators.Any() ?
                $",[{Environment.NewLine}{string.Join(string.Concat(",", Environment.NewLine), propertyValidators)}]" :
                string.Empty);
        }

        private IEnumerable<string> GetControls(IEnumerable<FormField> formFields, int level = 0)
        {
            var formControls = new List<string>();

            foreach (var group in formFields.GroupBy(ff => ff.EntityPropertyId))
            {
                var formField = group.First();
                if (formField.PropertyType == PropertyType.PrimaryKey)
                {
                    continue;
                }

                var propertyValidatorsString = GetValidationArray(formField, level);

                formControls.Add($@"        {new string(' ', 4 * level)}{formField.InternalNameCSharp.Camelize()}: new FormControl(null{propertyValidatorsString})");
            }

            return formControls;
        }

        internal IEnumerable<string> GetFormControls()
        {
            var formControls = new List<string>();
            var rootFields = (from formSection in ScreenSections
                              where !formSection.ParentEntityPropertyId.HasValue
                              from ff in formSection.FormSection.FormFields
                              select ff).ToArray();

            formControls.AddRange(GetControls(rootFields, 1));

            // TODO: implement child sections
            // Convert child properties to objects with properties
            //var childSections = (from formSection in ScreenSections
            //                     where formSection.ParentEntityPropertyId.HasValue
            //                     select formSection).ToArray();

            //foreach (var childItem in childSections.GroupBy(a => a.ParentEntityProperty).Select(a => new
            //{
            //    ParentEntityProperty = a.Key,
            //    ChildSections = a.ToArray()
            //}))
            //{
            //    var entityProperties = new List<string>();

            //    entityProperties.AddRange(
            //        GetControls((from screenSection in childItem.ChildSections
            //                     from ff in screenSection.FormSection.FormFields
            //                     select ff).ToArray(), 2));

            //    if (entityProperties.Any())
            //    {
            //        formControls.Add($@"            {childItem.ParentEntityProperty.InternalName.Camelize()}: new FormGroup({{
//{string.Join(string.Concat(",", Environment.NewLine), entityProperties)}
            //}})");
            //    }

            //}

            return formControls;
        }
    }
}
