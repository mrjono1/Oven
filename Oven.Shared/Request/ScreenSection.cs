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
    /// Screen Section
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ScreenSection: {Title}")]
    public partial class ScreenSection
    {
        /// <summary>
        /// Register of all Screen Section Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, ScreenSectionType> ScreenSectionTypeDictonary = new Dictionary<ObjectId, ScreenSectionType>
        {
            { new ObjectId("5ca877044a73264e4c06dfc4"), ScreenSectionType.Search },
            { new ObjectId("5ca876fe4a73264e4c06df7c"), ScreenSectionType.Form },
            { new ObjectId("5ca877094a73264e4c06dffb"), ScreenSectionType.MenuList },
            { new ObjectId("5ca877054a73264e4c06dfcf"), ScreenSectionType.Html }
        };
        /// <summary>
        /// Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Internal Name
        /// </summary>
        [Required]
        public string InternalName { get; set; }
        /// <summary>
        /// Screen Section Type Id
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId ScreenSectionTypeId { get; set; }
        /// <summary>
        /// Screen Section Type
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public ScreenSectionType ScreenSectionType
        {
            get
            {
                return ScreenSectionTypeDictonary[ScreenSectionTypeId];
            }
            set
            {
                ScreenSectionTypeId = ScreenSectionTypeDictonary.SingleOrDefault(v => v.Value == value).Key;
            }
        }
        /// <summary>
        /// Optional: Entity to display in this section
        /// </summary>
        [NonDefault]
        public ObjectId? EntityId { get; set; }
        /// <summary>
        /// Optional: Screen to navigate to on actoin
        /// </summary>
        [NonDefault]
        public ObjectId? NavigateToScreenId { get; set; }
        /// <summary>
        /// Screen Menu Items
        /// </summary>
        public IEnumerable<MenuItem> MenuItems { get; set; }
        /// <summary>
        /// Html Content
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// Required only for Menu List Type
        /// </summary>
        public IEnumerable<MenuItem> MenuListMenuItems { get; set; }
        /// <summary>
        /// Only populate when Screen Section Type = Search
        /// </summary>
        public SearchSection SearchSection { get; set; }
        /// <summary>
        /// Only populate when Screen Section Type = Form
        /// </summary>
        public FormSection FormSection { get; set; }
        // TODO: Change this to ObjectId OrderBySearchColumnId, defaulting to a column will be annoying
        /// <summary>
        /// The property <see cref="Property.InternalName"/> to order by, defaults to "Title"
        /// </summary>
        public string OrderBy { get; set; } = "Title";
        /// <summary>
        /// Visibility Expression
        /// </summary>
        public Expression VisibilityExpression { get; set; }
        /// <summary>
        /// Parent Screen Section Id
        /// </summary>
        public ObjectId? ParentScreenSectionId { get; set; }
        #region Internal Fields
        /// <summary>
        /// Form Response Class
        /// </summary>
        [JsonIgnore]
        public string FormResponseClass
        {
            get
            {
                return $"{Entity.InternalName}Response";
            }
        }
        /// <summary>
        /// Form Request Class
        /// </summary>
        [JsonIgnore]
        public string FormRequestClass
        {
            get
            {
                return $"{Entity.InternalName}Request";
            }
        }
        /// <summary>
        /// Entity
        /// </summary>
        [JsonIgnore]
        public Entity Entity { get; set; }
        /// <summary>
        /// Parent Screen Section, for nested sections
        /// </summary>
        [JsonIgnore]
        public ScreenSection ParentScreenSection { get; set; }
        #endregion
    }
}
