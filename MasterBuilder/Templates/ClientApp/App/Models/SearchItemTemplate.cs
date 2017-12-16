using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    public class SearchItemTemplate : ITemplate
    {
        private readonly Project Project;

        private readonly Screen Screen;

        private readonly ScreenSection ScreenSection;

        public SearchItemTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }
        
        public string GetFileName()
        {
            return $"{ScreenSection.InternalName}Item.ts";
        }

        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Screen.InternalName.ToLowerInvariant()}" };
        }

        public string GetFileContent()
        {
            var entity = Project.Entities.SingleOrDefault(p => p.Id == ScreenSection.EntityId);

            var properties = new List<string>();
            Property parentProperty = null;
            foreach (var property in entity.Properties)
            {
                if (property.Type == PropertyTypeEnum.ParentRelationship)
                {
                    parentProperty = property;
                    continue;
                }
                properties.Add($"   {property.InternalName.ToCamlCase()}: {property.TsType};");
            }
            
            return $@"export interface {ScreenSection.InternalName}Item {{
{string.Join(Environment.NewLine, properties)}
}}";
        }
    }
}
