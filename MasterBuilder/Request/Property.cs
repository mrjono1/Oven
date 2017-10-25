using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Property
    {
        public string InternalName { get; set; }

        public PropertyTypeEnum Type { get; set; }
        internal bool HasCalculation
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Calculation);
            }
        }
        public string Calculation { get; set; }
        public Guid Id { get; internal set; }

        /// <summary>
        /// C# Data Type
        /// </summary>
        internal string CsType
        {
            get
            {
                switch (Type)
                {
                    case PropertyTypeEnum.Uniqueidentifier:
                        return "Guid";
                    case PropertyTypeEnum.String:
                        return "string";
                    case PropertyTypeEnum.Integer:
                        return "int";
                    case PropertyTypeEnum.DateTime:
                        return "DateTime";
                    default:
                        return "string";
                }
            }
        }
    }
}
