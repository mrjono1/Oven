using Humanizer;
using MasterBuilder.Request;
using MasterBuilder.Templates.React.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.React.Src.Resources
{
    /// <summary>
    /// Input Field Template
    /// </summary>
    public class CreateEditInputPartialTemplate
    {
        private readonly Screen Screen;
        private readonly FormField FormField;
        private readonly bool IsCreate;

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateEditInputPartialTemplate(Screen screen, FormField formField, bool isCreate)
        {
            Screen = screen;
            FormField = formField;
            IsCreate = isCreate;
        }


        public IEnumerable<string> ReactAdminImports()
        {
            var imports = new List<string>();

            // Property Type
            switch (FormField.PropertyType)
            {
                case PropertyType.PrimaryKey:
                    // Not shown on UI
                    break;
                case PropertyType.String:
                    imports.Add("TextInput");
                    break;
                case PropertyType.Integer:
                    imports.Add("NumberInput");
                    break;
                case PropertyType.DateTime:
                    imports.Add("DateInput");
                    break;
                case PropertyType.Boolean:
                    if (FormField.Property.Required)
                    {
                        imports.Add("BooleanInput");
                    }
                    else
                    {
                        imports.Add("NullableBooleanInput");
                    }
                    break;
                case PropertyType.ParentRelationshipOneToMany:
                    break;
                case PropertyType.ReferenceRelationship:
                    if (IsCreate && FormField.Property.FilterExpression != null)
                    {
                        // dont add this type to a create screen
                        return imports;
                    }
                    imports.Add("ReferenceInput");
                    imports.Add("SelectInput");
                    break;
                case PropertyType.Double:
                    imports.Add("NumberInput");
                    break;
                case PropertyType.ParentRelationshipOneToOne:
                    break;
                case PropertyType.Uniqueidentifier:
                    break;
                case PropertyType.Spatial:
                    break;
            }

            // Validation
            if (FormField.Property.ValidationItems != null)
            {
                foreach (var validationItem in FormField.Property.ValidationItems)
                {
                    switch (validationItem.ValidationType)
                    {
                        case ValidationType.Required:
                            if (FormField.PropertyType != PropertyType.Boolean)
                            {
                                imports.Add("required");
                            }
                            break;
                        case ValidationType.MaximumLength:
                            imports.Add("maxLength");
                            break;
                        case ValidationType.MinimumLength:
                            imports.Add("minLength");
                            break;
                        case ValidationType.MaximumValue:
                            imports.Add("maxValue");
                            break;
                        case ValidationType.MinimumValue:
                            imports.Add("minValue");
                            break;
                        case ValidationType.Unique:
                            break;
                        case ValidationType.Email:
                            imports.Add("email");
                            break;
                        case ValidationType.RequiredTrue:
                            imports.Add("required");
                            break;
                        case ValidationType.Pattern:
                            imports.Add("regex");
                            break;
                        case ValidationType.RequiredExpression:
                            break;
                    }
                }
            }

            // Visibility
            if (FormField.VisibilityExpression != null)
            {
                imports.Add("FormDataConsumer");
            }

            return imports;
        }

        public string Content()
        {
            var type = "TextInput";
            var validate = "";
            var defaultValue = "";

            // Property Type
            switch (FormField.PropertyType)
            {
                case PropertyType.PrimaryKey:
                    // Not shown on UI
                    break;
                case PropertyType.String:
                    type = "TextInput";
                    if (!string.IsNullOrWhiteSpace(FormField.Property.DefaultStringValue))
                    {
                        defaultValue = $@"""{defaultValue}""";
                    }
                    break;
                case PropertyType.Integer:
                    if (FormField.Property.DefaultIntegerValue.HasValue)
                    {
                        defaultValue = $@"{{{FormField.Property.DefaultIntegerValue.Value}}}";
                    }
                    type = "NumberInput";
                    break;
                case PropertyType.DateTime:
                    type = "DateInput";
                    break;
                case PropertyType.Boolean:
                    if (FormField.Property.DefaultBooleanValue.HasValue)
                    {
                        defaultValue = $@"{{{(FormField.Property.DefaultBooleanValue.Value ? "true" : "false")}}}";
                    }
                    if (FormField.Property.Required)
                    {
                        type = "BooleanInput";
                    }
                    else
                    {
                        type = "NullableBooleanInput";
                    }
                    break;
                case PropertyType.ParentRelationshipOneToMany:
                    break;
                case PropertyType.ReferenceRelationship:
                    if (IsCreate && FormField.Property.FilterExpression != null)
                    {
                        // dont add this type to a create screen
                        return null;
                    }
                    type = "ReferenceInput";
                    break;
                case PropertyType.Double:
                    if (FormField.Property.DefaultDoubleValue.HasValue)
                    {
                        defaultValue = $@"{{{FormField.Property.DefaultDoubleValue.Value}}}";
                    }
                    type = "NumberInput";
                    break;
                case PropertyType.ParentRelationshipOneToOne:
                    break;
                case PropertyType.Uniqueidentifier:
                    break;
                case PropertyType.Spatial:
                    break;
            }

            // Validation
            if (FormField.Property.ValidationItems != null)
            {
                var validationList = new List<string>();
                foreach (var validationItem in FormField.Property.ValidationItems)
                {
                    switch (validationItem.ValidationType)
                    {
                        case ValidationType.Required:
                            if (FormField.PropertyType != PropertyType.Boolean)
                            {
                                validationList.Add("required()");
                            }
                            break;
                        case ValidationType.MaximumLength:
                            validationList.Add($"maxLength({validationItem.IntegerValue})");
                            break;
                        case ValidationType.MinimumLength:
                            validationList.Add($"minLength({validationItem.IntegerValue})");
                            break;
                        case ValidationType.MaximumValue:
                            if (FormField.PropertyType == PropertyType.Integer)
                            {
                                validationList.Add($"maxValue({validationItem.IntegerValue})");
                            }
                            else if (FormField.PropertyType == PropertyType.Double)
                            {
                                validationList.Add($"maxValue({validationItem.DoubleValue})");
                            }
                            break;
                        case ValidationType.MinimumValue:
                            if (FormField.PropertyType == PropertyType.Integer)
                            {
                                validationList.Add($"minValue({validationItem.IntegerValue})");
                            }
                            else if (FormField.PropertyType == PropertyType.Double)
                            {
                                validationList.Add($"minValue({validationItem.DoubleValue})");
                            }
                            break;
                        case ValidationType.Unique:
                            break;
                        case ValidationType.Email:
                            validationList.Add("email()");
                            break;
                        case ValidationType.RequiredTrue:
                            validationList.Add("required()");
                            break;
                        case ValidationType.Pattern:
                            validationList.Add($"regex(/{validationItem.StringValue}/)");
                            break;
                        case ValidationType.RequiredExpression:
                            break;
                    }
                }
                if (validationList.Any())
                {
                    if (validationList.Count == 1)
                    {
                        validate = $@"validate={{{validationList.Single()}}} ";
                    }
                    else
                    {
                        validate = $@"validate={{[{string.Join(", ", validationList)}]}} ";
                    }
                }
            }

            // Default Value
            if (!string.IsNullOrEmpty(defaultValue))
            {
                defaultValue = $@"defaultValue={defaultValue} ";
            }

            // Include ...rest
            var rest = "";
            if (FormField.VisibilityExpression != null)
            {
                rest = "{...rest} ";
            }

            // Create Element
            var element = "";
            switch (FormField.PropertyType)
            {
                case PropertyType.ReferenceRelationship:
                    var filterExpression = "";
                    if (FormField.Property.FilterExpression != null)
                    {
                        // ToDo: fix this
                        filterExpression = $@"filter={{{{{FormField.Property.Entity.InternalName.Camelize()}Id: props.id}}}} ";
                    }
                    element = $@"<ReferenceInput title=""{FormField.TitleValue}"" source=""{FormField.InternalNameJavaScript}"" {validate}reference=""{FormField.Property.ParentEntity.InternalNamePlural}"" {filterExpression}{validate}{rest}>
    <SelectInput optionText=""title"" />
</ReferenceInput>";
                    break;
                default:
                    element = $@"<{type} title=""{FormField.TitleValue}"" source=""{FormField.InternalNameJavaScript}"" {validate}{defaultValue}{rest}/>";
                    break;
            }

            // Visibility
            if (FormField.VisibilityExpression != null)
            {
                var expressionHelper = new ExpressionHelper(Screen);
                var expression = expressionHelper.GetExpression(FormField.VisibilityExpression, "formData");
                element = $@"<FormDataConsumer>
    {{({{ formData, ...rest }}) => 
        {expression} &&
    {element.IndentLines(8)}
    }}
</FormDataConsumer>";
            }

            // Return Result
            return element;

        }
    }
}
