using System;
using System.ComponentModel.DataAnnotations;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Search Column
    /// </summary>
    public class SearchColumn
    {
        /// <summary>
        /// Entity Property Id
        /// </summary>
        [RequiredNonDefault]
        public Guid EntityPropertyId { get; set; }
        /// <summary>
        /// Optional: Title, Defaulted from Entity if not provided
        /// </summary>
        public string Title { get; set; }
        
        #region Internal Helper Properties
        /// <summary>
        /// Property
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
        /// Internal Name C#
        /// </summary>
        internal string InternalNameCSharp
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyTypeEnum.ParentRelationship:
                        return $"{Property.InternalName}Id";
                    case PropertyTypeEnum.ReferenceRelationship:
                        return $"{Property.InternalName}Title";
                    default:
                        return Property.InternalName;
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
        /// Type C#
        /// </summary>
        internal string TypeCSharp
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyTypeEnum.PrimaryKey:
                        return "Guid";
                    case PropertyTypeEnum.ParentRelationship:
                        return Property.CsType;
                    case PropertyTypeEnum.ReferenceRelationship:
                        return "string";
                    default:
                        return Property.CsType;
                }
            }
        }
        #endregion
    }
}
