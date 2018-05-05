using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Form Field
    /// </summary>
    public partial class FormField
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
        /// Visibility Expression - FormFields are Visible by default, this allows it to not be visible based on an expression
        /// </summary>
        public Expression VisibilityExpression { get; set; }
        /// <summary>
        /// If true this property always hidden from the UI, also could be used in an expression
        /// </summary>
        public bool IsHiddenFromUi { get; set; }

        #region Internal Helper Properties
        /// <summary>
        /// Project
        /// </summary>
        [JsonIgnore]
        public Project Project { get; set; }
        /// <summary>
        /// Entity Property
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
                    case PropertyType.ReferenceRelationship:
                        return $"{Property.InternalName}Id";
                    default:
                        return Property.InternalName;
                }
            }
        }
        /// <summary>
        /// Internal Name Alternate
        /// </summary>
        [JsonIgnore]
        public string InternalNameAlternateCSharp
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyType.ReferenceRelationship:
                        return $"{Property.InternalName}Title";
                    default:
                        return null;
                }
            }
        }
        /// <summary>
        /// Internal Name Type Script
        /// </summary>
        [JsonIgnore]
        public string InternalNameTypeScript
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyType.ParentRelationshipOneToMany:
                    case PropertyType.ReferenceRelationship:
                        return $"{Property.InternalName}Id".Camelize();
                    default:
                        return Property.InternalName.Camelize();
                }
            }
        }
        /// <summary>
        /// Internal Name Alternate
        /// </summary>
        [JsonIgnore]
        public string InternalNameAlternateTypeScript
        {
            get
            {
                switch (Property.PropertyType)
                {
                    case PropertyType.ReferenceRelationship:
                        return $"{Property.InternalName}Title".Camelize();
                    default:
                        return null;
                }
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
                    case PropertyType.PrimaryKey:
                        return $"{Property.CsType}?";
                    case PropertyType.ParentRelationshipOneToOne:
                        var referenceEntityName = (from entity in Project.Entities where entity.Id == Property.ParentEntityId.Value select entity.InternalName).Single();
                        return $"{referenceEntityName}";
                    default:
                        return Property.CsType;
                }
            }
        }
        /// <summary>
        /// Type TypeScript
        /// </summary>
        [JsonIgnore]
        public string TypeTypeScript
        {
            get
            {
                return Property.TsType;
            }
        }
        /// <summary>
        /// Reference Request Class
        /// </summary>
        [JsonIgnore]
        public string ReferenceRequestClass
        {
            get
            {
                return $"{Property.InternalName}ReferenceRequest";
            }
        }
        /// <summary>
        /// Reference Item Class
        /// </summary>
        [JsonIgnore]
        public string ReferenceItemClass
        {
            get
            {
                return $"{Property.InternalName}ReferenceItem";
            }
        }
        /// <summary>
        /// Reference Response Class
        /// </summary>
        [JsonIgnore]
        public string ReferenceResponseClass
        {
            get
            {
                return $"{Property.InternalName}ReferenceResponse";
            }
        }
        #endregion
    }
}
