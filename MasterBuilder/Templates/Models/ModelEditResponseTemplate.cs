using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Entity Response Template
    /// </summary>
    public class ModelEditResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelEditResponseTemplate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
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
            return $"{Screen.InternalName}Response.cs";
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

            return $@"using System;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Load
    /// </summary>
    public class {Screen.InternalName}Response
    {{
{properties}
    }}
}}";
        }
    }
}
