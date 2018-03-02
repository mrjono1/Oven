using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
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
        private string internalName;
        /// <summary>
        /// Calculated Internal Name
        /// </summary>
        internal string InternalName
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
        /// Screen Features
        /// </summary>
        public IEnumerable<ScreenFeature> ScreenFeatures { get; set; }
        /// <summary>
        /// Screen Menu Items
        /// </summary>
        public IEnumerable<MenuItem> MenuItems { get; set; }
        // TODO: possibly get rid of this have it section level only
        /// <summary>
        /// On Screens like search navigate to this screen on an action
        /// </summary>
        [NonDefault]
        public Guid? NavigateToScreenId { get; set; }
        /// <summary>
        /// Screen Sections
        /// </summary>
        public ScreenSection[] ScreenSections { get; set; }
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
        #region Internal Helpers
        /// <summary>
        /// Entity
        /// </summary>
        internal Entity Entity { get; set; }
        /// <summary>
        /// Form Response Class
        /// </summary>
        internal string FormResponseClass
        {
            get
            {
                return $"{InternalName}Response";
            }
        }
        /// <summary>
        /// Form Request Class
        /// </summary>
        internal string FormRequestClass
        {
            get
            {
                return $"{InternalName}Request";
            }
        }
        /// <summary>
        /// Get the edit path
        /// </summary>
        internal string EditFullPath(Project project)
        {
            if (string.IsNullOrEmpty(Path))
            {
                return null;
            }

            if (ScreenType != ScreenType.Form)
            {
                return null;
            }
            var entity = (from e in project.Entities
                            where e.Id == EntityId
                            select e).SingleOrDefault();
            if (entity == null)
            {
                return null;
            }

            var ancestors = GetAncestors(project, this);
            StringBuilder ancestorsString = new StringBuilder();
            if (ancestors.Any())
            {
                foreach (var item in ancestors)
                {
                    ancestorsString.Append($"{item.Item1}/:{item.Item2}Id/");
                }
            }

            return $"{ancestorsString}{Path}/:{entity.InternalName.Camelize()}Id";
        }

        /// <summary>
        /// Get screen ancestors, for building things like routes
        /// </summary>
        private List<Tuple<string,string>> GetAncestors(Project project, Screen screen)
        {
            var anc = new List<Tuple<string, string>>();
            Screen foundParentScreen = null;

            var entity = (from e in project.Entities
                          where e.Id == screen.EntityId
                          select e).SingleOrDefault();
            if (entity != null)
            {
                var parentProperty = (from p in entity.Properties
                                      where p.PropertyType == PropertyType.ParentRelationship
                                      select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    // Standard Parent Relationship
                    foundParentScreen = (from s in project.Screens
                                         where s.EntityId == parentProperty.ParentEntityId &&
                                         s.ScreenType == ScreenType.Form &&
                                         s.Id != screen.Id
                                         select s).SingleOrDefault();

                    // One to One Relationship
                    if (foundParentScreen == null)
                    {
                        var parentEntity = parentProperty.ParentEntity;

                        foundParentScreen = (from s in project.Screens
                                             where s.ScreenType == ScreenType.Form &&
                                             s.Id != screen.Id
                                             from p in s.Entity.Properties
                                             where p.PropertyType == PropertyType.OneToOneRelationship &&
                                             p.ParentEntityId == parentEntity.Id
                                             select s).SingleOrDefault();
                    }

                    if (foundParentScreen != null)
                    {
                        var parentEntity = project.Entities.SingleOrDefault(e => e.Id == foundParentScreen.EntityId);

                        anc.AddRange(GetAncestors(project, foundParentScreen));
                        anc.Add(new Tuple<string, string>(foundParentScreen.Path, parentEntity.InternalName.Camelize()));
                    }
                }
            }
            return anc;
        }

        /// <summary>
        /// Get the Screen full path
        /// </summary>
        internal string FullPath(Project project)
        {
            if (string.IsNullOrEmpty(Path))
            {
                return null;
            }
            
            var ancestors = GetAncestors(project, this);
            StringBuilder ancestorsString = new StringBuilder();
            if (ancestors.Any())
            {
                foreach (var item in ancestors)
                {
                    ancestorsString.Append($"{item.Item1}/:{item.Item2}Id/");
                }
            }
            return $"{ancestorsString}{Path}";
        }
        #endregion
    }
}
