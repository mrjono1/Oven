using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen
{
    public class ScreenTypeEditTsTemplate
    {
        public static string Evaluate(Project project, Request.Screen screen)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screen.EntityId);

            var sectionImports = new List<string>();
            var sections = new List<string>();
            var properties = new List<string>();

            var formControls = new List<string>();
            foreach (var property in entity.Properties)
            {
                if (property.PropertyTemplate == PropertyTemplateEnum.PrimaryKey)
                {
                    continue;
                }

                var propertyValidators = new List<string>();
                if (property.ValidationItems != null)
                {
                    foreach (var validationItem in property.ValidationItems)
                    {
                        switch (validationItem.ValidationType)
                        {
                            case ValidationTypeEnum.Required:
                                propertyValidators.Add("            Validators.required");
                                break;
                            case ValidationTypeEnum.MaximumLength:
                                propertyValidators.Add($"            Validators.maxLength({validationItem.IntegerValue.Value})");
                                break;
                            case ValidationTypeEnum.MinimumLength:
                                propertyValidators.Add($"            Validators.minLength({validationItem.IntegerValue.Value})");
                                break;
                            case ValidationTypeEnum.MaximumValue:
                                propertyValidators.Add($"            Validators.max({validationItem.IntegerValue.Value})");
                                break;
                            case ValidationTypeEnum.MinimumValue:
                                propertyValidators.Add($"            Validators.min({validationItem.IntegerValue.Value})");
                                break;
                            case ValidationTypeEnum.Unique:
                                break;
                            case ValidationTypeEnum.Email:
                                propertyValidators.Add($"            Validators.email");
                                break;
                            case ValidationTypeEnum.RequiredTrue:
                                propertyValidators.Add($"            Validators.requiredTrue");
                                break;
                            case ValidationTypeEnum.Pattern:
                                propertyValidators.Add($"            Validators.pattern({validationItem.StringValue})");
                                break;
                            default:
                                break;
                        }
                    }
                }

                var propertyValidatorsString = (propertyValidators.Any() ? $",[{Environment.NewLine}{string.Join(string.Concat(",", Environment.NewLine), propertyValidators)}]" : string.Empty);

                if (property.Type == PropertyTypeEnum.ParentRelationship)
                {
                    formControls.Add($@"        '{property.InternalName.ToCamlCase()}Id': new FormControl(this.{screen.InternalName.ToCamlCase()}.{property.InternalName.ToCamlCase()}Id{propertyValidatorsString})");
                    properties.Add($@"    get {property.InternalName.ToCamlCase()}Id() {{ return this.{screen.InternalName.ToCamlCase()}Form.get('{property.InternalName.ToCamlCase()}Id'); }}");
                }
                else
                {
                    formControls.Add($@"        '{property.InternalName.ToCamlCase()}': new FormControl(this.{screen.InternalName.ToCamlCase()}.{property.InternalName.ToCamlCase()}{propertyValidatorsString})");
                    properties.Add($@"    get {property.InternalName.ToCamlCase()}() {{ return this.{screen.InternalName.ToCamlCase()}Form.get('{property.InternalName.ToCamlCase()}'); }}");
                }
            }

            return $@" 

    setupForm(){{
        this.{screen.InternalName.ToCamlCase()}Form = new FormGroup({{
{string.Join(string.Concat(",", Environment.NewLine), formControls)}
        }});
    }}

{string.Join(Environment.NewLine, properties)}

    private getPatchOperations(): Operation[] {{
        let operations: Operation[] = [];

        Object.keys(this.{screen.InternalName.ToCamlCase()}Form.controls).forEach((name) => {{
            let currentControl = this.{screen.InternalName.ToCamlCase()}Form.controls[name];

            if (currentControl.dirty) {{
                let operation = new Operation();
                operation.op = 'replace';
                operation.path = '/' + name;
                operation.value = currentControl.value;
                operations.push(operation);
            }}
        }});
        return operations;
    }}

    onSubmit() {{ 
        // Don't submit if nothing has changed
        if (this.{screen.InternalName.ToCamlCase()}Form.pristine || !this.{screen.InternalName.ToCamlCase()}Form.valid) {{
            return;
        }}
        
        if (this.new){{
            // Post new
            this.http.post('api/{entity.InternalName}/{screen.InternalName}', this.{screen.InternalName.ToCamlCase()}Form.getRawValue()).subscribe( result => {{
                if (result.status === 200){{
                    this.router.navigate(['{screen.InternalName.ToCamlCase()}/' + result.json()]);
                }} else {{
                    alert(result);
                }}
            }});
        }} else {{
            // Patch existing
            let operations = this.getPatchOperations();
            this.http.patch('api/{entity.InternalName}/{screen.InternalName}/' + this.{screen.InternalName.ToCamlCase()}.id, operations).subscribe( result => {{
                if (result.status === 200){{
                    this.{screen.InternalName.ToCamlCase()}Form.markAsPristine({{ onlySelf: false }});
                }} else {{
                    alert(result);
                }}
            }});
        }}
    }}";
        }

        internal static IEnumerable<string> Imports(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        results.AddRange(ScreenTypeEdit.SectionTypeFormTsTemplate.Imports(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Search:
                        results.AddRange(ScreenTypeSearch.SectionTypeSearchTsTemplate.Imports(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }
            }
            return results.Distinct();
        }

        internal static IEnumerable<string> ClassProperties(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        results.AddRange(ScreenTypeEdit.SectionTypeFormTsTemplate.ClassProperties(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Search:
                        results.AddRange(ScreenTypeSearch.SectionTypeSearchTsTemplate.ClassProperties(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }
            }

            results.Add("public new: boolean;");

            return results.Distinct();
        }

        internal static IEnumerable<string> ConstructorParameters(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        results.AddRange(ScreenTypeEdit.SectionTypeFormTsTemplate.ConstructorParameters(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Search:
                        results.AddRange(ScreenTypeSearch.SectionTypeSearchTsTemplate.ConstructorParameters(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }
            }
            return results.Distinct();
        }

        internal static IEnumerable<string> ConstructorBody(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        results.Add(ScreenTypeEdit.SectionTypeFormTsTemplate.ConstructorBody(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Search:
                        results.Add(ScreenTypeSearch.SectionTypeSearchTsTemplate.ConstructorBody(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }
            }
            return results.Distinct();
        }

        internal static IEnumerable<string> NgInitBody(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        results.Add(ScreenTypeEdit.SectionTypeFormTsTemplate.NgOnInitBody(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Search:
                        results.Add(ScreenTypeSearch.SectionTypeSearchTsTemplate.NgOnInitBody(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }
            }
            return results.Distinct();
        }

        internal static IEnumerable<string> Classes(Project project, Request.Screen screen)
        {
            var results = new List<string>();
            foreach (var section in screen.ScreenSections)
            {
                switch (section.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        results.AddRange(ScreenTypeEdit.SectionTypeFormTsTemplate.Classes(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Search:
                        results.AddRange(ScreenTypeSearch.SectionTypeSearchTsTemplate.Classes(project, screen, section));
                        break;
                    case ScreenSectionTypeEnum.Grid:
                        break;
                    case ScreenSectionTypeEnum.Html:
                        break;
                    default:
                        break;
                }
            }

            // Patch operation
            results.Add(@"export class Operation {
    op: string;
    path: string;
    value: any;
}");
            return results.Distinct();
        }
    }
}