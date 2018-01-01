using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Ts
{
    public class BuildForm
    {
        private readonly Project Project;
        private readonly Screen Screen;

        public BuildForm(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        public string GetFunctions()
        {
            var entity = Project.Entities.SingleOrDefault(p => p.Id == Screen.EntityId);

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
                    formControls.Add($@"        '{property.InternalName.Camelize()}Id': new FormControl(this.{Screen.InternalName.Camelize()}.{property.InternalName.Camelize()}Id{propertyValidatorsString})");
                    properties.Add($@"    get {property.InternalName.Camelize()}Id() {{ return this.{Screen.InternalName.Camelize()}Form.get('{property.InternalName.Camelize()}Id'); }}");
                }
                else if (property.Type == PropertyTypeEnum.ReferenceRelationship)
                {
                    formControls.Add($@"        '{property.InternalName.Camelize()}Id': new FormControl(this.{Screen.InternalName.Camelize()}.{property.InternalName.Camelize()}Id{propertyValidatorsString})");
                    properties.Add($@"    get {property.InternalName.Camelize()}Id() {{ return this.{Screen.InternalName.Camelize()}Form.get('{property.InternalName.Camelize()}Id'); }}");
                    properties.Add($@"    {property.InternalName.Camelize()}Options: any[] = [];");
                }
                else
                {
                    formControls.Add($@"        '{property.InternalName.Camelize()}': new FormControl(this.{Screen.InternalName.Camelize()}.{property.InternalName.Camelize()}{propertyValidatorsString})");
                    properties.Add($@"    get {property.InternalName.Camelize()}() {{ return this.{Screen.InternalName.Camelize()}Form.get('{property.InternalName.Camelize()}'); }}");
                }
            }

            return $@" 

    setupForm(){{
        this.{Screen.InternalName.Camelize()}Form = new FormGroup({{
{string.Join(string.Concat(",", Environment.NewLine), formControls)}
        }});
    }}

{string.Join(Environment.NewLine, properties)}

    private getPatchOperations(): Operation[] {{
        let operations: Operation[] = [];

        Object.keys(this.{Screen.InternalName.Camelize()}Form.controls).forEach((name) => {{
            let currentControl = this.{Screen.InternalName.Camelize()}Form.controls[name];

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
        if (this.{Screen.InternalName.Camelize()}Form.pristine || !this.{Screen.InternalName.Camelize()}Form.valid) {{
            return;
        }}
        
        if (this.new){{
            // Post new
            this.{entity.InternalName.Camelize()}Service.add{entity.InternalName}{Screen.InternalName}(this.{Screen.InternalName.Camelize()}Form.getRawValue()).subscribe( id => {{
                this.router.navigate([this.router.url + '/' + id]);
            }});
        }} else {{
            // Patch existing
            let operations = this.getPatchOperations();
            this.{entity.InternalName.Camelize()}Service.update{entity.InternalName}{Screen.InternalName}(this.{Screen.InternalName.Camelize()}.id, operations).subscribe( result => {{
                this.{Screen.InternalName.Camelize()}Form.markAsPristine({{ onlySelf: false }});
            }});
        }}
    }}";
        }
    }
}
