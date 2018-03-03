using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MasterBuilder.Request
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
        internal static readonly Dictionary<Guid, ScreenSectionType> ScreenSectionTypeDictonary = new Dictionary<Guid, ScreenSectionType>
        {
            { new Guid("{0637300C-B76E-45E2-926A-055BB335129F}"), ScreenSectionType.Search },
            { new Guid("{DC1169A8-8F49-45E9-9969-B64BEF4D0F42}"), ScreenSectionType.Form },
            { new Guid("{4270A420-64CB-4A2C-B718-2C645DB2B57B}"), ScreenSectionType.MenuList },
            { new Guid("{38EF9B44-A993-479B-91EC-1FE436E91556}"), ScreenSectionType.Html }
        };
        /// <summary>
        /// Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public Guid Id { get; set; }
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
        public Guid ScreenSectionTypeId { get; set; }
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
        public Guid? EntityId { get; set; }
        /// <summary>
        /// Optional: Screen to navigate to on actoin
        /// </summary>
        [NonDefault]
        public Guid? NavigateToScreenId { get; set; }
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
        /// Parent Property Id (used for nested sections)
        /// </summary>
        [NonDefault]
        public Guid? ParentEntityPropertyId { get; set; }
        /// <summary>
        /// Only populate when Screen Section Type = Search
        /// </summary>
        public SearchSection SearchSection { get; set; }
        /// <summary>
        /// Only populate when Screen Section Type = Form
        /// </summary>
        public FormSection FormSection { get; set; }

        /// <summary>
        /// The property <see cref="Property.InternalName"/> to order by, defaults to "Title"
        /// </summary>
        public string OrderBy { get; set; } = "Title";
        /// <summary>
        /// Visibility Expression
        /// </summary>
        public Expression VisibilityExpression { get; set; }
        #region Internal Fields

        /// <summary>
        /// Entity
        /// </summary>
        internal Entity Entity { get; set; }
        /// <summary>
        /// Parent Entity Property, for nested sections
        /// </summary>
        internal Property ParentEntityProperty { get; set; }
        #endregion
    }
}
