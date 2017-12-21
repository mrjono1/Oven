using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Search Item Template
    /// </summary>
    public class ModelSearchItemTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelSearchItemTemplate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
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
                return $"{Screen.InternalName}{ScreenSection.InternalName}Item.cs";
            }
            else
            {
                return $"{Screen.InternalName}Item.cs";
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
            StringBuilder properties = null;
            if (Entity.Properties != null)
            {
                properties = new StringBuilder();
                foreach (var item in Entity.Properties)
                {
                    properties.AppendLine(ModelPropertyTemplate.Evaluate(item));
                }
            }

            var className = $"{Screen.InternalName}Item";
            if (Screen.EntityId.HasValue && ScreenSection.EntityId.HasValue && Screen.EntityId != ScreenSection.EntityId)
            {
                className = $"{Screen.InternalName}{ScreenSection.InternalName}Item";
            }
            return $@"using System;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Search Item
    /// </summary>
    public class {className}
    {{
{properties}
    }}
}}";
        }


    }
}
