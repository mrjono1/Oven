using Humanizer;
using MasterBuilder.Request;
using MasterBuilder.Request.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Api.Helpers
{
    /// <summary>
    /// Generate an C# expression
    /// </summary>
    public class CsExpressionPartial
    {
        private readonly Property Property;
        private readonly Expression Expression;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property">The property</param>
        /// <param name="expression">The Expression</param>
        public CsExpressionPartial(Property property, Expression expression)
        {
            Property = property;
            Expression = expression;
        }

        /// <summary>
        /// Get Generated Experession
        /// </summary>
        public string GetExpression(string objectName, string inputObjectName, Expression expression = null)
        {
            if (expression == null)
            {
                expression = Expression;
            }
            var propertyType = GetPropertyType();

            var firstPart = GetLeft(expression, objectName);
            var @operator = GetOperator(expression);
            var secondPart = GetRight(expression, propertyType, inputObjectName);

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

        internal IEnumerable<Property> GetFilterProperties()
        {
            var properties = new List<Property>();

            var property = Property.Entity.Properties.SingleOrDefault(prop => prop.Id == Expression.PropertyId.Value);
            if (property != null)
            {
                properties.Add(property);
            }

            return properties;
        }

        /// <summary>
        /// Resolve User Types to data types
        /// </summary>
        /// <returns>A data type</returns>
        private PropertyType GetPropertyType()
        {
            switch (Property.PropertyType)
            {
                case PropertyType.ParentRelationshipOneToMany:
                case PropertyType.PrimaryKey:
                case PropertyType.ReferenceRelationship:
                case PropertyType.ParentRelationshipOneToOne:
                    return PropertyType.Uniqueidentifier;
                default:
                    return Property.PropertyType;
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
                    return "==";
                case ExpressionOperator.NotEqual:
                    return "!=";
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
                    return "== null";
                case ExpressionOperator.IsNotNull:
                    return "!= null";
                case ExpressionOperator.Like:
                    throw new NotImplementedException("Like Operator not implemented");
            }
            
            throw new InvalidOperationException("operator not defined");
        }

        private string GetLeft(Expression expression, string objectName)
        {
            var property = Property.Entity.Properties.SingleOrDefault(prop => prop.Id == expression.PropertyId.Value);

            if (property.PropertyType == PropertyType.PrimaryKey)
            {
                return $@"{objectName}.{property.Entity.InternalName}{property.InternalNameCSharp}";
            }
            else
            {
                return $@"{objectName}.{property.InternalNameCSharp}";
            }
        }

        private string GetRight(Expression expression, PropertyType propertyType, string inputObjectName)
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
                    if (expression.ChildPropertyId.HasValue)
                    {
                        if (Property.ParentEntity != null)
                        {
                            var childProperty = Property.ParentEntity.Properties.SingleOrDefault(prop => prop.Id == expression.ChildPropertyId.Value);
                            return $@"{inputObjectName}.{childProperty.InternalNameCSharp}";
                        }
                    }
                    else if (expression.UniqueidentifierValue.HasValue)
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
