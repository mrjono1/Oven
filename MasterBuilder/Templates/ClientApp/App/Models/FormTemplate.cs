using Humanizer;
using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Models
{
    /// <summary>
    /// Form Template
    /// </summary>
    public class FormTemplate : ITemplate
    {
        private readonly Project Project;

        private readonly Screen Screen;

        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormTemplate(Project project, Screen screen, ScreenSection screenSection)
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
            return $"{ScreenSection.InternalName}.ts";
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
            foreach (var property in entity.Properties)
            {
                if (property.Type == PropertyTypeEnum.ParentRelationship || property.Type == PropertyTypeEnum.ReferenceRelationship)
                {
                    properties.Add($"   {property.InternalName.Camelize()}Id: {property.TsType};");
                }
                else
                {
                    properties.Add($"   {property.InternalName.Camelize()}: {property.TsType};");
                }
            }

            return $@"export class {ScreenSection.InternalName} {{
{string.Join(Environment.NewLine, properties)}
}}";
        }
    }
}
