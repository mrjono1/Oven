using Humanizer;
using Oven.Request;
using Oven.Templates.React.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oven.Templates.React.ClientApp.Src.Resources
{
    /// <summary>
    /// Input Field Template
    /// </summary>
    public class CreateEditInputPartialTemplate
    {
        private readonly Screen Screen;
        private readonly FormField FormField;
        private readonly string Source;

        public bool WrapInFormDataConsumer { get; private set; }
        public List<string> Constants { get; private set; } = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateEditInputPartialTemplate(Screen screen, FormField formField, string source)
        {
            Screen = screen;
            FormField = formField;
            Source = source;
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
                        defaultValue = $@"""{FormField.Property.DefaultStringValue}""";
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
                        Constants.Add($@"const validate{FormField.Property.InternalName} = [{string.Join(", ", validationList)}];");
                        validate = $@"validate={{validate{FormField.Property.InternalName}}} ";
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
            var includeRest = false;
            if (FormField.VisibilityExpression != null)
            {
                rest = "{...rest} ";
                includeRest = true;
            }

            // Create Element
            var element = "";
            var expressionItems = new List<string>();
            switch (FormField.PropertyType)
            {
                case PropertyType.ReferenceRelationship:
                    var filter = "";
                    if (FormField.Property.FilterExpression != null)
                    {
                        var referenceProperty = FormField.Property.ReferenceEntity.Properties.Single(a => a.Id == FormField.Property.FilterExpression.ReferencePropertyId);
                        
                        if (FormField.Property.FilterExpression.EntityId == null)
                        {
                            var localProperty = Screen.Entity.Properties.SingleOrDefault(a => a.Id == FormField.Property.FilterExpression.PropertyId);

                            // May be empty if the property is from a parent record so it should be the same as the reference property
                            // The backend does the hard work here
                            if (localProperty == null)
                            {
                                localProperty = referenceProperty;
                            }

                            if (!localProperty.InternalNameJavaScript.Equals("id"))
                            {
                                WrapInFormDataConsumer = true;
                            }
                            filter = $@"filter={{{{{referenceProperty.InternalNameJavaScript}: {(WrapInFormDataConsumer ? "formData" : "props") }.{localProperty.InternalNameJavaScript}}}}} ";
                            expressionItems.Add($@"{(WrapInFormDataConsumer ? "formData" : "props") }.{localProperty.InternalNameJavaScript}");
                        }
                        else
                        {
                            WrapInFormDataConsumer = true;
                            var entityProperty = FormField.Property.FilterExpression.Entity.Properties.Where(a => a.Id == FormField.Property.FilterExpression.PropertyId).Single();
                            filter = $@"filter={{{{{referenceProperty.InternalNameJavaScript}: formData.{entityProperty.InternalNameJavaScript}}}}} ";
                            expressionItems.Add($"formData.{entityProperty.InternalNameJavaScript}");
                        }
                    }
                    element = $@"<ReferenceInput title=""{FormField.TitleValue}"" source=""{Source}{FormField.InternalNameJavaScript}"" reference=""{FormField.Property.ReferenceEntity.InternalNamePlural}"" {filter}{validate}{rest}>
    <SelectInput optionText=""title"" />
</ReferenceInput>";
                    break;
                default:
                    element = $@"<{type} title=""{FormField.TitleValue}"" source=""{Source}{FormField.InternalNameJavaScript}"" {validate}{defaultValue}{rest}/>";
                    break;
            }
            
            // Visibility
            if (FormField.VisibilityExpression != null)
            {
                var expressionHelper = new ExpressionHelper(Screen);
                expressionItems.Add(expressionHelper.GetExpression(FormField.VisibilityExpression, "formData"));
                WrapInFormDataConsumer = true;
            }

            if (WrapInFormDataConsumer)
            {
                if (expressionItems.Any())
                {
                    element = $@"{string.Join(string.Join(Environment.NewLine, " && "), expressionItems)} &&
{element}";
                }
                return $@"<FormDataConsumer>
    {{({{ formData{(includeRest ? ", ...rest": "")} }}) => 
{element.IndentLines(8)}
    }}
</FormDataConsumer>";
            }
            else
            {
                // Return Result
                return element;
            }
        }
    }
}
