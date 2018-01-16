using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Form Response Template
    /// </summary>
    public class ModelFormResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelFormResponseTemplate(Project project, Screen screen, ScreenSection screenSection)
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
            return $"{ScreenSection.FormSection.FormResponseClass}.cs";
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
            var properties = new List<string>();
            
            foreach (var formField in ScreenSection.FormSection.FormFields)
            {
                properties.Add(ModelFormResponsePropertyTemplate.Evaluate(formField));
            }

            return $@"using System;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Load
    /// </summary>
    public class {ScreenSection.FormSection.FormResponseClass}
    {{
{string.Join(Environment.NewLine ,properties)}
    }}
}}";
        }
    }
}
