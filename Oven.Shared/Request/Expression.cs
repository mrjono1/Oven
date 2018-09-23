using Newtonsoft.Json;
using Oven.Request.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Oven.Request
{
    /// <summary>
    /// Logic Rule
    /// </summary>
    public partial class Expression
    {
        internal bool IsExpressionSet
        {
            get
            {
                return Condition.HasValue;
            }
        }
        /// <summary>
        /// If you just want the expression to return true/false use this property
        /// </summary>
        public bool? Value { get; set; }
        /// <summary>
        /// And/Or
        /// </summary>
        public ExpresssionCondition? Condition { get; set; }
        /// <summary>
        /// Rules
        /// </summary>
        public IEnumerable<Expression> Expressions { get; set; }
        /// <summary>
        /// Specify Entity Id if the Property Id is not on the current Entity
        /// </summary>
        [NonDefault]
        public Guid? EntityId { get; set; }
        /// <summary>
        /// Entity Property Id
        /// </summary>
        [NonDefault]
        public Guid? PropertyId { get; set; }
        /// <summary>
        /// =/!=/> etc
        /// </summary>
        public ExpressionOperator? Operator { get; set; }

        #region Strongly Typed Values
        /// <summary>
        /// Boolean Value
        /// </summary>
        public bool? BooleanValue { get; set; }
        /// <summary>
        /// String Value
        /// </summary>
        public string StringValue { get; set; }
        /// <summary>
        /// Integer Value
        /// </summary>
        public int? IntegerValue { get; set; }
        /// <summary>
        /// Double Value
        /// </summary>
        public double? DoubleValue { get; set; }
        /// <summary>
        /// Uniqueidentifier Value
        /// </summary>
        public Guid? UniqueidentifierValue { get; set; }
        /// <summary>
        /// String Values
        /// </summary>
        public IEnumerable<string> StringValues { get; set; }
        /// <summary>
        /// Integer Values
        /// </summary>
        public IEnumerable<int> IntegerValues { get; set; }
        /// <summary>
        /// Double Values
        /// </summary>
        public IEnumerable<double> DoubleValues { get; set; }
        /// <summary>
        /// Uniqueidentifier Value
        /// </summary>
        public IEnumerable<Guid> UniqueidentifierValues { get; set; }
        /// <summary>
        /// Child Property Id
        /// </summary>
        public Guid? ReferencePropertyId { get; set; }
        #endregion

        #region Resolved Fields
        /// <summary>
        /// Specify Entity if the Property Id is not on the current Entity
        /// </summary>
        [NonDefault]
        [JsonIgnore]
        public Entity Entity { get; set; }
        #endregion
    }
}
