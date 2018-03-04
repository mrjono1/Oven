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
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelSearchRequestTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{ScreenSection.SearchSection.SearchRequestClass}.cs";
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
            string parentPropertyString = null;
            Entity parentEntity = null;
            var parentProperty = (from p in ScreenSection.SearchSection.Entity.Properties
                                    where p.PropertyType == PropertyType.ParentRelationshipOneToMany
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
            

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Search Request
    /// </summary>
    public class {ScreenSection.SearchSection.SearchRequestClass}
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
