using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Screen
    /// </summary>
    public class Screen
    {
        /// <summary>
        /// Register of all Screen Type Ids to Enum for easy use
        /// </summary>
        internal static readonly Dictionary<Guid, ScreenTypeEnum> ScreenTypeDictonary = new Dictionary<Guid, ScreenTypeEnum>
        {
            { new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"), ScreenTypeEnum.Search },
            { new Guid("{9B422DE1-FACE-4A63-9A46-0BD1AF3D47F4}"), ScreenTypeEnum.Edit },
            { new Guid("{ACE5A965-7005-4E34-9C66-AF0F0CD15AE9}"), ScreenTypeEnum.View },
            { new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), ScreenTypeEnum.Html }
        };

        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Internal Name
        /// </summary>
        public string InternalName { get; set; }

        // TODO: remove controller code as its to a setting
        /// <summary>
        /// Controller Code
        /// </summary>
        public string ControllerCode { get; set; }
        public string MetaDescription { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Entity Id for entity related screens
        /// </summary>
        public Guid? EntityId { get; set; }
        /// <summary>
        /// Screen Type Id
        /// </summary>
        public Guid ScreenTypeId { get; set; }
        /// <summary>
        /// Path segment
        /// </summary>
        public string Path { get; set; }
        // TODO: possibly get rid of this
        /// <summary>
        /// Custom Html
        /// </summary>
        public string Html { get; set; }
        // TODO: possibly get rid of this
        /// <summary>
        /// Custom Type Script
        /// </summary>
        public string TypeScript { get; set; }
        /// <summary>
        /// Screen Template Id, using a template means the screen will get updated when the template does
        /// </summary>
        public Guid? TemplateId { get; set; }
        // TODO: possibly get rid of this
        /// <summary>
        /// Custom Css
        /// </summary>
        public string Css { get; set; }
        /// <summary>
        /// Screen Features
        /// </summary>
        public IEnumerable<ScreenFeature> ScreenFeatures { get; set; }
        // TODO: possibly get rid of this have it section level only
        /// <summary>
        /// On Screens like search navigate to this screen on an action
        /// </summary>
        public Guid? NavigateToScreenId { get; set; }
        /// <summary>
        /// Screen Sections
        /// </summary>
        public ScreenSection[] ScreenSections { get; set; }
        // TODO: possibly get rid of this have it section level only
        /// <summary>
        /// Screen Type
        /// </summary>
        internal ScreenTypeEnum ScreenType
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
        /// Validate the screen and fix a few values
        /// </summary>
        internal bool Validate(out string errors)
        {
            if (ScreenSections == null || !ScreenSections.Any())
            {
                var screenSectionType = ScreenSectionTypeEnum.Form;
                switch (ScreenType)
                {
                    case ScreenTypeEnum.Search:
                        screenSectionType = ScreenSectionTypeEnum.Search;
                        break;
                    case ScreenTypeEnum.Edit:
                        screenSectionType = ScreenSectionTypeEnum.Form;
                        break;
                    case ScreenTypeEnum.View:
                        screenSectionType = ScreenSectionTypeEnum.Form;
                        break;
                    case ScreenTypeEnum.Html:
                        screenSectionType = ScreenSectionTypeEnum.Html;
                        break;
                    default:
                        break;
                }
                ScreenSections = new ScreenSection[]
                {
                    new ScreenSection
                    {
                        Id = Id,
                        Title = Title,
                        EntityId = EntityId,
                        InternalName = InternalName,
                        ScreenSectionType = screenSectionType,
                        NavigateToScreenId = NavigateToScreenId
                    }
                };
            }
            else
            {
                foreach (var screenSection in ScreenSections)
                {
                    if (!screenSection.EntityId.HasValue)
                    {
                        screenSection.EntityId = EntityId;
                    }
                }
            }
            
            errors = "";
            return true;
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

            if (ScreenType != ScreenTypeEnum.Edit)
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

            return $"{ancestorsString}{Path}/:{entity.InternalName.ToCamlCase()}Id";
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
                                      where p.Type == PropertyTypeEnum.ParentRelationship
                                      select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    foundParentScreen = (from s in project.Screens
                                         where s.EntityId == parentProperty.ParentEntityId &&
                                         s.ScreenType == ScreenTypeEnum.Edit &&
                                         s.Id != screen.Id
                                         select s).SingleOrDefault();

                    var parentEntity = project.Entities.SingleOrDefault(e => e.Id == foundParentScreen.EntityId);

                    anc.AddRange(GetAncestors(project, foundParentScreen));
                    anc.Add(new Tuple<string, string>(foundParentScreen.Path, parentEntity.InternalName.ToCamlCase()));
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
    }
}
