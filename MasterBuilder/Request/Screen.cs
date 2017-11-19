using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    public class Screen
    {
        public string InternalName { get; set; }
        public string ControllerCode { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }
        public Guid? EntityId { get; set; }
        public Guid ScreenTypeId { get; set; }
        public string Path { get; set; }
        public string Html { get; set; }
        public string TypeScript { get; set; }
        public Guid? TemplateId { get; set; }
        public string Css { get; set; }
        public IEnumerable<ScreenFeature> ScreenFeatures { get; set; }
        public Guid? NavigateToScreenId { get; set; }
        public ScreenSection[] ScreenSections { get; set; }

        internal ScreenTypeEnum ScreenType
        {
            get
            {
                var screenTypes = new Dictionary<Guid, ScreenTypeEnum>
                {
                    { new Guid("{03CD1D4E-CA2B-4466-8016-D96C2DABEB0D}"), ScreenTypeEnum.Search },
                    { new Guid("{9B422DE1-FACE-4A63-9A46-0BD1AF3D47F4}"), ScreenTypeEnum.Edit },
                    { new Guid("{ACE5A965-7005-4E34-9C66-AF0F0CD15AE9}"), ScreenTypeEnum.View },
                    { new Guid("{7A37305E-C518-4A16-91AE-BCF2AE032A9C}"), ScreenTypeEnum.Html }
                };
                
                return screenTypes.GetValueOrDefault(ScreenTypeId, ScreenTypeEnum.Search);
            }
        }

        internal bool Validate(out string errors)
        {
            if (ScreenSections == null || !ScreenSections.Any())
            {
                ScreenSections = new ScreenSection[]
                {
                    new ScreenSection
                    {
                        Id = Id,
                        Title = Title,
                        EntityId = EntityId,
                        InternalName = InternalName,
                        ScreenSectionTypeId = ScreenTypeId, // todo fix this
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
