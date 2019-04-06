using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Oven.Request
{
    /// <summary>
    /// Validation
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Register of all Validation Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, ValidationType> ValidationTypeDictonary = new Dictionary<ObjectId, ValidationType>
        {
            { new ObjectId("5ca877084a73264e4c06dfea"), ValidationType.Email },
            { new ObjectId("5ca877074a73264e4c06dfde"), ValidationType.MaximumLength },
            { new ObjectId("5ca877074a73264e4c06dfdf"), ValidationType.MaximumValue },
            { new ObjectId("5ca877074a73264e4c06dfe0"), ValidationType.MinimumLength },
            { new ObjectId("5ca877074a73264e4c06dfe1"), ValidationType.MinimumValue },
            { new ObjectId("5ca877074a73264e4c06dfe2"), ValidationType.Pattern },
            { new ObjectId("5ca877084a73264e4c06dfeb"), ValidationType.Required },
            { new ObjectId("5ca877084a73264e4c06dfec"), ValidationType.RequiredTrue },
            { new ObjectId("5ca877084a73264e4c06dfed"), ValidationType.Unique },
            { new ObjectId("5ca8770e4a73264e4c06e032"), ValidationType.RequiredExpression }
        };

        /// <summary>
        /// Validation Primary Key
        /// </summary>
        [NonDefault]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Validation Type Id
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId ValidationTypeId { get; set; }
        /// <summary>
        /// Validation Type Enum
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public ValidationType ValidationType
        {
            get
            {
                return ValidationTypeDictonary[ValidationTypeId];
            }
            set
            {
                ValidationTypeId = ValidationTypeDictonary.SingleOrDefault(v => v.Value == value).Key;
            }
        }
        /// <summary>
        /// Required for data type integer and validation types: MaximumLength, MinimumLength, MaximumValue, MinimumValue
        /// </summary>
        public int? IntegerValue { get; set; }
        /// <summary>
        /// Required for data type double and validation types: MaximumLength, MinimumLength, MaximumValue, MinimumValue
        /// </summary>
        public double? DoubleValue { get; set; }
        /// <summary>
        /// Required for validation types: Pattern
        /// </summary>
        public string StringValue { get; set; }
        /// <summary>
        /// Required for validation type: Requried Expression
        /// </summary>
        public Expression Expression { get; set; }
        /// <summary>
        /// Optional: Validation Failure Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get the validation failure message, auto generated if not specifed by the <seealso cref="Message"/>Message property
        /// </summary>
        /// <param name="propertyTitle">The Failing Property Title</param>
        /// <returns>Validation failure message</returns>
        public string GetMessage(string propertyTitle)
        {
            if (string.IsNullOrEmpty(Message))
            {
                switch (ValidationType)
                {
                    case ValidationType.Required:
                        return $"{propertyTitle} is requried";
                    case ValidationType.MaximumLength:
                        return $"{propertyTitle} must be at less than {IntegerValue} characters in length";
                    case ValidationType.MinimumLength:
                        return $"{propertyTitle} must be at least {IntegerValue} characters in length";
                    case ValidationType.MaximumValue:
                        return $"{propertyTitle} must be less than {IntegerValue}";
                    case ValidationType.MinimumValue:
                        return $"{propertyTitle} must be greater than {IntegerValue}";
                    case ValidationType.Unique:
                        return $"{propertyTitle} must be unique";
                    case ValidationType.Email:
                        return $"{propertyTitle} must be a valid email";
                    case ValidationType.RequiredTrue:
                        return $"{propertyTitle} must be true";
                    case ValidationType.Pattern:
                        return $"{propertyTitle} does not match the complexity";
                    case ValidationType.RequiredExpression:
                        return $"{propertyTitle} is required";
                    default:
                        return "Invalid";
                }
            }
            else
            {
                return Message;
            }
        }
    }
}
