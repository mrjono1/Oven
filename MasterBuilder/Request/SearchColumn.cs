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
        [Required]
        [NonDefault]
        public Guid EntityPropertyId { get; set; }
        /// <summary>
        /// Optional: Title, Defaulted from Entity if not provided
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The column order
        /// </summary>
        public int Ordinal { get; set; }
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
                    case PropertyType.ParentRelationship:
                        return $"{Property.InternalName}Id";
                    case PropertyType.ReferenceRelationship:
                        return $"{Property.InternalName}Title";
                    default:
                        return Property.InternalName;
                }
            }
        }
        /// <summary>
        /// Property Type
        /// </summary>
        internal PropertyType PropertyType
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
                    case PropertyType.ReferenceRelationship:
                        return "string";
                    default:
                        return Property.CsType;
                }
            }
        }
        internal string TypeTs
        {
            get
            {
                return Property.TsType;
            }
        }
        #endregion
    }
}
