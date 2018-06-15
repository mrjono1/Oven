using Humanizer;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public Property Property { get; set; }
        /// <summary>
        /// Use Title specifed or if blank used Property Title
        /// </summary>
        [JsonIgnore]
        public string TitleValue
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
        [JsonIgnore]
        public string InternalNameCSharp
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyType.ParentRelationshipOneToMany:
                        return $"{Property.InternalName}Id";
                    case PropertyType.ReferenceRelationship:
                        return $"{Property.InternalName}Title";
                    default:
                        return Property.InternalName;
                }
            }
        }
        /// <summary>
        /// Internal Name javascipt
        /// </summary>
        [JsonIgnore]
        public string InternalNameJavascript
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyType.ParentRelationshipOneToMany:
                        return $"{Property.InternalName.Camelize()}Id";
                    case PropertyType.ReferenceRelationship:
                        return $"{Property.InternalName.Camelize()}Title";
                    default:
                        return Property.InternalName.Camelize();
                }
            }
        }
        /// <summary>
        /// Property Type
        /// </summary>
        [JsonIgnore]
        public PropertyType PropertyType
        {
            get
            {
                return Property.PropertyType;
            }
        }
        /// <summary>
        /// Type C#
        /// </summary>
        [JsonIgnore]
        public string TypeCSharp
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
        [JsonIgnore]
        public string TypeTs
        {
            get
            {
                return Property.TsType;
            }
        }
        #endregion
    }
}
