using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    public class SearchRequestTemplate : ITemplate
    {
        private readonly Project Project;

        private readonly Screen Screen;

        private readonly ScreenSection ScreenSection;

        public SearchRequestTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }
        
        public string GetFileName()
        {
            return $"{ScreenSection.InternalName}Request.ts";
        }

        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Screen.InternalName.ToLowerInvariant()}" };
        }

        public string GetFileContent()
        {
            var entity = Project.Entities.SingleOrDefault(p => p.Id == ScreenSection.EntityId);
            
            Property parentProperty = null;

            string parentPropertyFilterString = null;
            Entity parentEntity = null;
            if (entity != null)
            {
                if (parentProperty != null)
                {
                    parentEntity = (from s in Project.Entities
                                    where s.Id == parentProperty.ParentEntityId
                                    select s).SingleOrDefault();
                    parentPropertyFilterString = $"{parentEntity.InternalName.ToCamlCase()}Id: string;";
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
