using Oven.Interfaces;
using Oven.Request;
using System.Linq;

namespace Oven.Templates.Api.Models
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
                                where s.Id == parentProperty.ReferenceEntityId
                                select s).SingleOrDefault();
                parentPropertyString = $@"        /// <summary>
        /// {parentEntity.Title} Id
        /// </summary>
        [Required]
        [NonDefault]
        public Guid {parentEntity.InternalName}Id {{ get; set; }}";
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
        public int start {{ get; set; }}
        public int end {{ get; set; }}
        public string order {{ get; set; }}
        public string sort {{ get; set; }}
        {parentPropertyString}
        // TODO: Search fields
    }}
}}";
        }


    }
}
