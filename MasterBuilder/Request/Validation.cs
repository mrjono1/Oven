using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Validation
    {
        public Guid Id { get; set; }
        public ValidationTypeEnum ValidationType { get; set; }
        public int? IntegerValue { get; set; }
        public string Message { get; set; }
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
        public string StringValue { get; set; }
    }
}
