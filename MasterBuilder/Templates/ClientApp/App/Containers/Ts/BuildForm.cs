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

                if (property.Type == PropertyTypeEnum.ParentRelationship || property.Type == PropertyTypeEnum.ReferenceRelationship)
                {
                    formControls.Add($@"        '{property.InternalName.ToCamlCase()}Id': new FormControl(this.{Screen.InternalName.ToCamlCase()}.{property.InternalName.ToCamlCase()}Id{propertyValidatorsString})");
                    properties.Add($@"    get {property.InternalName.ToCamlCase()}Id() {{ return this.{Screen.InternalName.ToCamlCase()}Form.get('{property.InternalName.ToCamlCase()}Id'); }}");
                }
                else
                {
                    formControls.Add($@"        '{property.InternalName.ToCamlCase()}': new FormControl(this.{Screen.InternalName.ToCamlCase()}.{property.InternalName.ToCamlCase()}{propertyValidatorsString})");
                    properties.Add($@"    get {property.InternalName.ToCamlCase()}() {{ return this.{Screen.InternalName.ToCamlCase()}Form.get('{property.InternalName.ToCamlCase()}'); }}");
                }
            }

            return $@" 

    setupForm(){{
        this.{Screen.InternalName.ToCamlCase()}Form = new FormGroup({{
{string.Join(string.Concat(",", Environment.NewLine), formControls)}
        }});
    }}

{string.Join(Environment.NewLine, properties)}

    private getPatchOperations(): Operation[] {{
        let operations: Operation[] = [];

        Object.keys(this.{Screen.InternalName.ToCamlCase()}Form.controls).forEach((name) => {{
            let currentControl = this.{Screen.InternalName.ToCamlCase()}Form.controls[name];

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
        if (this.{Screen.InternalName.ToCamlCase()}Form.pristine || !this.{Screen.InternalName.ToCamlCase()}Form.valid) {{
            return;
        }}
        
        if (this.new){{
            // Post new
            this.{entity.InternalName.ToCamlCase()}Service.add{entity.InternalName}{Screen.InternalName}(this.{Screen.InternalName.ToCamlCase()}Form.getRawValue()).subscribe( id => {{
                this.router.navigate([this.router.url + '/' + id]);
            }});
        }} else {{
            // Patch existing
            let operations = this.getPatchOperations();
            this.{entity.InternalName.ToCamlCase()}Service.update{entity.InternalName}{Screen.InternalName}(this.{Screen.InternalName.ToCamlCase()}.id, operations).subscribe( result => {{
                this.{Screen.InternalName.ToCamlCase()}Form.markAsPristine({{ onlySelf: false }});
            }});
        }}
    }}";
        }
    }
}
