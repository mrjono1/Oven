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
        public static IEnumerable<string> Imports(Project project, Request.Screen screen)
        {
            var imports = new List<string>
            {
                "import { FormControl, FormGroup, Validators } from '@angular/forms';"
            };

            return imports;
        }


        public static string Evaluate(Project project, Request.Screen screen)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screen.EntityId);

            var sectionImports = new List<string>();
            var sections = new List<string>();
            var properties = new List<string>();

            foreach (var section in screen.ScreenSections)
            {

            }
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

                formControls.Add($@"        '{property.InternalName.ToCamlCase()}': new FormControl(this.{screen.InternalName.ToCamlCase()}.{property.InternalName.ToCamlCase()}{propertyValidatorsString})");
                properties.Add($@"    get {property.InternalName.ToCamlCase()}() {{ return this.{screen.InternalName.ToCamlCase()}Form.get('{property.InternalName.ToCamlCase()}'); }}");
            }

            return $@"    constructor(private route: ActivatedRoute,
                private router: Router,
                private http: Http) {{ }}

    ngOnInit(){{
        this.sub = this.route.params.subscribe(params => {{
            if (params['id']) {{
                this.new = false;
                this.http.get('api/{entity.InternalName}/{screen.InternalName}/' + params['id']).subscribe(result => {{
                    this.{screen.InternalName.ToCamlCase()} = result.json() as {screen.InternalName};
                    this.setupForm();
                }}, error => console.error(error));
            }} else {{
                this.new = true;
                this.{screen.InternalName.ToCamlCase()} = new {screen.InternalName}();
                this.setupForm();
            }}
        }});
    }}

    setupForm(){{
        this.{screen.InternalName.ToCamlCase()}Form = new FormGroup({{
{string.Join(string.Concat(",", Environment.NewLine), formControls)}
        }});
    }}

{string.Join(Environment.NewLine, properties)}

    private getPatchOperations(): Operation[] {{
        let operations: Operation[] = [];

        Object.keys(this.projectForm.controls).forEach((name) => {{
            let currentControl = this.projectForm.controls[name];

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
        if (this.projectForm.pristine || !this.projectForm.valid) {{
            return;
        }}
        
        //todo ensure validated
        this.submitted = true;
        
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
                    this.projectForm.markAsPristine({{ onlySelf: false }});
                }} else {{
                    alert(result);
                }}
            }});
        }}
    }}";
        }

        internal static IEnumerable<string> Classes(Project project, Request.Screen screen)
        {
            var entity = project.Entities.SingleOrDefault(p => p.Id == screen.EntityId);

            var properties = new List<string>();
            foreach (var property in entity.Properties)
            {
                properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");
            }

            return new string[]
            {
                $@"export class {screen.InternalName} {{
{string.Join(Environment.NewLine, properties)}
}}",
                @"export class Operation {
    op: string;
    path: string;
    value: any;
}"
            };
        }

        internal static IEnumerable<string> ClassProperties(Project project, Request.Screen screen)
        {
            var properties = new List<string>
            {
                $@"public {screen.InternalName.ToCamlCase()}: {screen.InternalName};",
                $"public {screen.InternalName.ToCamlCase()}Form: FormGroup;",
                "public new: boolean;",
                "private sub: any;",
                "private submitted: boolean;"
            };

            return properties;
        }
    }
}