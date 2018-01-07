using Humanizer;
using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    /// <summary>
    /// Search Item Template
    /// </summary>
    public class SearchItemTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchItemTemplate(Project project, Screen screen, ScreenSection screenSection)
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
            return $"{ScreenSection.InternalName}Item.ts";
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

            var properties = new List<string>();
            Property parentProperty = null;
            foreach (var property in entity.Properties)
            {
                switch (property.PropertyType)
                {
                    case PropertyTypeEnum.ParentRelationship:
                        parentProperty = property;
                        continue;
                    case PropertyTypeEnum.ReferenceRelationship:
                        properties.Add($"   {property.InternalName.Camelize()}Title: string;");
                        break;
                    default:
                        properties.Add($"   {property.InternalName.Camelize()}: {property.TsType};");
                        break;
                }
            }
            
            return $@"export interface {ScreenSection.InternalName}Item {{
{string.Join(Environment.NewLine, properties)}
}}";
        }
    }
}
