using MasterBuilder.Request;
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
        private readonly FormField FormField;

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateEditInputPartialTemplate(FormField formField)
        {
            FormField = formField;
        }


        public IEnumerable<string> ReactAdminImports()
        {
            var imports = new List<string>();

            switch (FormField.PropertyType)
            {
                case PropertyType.PrimaryKey:
                    break;
                case PropertyType.String:
                    imports.Add("TextInput");
                    break;
                case PropertyType.Integer:
                    imports.Add("NumberInput");
                    break;
                case PropertyType.DateTime:
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

            if (FormField.Property.ValidationItems != null)
            {
                foreach (var validationItem in FormField.Property.ValidationItems)
                {
                    switch (validationItem.ValidationType)
                    {
                        case ValidationType.Required:
                            imports.Add("required");
                            break;
                        case ValidationType.MaximumLength:
                            break;
                        case ValidationType.MinimumLength:
                            break;
                        case ValidationType.MaximumValue:
                            break;
                        case ValidationType.MinimumValue:
                            break;
                        case ValidationType.Unique:
                            break;
                        case ValidationType.Email:
                            break;
                        case ValidationType.RequiredTrue:
                            break;
                        case ValidationType.Pattern:
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

            switch (FormField.PropertyType)
            {
                case PropertyType.PrimaryKey:
                    break;
                case PropertyType.String:
                    type = "TextInput";
                    break;
                case PropertyType.Integer:
                    type = "NumberInput";
                    break;
                case PropertyType.DateTime:
                    break;
                case PropertyType.Boolean:
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
                    break;
                case PropertyType.Double:
                    type = "NumberInput";
                    break;
                case PropertyType.ParentRelationshipOneToOne:
                    break;
                case PropertyType.Uniqueidentifier:
                    break;
                case PropertyType.Spatial:
                    break;
            }

            if (FormField.Property.ValidationItems != null)
            {
                var validationList = new List<string>();
                foreach (var validationItem in FormField.Property.ValidationItems)
                {
                    switch (validationItem.ValidationType)
                    {
                        case ValidationType.Required:
                            validationList.Add("required()");
                            break;
                        case ValidationType.MaximumLength:
                            break;
                        case ValidationType.MinimumLength:
                            break;
                        case ValidationType.MaximumValue:
                            break;
                        case ValidationType.MinimumValue:
                            break;
                        case ValidationType.Unique:
                            break;
                        case ValidationType.Email:
                            break;
                        case ValidationType.RequiredTrue:
                            break;
                        case ValidationType.Pattern:
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
            
            return $@"<{type} source=""{FormField.InternalNameJavaScript}"" {validate}/>";
        }
    }
}
