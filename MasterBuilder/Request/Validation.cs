using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Validation
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Register of all Validation Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, ValidationTypeEnum> ValidationTypeDictonary = new Dictionary<Guid, ValidationTypeEnum>
        {
            { new Guid("{17CC19D3-8E91-432E-98F7-4D9368DE3C44}"), ValidationTypeEnum.Email },
            { new Guid("{F7788E3D-7753-4491-98B1-AE78E16CDD0E}"), ValidationTypeEnum.MaximumLength },
            { new Guid("{0046F484-17EB-4665-AE59-45189BB203A9}"), ValidationTypeEnum.MaximumValue },
            { new Guid("{35D78EB6-F5DE-4E7B-AE79-B69A1D3DC7C9}"), ValidationTypeEnum.MinimumLength },
            { new Guid("{A679CB09-DE53-42F7-BB89-7E29947B51A1}"), ValidationTypeEnum.MinimumValue },
            { new Guid("{C0A88F1A-AAA8-47DA-A75B-94490915616C}"), ValidationTypeEnum.Pattern },
            { new Guid("{BD110234-F05D-42AB-BF2E-382B83093D0C}"), ValidationTypeEnum.Required },
            { new Guid("{CB9A60D3-42B3-411F-8FCE-2FC36C812A16}"), ValidationTypeEnum.RequiredTrue },
            { new Guid("{890C7A9E-09AE-4BB8-970E-85C564F753F1}"), ValidationTypeEnum.Unique }
        };

        /// <summary>
        /// Validation Primary Key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Validation Type Id
        /// </summary>
        public Guid ValidationTypeId { get; set; }

        /// <summary>
        /// Validation Type Enum
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public ValidationTypeEnum ValidationType
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
        /// Optional: Validation Failure Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get the validation failure message, auto generated if not specifed by the <seealso cref="Message"/>Message property
        /// </summary>
        /// <param name="propertyTitle">The Failing Property Title</param>
        /// <returns>Validation failure message</returns>
        internal string GetMessage(string propertyTitle)
        {
            if (string.IsNullOrEmpty(Message))
            {
                switch (ValidationType)
                {
                    case ValidationTypeEnum.Required:
                        return $"{propertyTitle} is requried";
                    case ValidationTypeEnum.MaximumLength:
                        return $"{propertyTitle} must be at less than {IntegerValue} characters in length";
                    case ValidationTypeEnum.MinimumLength:
                        return $"{propertyTitle} must be at least {IntegerValue} characters in length";
                    case ValidationTypeEnum.MaximumValue:
                        return $"{propertyTitle} must be less than {IntegerValue}";
                    case ValidationTypeEnum.MinimumValue:
                        return $"{propertyTitle} must be greater than {IntegerValue}";
                    case ValidationTypeEnum.Unique:
                        return $"{propertyTitle} must be unique";
                    case ValidationTypeEnum.Email:
                        return $"{propertyTitle} must be a valid email";
                    case ValidationTypeEnum.RequiredTrue:
                        return $"{propertyTitle} must be true";
                    case ValidationTypeEnum.Pattern:
                        return $"{propertyTitle} does not match the complexity";
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
