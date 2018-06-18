using Humanizer;
using MasterBuilder.Request;
using MasterBuilder.Request.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Evaluate
{
    /// <summary>
    /// Generate an TypeScript expression
    /// </summary>
    public class TsExpressionPartial
    {
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="screen">The Screen</param>
        /// <param name="screenSections">FormSections</param>
        public TsExpressionPartial(Screen screen, IEnumerable<ScreenSection> screenSections)
        {
            Screen = screen;
            ScreenSections = screenSections;
        }

        /// <summary>
        /// Get Generated Experession
        /// </summary>
        public string GetExpression(Expression expression)
        {
            var propertyType = GetPropertyType(expression);

            var firstPart = GetLeft(expression);
            var @operator = GetOperator(expression);
            var secondPart = GetRight(expression, propertyType);

            if (IsBasicOperation(expression))
            {
                return $@"{firstPart} {@operator} {secondPart}";
            }
            else if (IsRangeOperation(expression))
            {
                return $@"{secondPart}.indexOf({firstPart}) > -1";
            }
            else if (IsSingleOperation(expression))
            {
                return $@"{firstPart} {@operator}";
            }
            else
            {
                return "false";
            }
        }

        internal IEnumerable<FormField> GetFilterProperties(Expression expression)
        {
            var formFields = new List<FormField>();
            
            var formField = (from screenSection in ScreenSections
                                where screenSection.ScreenSectionType == ScreenSectionType.Form
                                from ff in screenSection.FormSection.FormFields
                                where ff.EntityPropertyId == expression.PropertyId
                                select ff).Single();
            formFields.Add(formField);

            return formFields;
        }

        private PropertyType GetPropertyType(Expression expression)
        {
            var property = (from screenSection in ScreenSections
                            where screenSection.ScreenSectionType == ScreenSectionType.Form
                            from ff in screenSection.FormSection.FormFields
                            where ff.EntityPropertyId == expression.PropertyId
                            select ff).Single();
            switch (property.PropertyType)
            {
                case PropertyType.ParentRelationshipOneToMany:
                case PropertyType.PrimaryKey:
                case PropertyType.ReferenceRelationship:
                case PropertyType.ParentRelationshipOneToOne:
                    return PropertyType.Uniqueidentifier;
                default:
                    return property.PropertyType;
            }
        }

        private bool IsBasicOperation(Expression expression)
        {
            switch (expression.Operator)
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.NotEqual:
                case ExpressionOperator.GreaterThan:
                case ExpressionOperator.GreaterThanOrEqual:
                case ExpressionOperator.LessThan:
                case ExpressionOperator.LessThanOrEqual:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsRangeOperation(Expression expression)
        {
            switch (expression.Operator)
            {
                case ExpressionOperator.In:
                case ExpressionOperator.NotIn:
                case ExpressionOperator.Like:
                    return true;
                default:
                    return false;
            }
            
        }

        private bool IsSingleOperation(Expression expression)
        {
            switch (expression.Operator)
            {
                case ExpressionOperator.IsNull:
                case ExpressionOperator.IsNotNull:
                    return true;
                default:
                    return false;
            }
        }

        private string GetOperator(Expression expression)
        {
            switch (expression.Operator)
            {
                case ExpressionOperator.Equal:
                    return "===";
                case ExpressionOperator.NotEqual:
                    return "!==";
                case ExpressionOperator.GreaterThan:
                    return ">";
                case ExpressionOperator.GreaterThanOrEqual:
                    return ">=";
                case ExpressionOperator.LessThan:
                    return "<";
                case ExpressionOperator.LessThanOrEqual:
                    return "<=";
                case ExpressionOperator.In:
                    return "indexOf";
                case ExpressionOperator.NotIn:
                    throw new NotImplementedException("Not In Operator not implemented");
                case ExpressionOperator.IsNull:
                    return "=== null || === undefined";
                case ExpressionOperator.IsNotNull:
                    return "!== null && !== undefined";
                case ExpressionOperator.Like:
                    throw new NotImplementedException("Like Operator not implemented");
            }
            
            throw new InvalidOperationException("operator not defined");
        }

        private string GetLeft(Expression expression)
        {
            var property = (from screenSection in ScreenSections
                            where screenSection.ScreenSectionType == ScreenSectionType.Form
                            from ff in screenSection.FormSection.FormFields
                            where ff.EntityPropertyId == expression.PropertyId
                            select ff).Single();

            return $@"(this.{Screen.InternalName.Camelize()}Form.get('{property.InternalNameJavaScript}') ? this.{Screen.InternalName.Camelize()}Form.get('{property.InternalNameJavaScript}').value : undefined)";
        }

        private string GetRight(Expression expression, PropertyType propertyType)
        {
            switch (propertyType)
            {
                case PropertyType.String:
                    if (!string.IsNullOrEmpty(expression.StringValue))
                    {
                        return $@"'{expression.StringValue}'";
                    }
                    break;
                case PropertyType.Integer:
                    if (expression.IntegerValue.HasValue)
                    {
                        return $@"{expression.IntegerValue.Value}";
                    }
                    break;
                case PropertyType.DateTime:
                    break;
                case PropertyType.Boolean:
                    if (expression.BooleanValue.HasValue)
                    {
                        return $@"{expression.BooleanValue.Value.ToString().ToLowerInvariant()}";
                    }
                    break;
                case PropertyType.Double:
                    if (expression.DoubleValue.HasValue)
                    {
                        return $@"{expression.DoubleValue.Value}";
                    }
                    break;
                case PropertyType.Uniqueidentifier:
                    if (expression.UniqueidentifierValue.HasValue)
                    {
                        return $@"'{expression.UniqueidentifierValue.ToString().ToLowerInvariant()}'";
                    }
                    else if (expression.UniqueidentifierValues != null && expression.UniqueidentifierValues.Any())
                    {
                        return string.Concat("['", string.Join("','", expression.UniqueidentifierValues), "']");
                    }
                    break;
            }
           
            return "null";
        }
    }
}
