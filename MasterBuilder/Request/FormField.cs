using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Form Field
    /// </summary>
    public class FormField
    {
        /// <summary>
        /// Entity Property Id
        /// </summary>
        public Guid EntityPropertyId { get; set; }
        /// <summary>
        /// Optional: Title, Defaulted from Entity if not provided
        /// </summary>
        public string Title { get; set; }

        #region Internal Helper Properties
        /// <summary>
        /// Entity Property
        /// </summary>
        internal Property Property { get; set; }
        /// <summary>
        /// Use Title specifed or if blank used Property Title
        /// </summary>
        internal string TitleValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Title))
                {
                    return Property.Title;
                }
                else
                {
                    return Title;
                }
            }
        }
        /// <summary>
        /// Property Type
        /// </summary>
        internal PropertyTypeEnum PropertyType
        {
            get
            {
                return Property.PropertyType;
            }
        }
        /// <summary>
        /// Internal Name C#
        /// </summary>
        internal string InternalNameCSharp
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyTypeEnum.ParentRelationship:
                    case PropertyTypeEnum.ReferenceRelationship:
                        return $"{Property.InternalName}Id";
                    default:
                        return Property.InternalName;
                }
            }
        }
        /// <summary>
        /// Internal Name Alternate
        /// </summary>
        internal string InternalNameAlternateCSharp
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyTypeEnum.ReferenceRelationship:
                        return $"{Property.InternalName}Title";
                    default:
                        return null;
                }
            }
        }
        /// <summary>
        /// Type C#
        /// </summary>
        internal string TypeCSharp
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyTypeEnum.PrimaryKey:
                        return $"{Property.CsType}?";
                    default:
                        return Property.CsType;
                }
            }
        }
        #endregion
    }
}
