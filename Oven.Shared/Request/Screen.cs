using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Oven.Request
{
    /// <summary>
    /// Screen
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Screen: {Title}")]
    public partial class Screen
    {
        /// <summary>
        /// Register of all Screen Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, ScreenType> ScreenTypeDictonary = new Dictionary<Guid, ScreenType>
        {
            { Guid.Empty, ScreenType.None},
            { new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"), ScreenType.Search },
            { new Guid("{9B422DE1-FACE-4A63-9A46-0BD1AF3D47F4}"), ScreenType.Form },
            { new Guid("{ACE5A965-7005-4E34-9C66-AF0F0CD15AE9}"), ScreenType.View },
            { new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), ScreenType.Html }
        };

        /// <summary>
        /// Register of all Screen Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, ScreenTemplate> ScreenTemplateDictonary = new Dictionary<Guid, ScreenTemplate>
        {
            { Guid.Empty, ScreenTemplate.None},
            { new Guid("{142B82E8-471B-47E5-A13F-158D2B06874B}"), ScreenTemplate.Reference },
            { new Guid("{79FEFA81-D6F7-4168-BCAF-FE6494DC3D72}"), ScreenTemplate.Home }
        };

        /// <summary>
        /// Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public Guid Id { get; set; }
        /// <summary>
        /// SEO Meta description
        /// </summary>
        public string MetaDescription { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Entity Id for entity related screens
        /// </summary>
        [NonDefault]
        public Guid? EntityId { get; set; }
        /// <summary>
        /// Screen Type Id
        /// </summary>
        [Required]
        [NonDefault]
        public Guid ScreenTypeId { get; set; }
        /// <summary>
        /// Path segment
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Screen Template Id, using a template means the screen will get updated when the template does
        /// </summary>
        public Guid? TemplateId { get; set; }
        /// <summary>
        /// Screen Features
        /// </summary>
        public IEnumerable<ScreenFeature> ScreenFeatures { get; set; }
        /// <summary>
        /// Screen Menu Items
        /// </summary>
        public IEnumerable<MenuItem> MenuItems { get; set; }
        /// <summary>
        /// Screen Sections
        /// </summary>
        public ScreenSection[] ScreenSections { get; set; }
        /// <summary>
        /// Define child properties and objects by defining a json payload
        /// </summary>
        public string DefaultObjectJsonData { get; set; }
        #region Internal Helpers
        private string internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        [JsonIgnore]
        public string InternalName
        {
            get
            {
                if (internalName == null)
                {
                    internalName = Title.Dehumanize();
                }
                return internalName;
            }
        }
        // TODO: possibly get rid of this have it section level only
        /// <summary>
        /// Screen Type
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public ScreenType ScreenType
        {
            get
            {
                return ScreenTypeDictonary[ScreenTypeId];
            }
            set
            {
                ScreenTypeId = ScreenTypeDictonary.SingleOrDefault(v => v.Value == value).Key;
            }
        }
        /// <summary>
        /// Screen Template, using a template means the screen will get updated when the template does
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public ScreenTemplate Template
        {
            get
            {
                var id = TemplateId ?? Guid.Empty;
                return ScreenTemplateDictonary[id];
            }
            set
            {
                var id = ScreenTemplateDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == Guid.Empty)
                {
                    TemplateId = null;
                }
                else
                {
                    TemplateId = id;
                }
            }
        }
        /// <summary>
        /// Entity
        /// </summary>
        [JsonIgnore]
        public Entity Entity { get; set; }
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
        #endregion
    }
}
