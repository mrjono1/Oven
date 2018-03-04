using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    /// <summary>
    /// Search Request Template
    /// </summary>
    public class SearchRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchRequestTemplate(Project project, Screen screen, ScreenSection screenSection)
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
            return $"{ScreenSection.SearchSection.SearchRequestClass}.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Screen.InternalName.ToLowerInvariant()}" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            
            string parentPropertyFilterString = null;
            Entity parentEntity = null;
            var parentProperty = (from p in ScreenSection.SearchSection.Entity.Properties
                                    where p.PropertyType == PropertyType.ParentRelationshipOneToMany
                                    select p).SingleOrDefault();
            if (parentProperty != null)
            {
                parentEntity = (from s in Project.Entities
                                where s.Id == parentProperty.ParentEntityId
                                select s).SingleOrDefault();
                parentPropertyFilterString = $"{parentEntity.InternalName.Camelize()}Id: string;";
            }

            return $@"export class {ScreenSection.SearchSection.SearchRequestClass} {{
    page: number;
    pageSize: number;
    {parentPropertyFilterString}
}}";
        }
    }
}
