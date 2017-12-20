using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Edit Request Template
    /// </summary>
    public class ModelEditRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Entity Entity;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelEditRequestTemplate(Project project, Entity entity, Screen screen, ScreenSection screenSection)
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
            return $"{Screen.InternalName}Request.cs";
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
                    properties.AppendLine(ModelPropertyTemplate.Evaluate(item, true));
                }
            }

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Insert/Update Model
    /// </summary>
    public class {Screen.InternalName}Request
    {{
{properties}
    }}
}}";
        }


    }
}
