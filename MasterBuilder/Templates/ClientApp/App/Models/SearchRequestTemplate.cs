using Humanizer;
using MasterBuilder.Helpers;
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
            return $"{ScreenSection.InternalName}Request.ts";
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
            var entity = Project.Entities.SingleOrDefault(p => p.Id == ScreenSection.EntityId);
            
            string parentPropertyFilterString = null;
            Entity parentEntity = null;
            if (entity != null)
            {
                var parentProperty = (from p in entity.Properties
                                      where p.Type == PropertyTypeEnum.ParentRelationship
                                      select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    parentEntity = (from s in Project.Entities
                                    where s.Id == parentProperty.ParentEntityId
                                    select s).SingleOrDefault();
                    parentPropertyFilterString = $"{parentEntity.InternalName.Camelize()}Id: string;";
                }
            }

            return $@"export class {ScreenSection.InternalName}Request {{
    page: number;
    pageSize: number;
    {parentPropertyFilterString}
}}";
        }
    }
}
