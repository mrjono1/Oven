using Humanizer;
using MongoDB.Bson;
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
        internal static readonly Dictionary<ObjectId, ScreenType> ScreenTypeDictonary = new Dictionary<ObjectId, ScreenType>
        {
            { ObjectId.Empty, ScreenType.None},
            { new ObjectId("5ca877054a73264e4c06dfc9"), ScreenType.Search },
            { new ObjectId("5ca877054a73264e4c06dfca"), ScreenType.Form },
            { new ObjectId("5ca8770a4a73264e4c06dffe"), ScreenType.View },
            { new ObjectId("5ca8770a4a73264e4c06dfff"), ScreenType.Html }
        };

        /// <summary>
        /// Register of all Screen Template Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<ObjectId, ScreenTemplate> ScreenTemplateDictonary = new Dictionary<ObjectId, ScreenTemplate>
        {
            { ObjectId.Empty, ScreenTemplate.None},
            { new ObjectId("5ca8770a4a73264e4c06e002"), ScreenTemplate.Reference },
            { new ObjectId("5ca8770a4a73264e4c06e003"), ScreenTemplate.Home }
        };

        /// <summary>
        /// Identifier
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId Id { get; set; }
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
        public ObjectId? EntityId { get; set; }
        /// <summary>
        /// Screen Type Id
        /// </summary>
        [Required]
        [NonDefault]
        public ObjectId ScreenTypeId { get; set; }
        /// <summary>
        /// Path segment
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Screen Template Id, using a template means the screen will get updated when the template does
        /// </summary>
        public ObjectId? TemplateId { get; set; }
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
                var id = TemplateId ?? ObjectId.Empty;
                return ScreenTemplateDictonary[id];
            }
            set
            {
                var id = ScreenTemplateDictonary.SingleOrDefault(v => v.Value == value).Key;
                if (id == ObjectId.Empty)
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
