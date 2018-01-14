using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Linq;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Search Request Template
    /// </summary>
    public class ModelSearchRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelSearchRequestTemplate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Entity = entity;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            if (Screen.EntityId.HasValue && ScreenSection.EntityId.HasValue && Screen.EntityId != ScreenSection.EntityId)
            {
                return $"{Screen.InternalName}{ScreenSection.InternalName}Request.cs";
            }
            else
            {
                return $"{Screen.InternalName}Request.cs";
            }
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Models", Screen.InternalName };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var className = $"{Screen.InternalName}Request";
            if (Screen.EntityId.HasValue && ScreenSection.EntityId.HasValue && Screen.EntityId != ScreenSection.EntityId)
            {
                className = $"{Screen.InternalName}{ScreenSection.InternalName}Request";
            }

            string parentPropertyString = null;
            Entity parentEntity = null;
            var sectionEntity = (from e in Project.Entities
                          where e.Id == ScreenSection.EntityId
                          select e).SingleOrDefault();
            if (sectionEntity != null)
            {
                var parentProperty = (from p in sectionEntity.Properties
                                      where p.PropertyType == PropertyTypeEnum.ParentRelationship
                                      select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    parentEntity = (from s in Project.Entities
                                         where s.Id == parentProperty.ParentEntityId
                                         select s).SingleOrDefault();
                    parentPropertyString = $@"        /// <summary>
        /// {parentEntity.Title} Id
        /// </summary>
        public Guid? {parentEntity.InternalName}Id {{ get; set; }}";
                }
            }

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Search Request
    /// </summary>
    public class {className}
    {{
        /// <summary>
        /// Page
        /// </summary>
        [Required]
        [DefaultValue(1)]
        public int Page {{ get; set; }}
        /// <summary>
        /// Page Size
        /// </summary>
        [Required]
        [DefaultValue(10)]
        public int PageSize {{ get; set; }}
        {parentPropertyString}
        // TODO: Search fields
    }}
}}";
        }


    }
}
