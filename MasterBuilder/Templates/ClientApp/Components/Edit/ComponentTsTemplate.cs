using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Edit
{
    public class ComponentTsTemplate
    {
        public static string FileName(string folder, Screen screen)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", screen.InternalName.ToLowerInvariant())), $"{screen.InternalName.ToLowerInvariant()}.component.ts");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            var properties = new List<string>();
            var formControls = new List<string>();
            foreach (var property in entity.Properties)
            {
                properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");

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
                                break;
                            case ValidationTypeEnum.MinimumValue:
                                break;
                            case ValidationTypeEnum.Unique:
                                break;
                            default:
                                break;
                        }
                    }
                }

                var propertyValidatorsString = (propertyValidators.Any() ? $",[{Environment.NewLine}{string.Join(string.Concat(",", Environment.NewLine), propertyValidators)}]" : string.Empty);

                formControls.Add($@"        '{property.InternalName.ToCamlCase()}': new FormControl(this.{screen.InternalName.ToCamlCase()}.{property.InternalName.ToCamlCase()}{propertyValidatorsString})");
            }

            string cssFile = null;
            if (!string.IsNullOrEmpty(screen.Css))
            {
                cssFile = $@",
    styleUrls: ['./{screen.InternalName.ToLowerInvariant()}.component.css']";
            }

            return $@"import {{ Component, Inject, OnInit }} from '@angular/core';
import {{ Http }} from '@angular/http';
import {{ ActivatedRoute }} from '@angular/router';
import {{ FormControl,FormGroup, Validators }} from '@angular/forms';

@Component({{
    selector: '{screen.InternalName.ToLowerInvariant()}',
    templateUrl: './{screen.InternalName.ToLowerInvariant()}.component.html'{cssFile}
}})
export class {screen.InternalName}Component implements OnInit {{
    public {screen.InternalName.ToCamlCase()}: {screen.InternalName};
    public {screen.InternalName.ToCamlCase()}Form: FormGroup;
    public new: boolean;
    private sub: any;
    private submitted: boolean;

    constructor(private route: ActivatedRoute, 
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
        if (this.projectForm.pristine) {{
            return;
        }}
        
        //todo ensure validated
        this.submitted = true;
        
        let request = {{}};
        if (this.new){{
            // Post new
            this.http.post('api/{entity.InternalName}/{screen.InternalName}', this.{screen.InternalName.ToCamlCase()}).subscribe( results => {{
                alert(results);
            }});
        }} else {{
            // Patch existing
            let operations = this.getPatchOperations();
            this.http.patch('api/{entity.InternalName}/{screen.InternalName}/' + this.{screen.InternalName.ToCamlCase()}.id, operations).subscribe( results => {{
                alert(results);
                this.projectForm.markAsPristine(true);
            }});
        }}
    }}
}}

export class {screen.InternalName} {{
{string.Join(Environment.NewLine, properties)}
}}

export class Operation {{
    op: string;
    path: string;
    value: any;
}}";
        }
    }
}