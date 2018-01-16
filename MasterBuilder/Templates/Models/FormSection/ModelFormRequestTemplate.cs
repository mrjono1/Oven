using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Form Request Template
    /// </summary>
    public class ModelFormRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelFormRequestTemplate(Project project, Screen screen, ScreenSection screenSection)
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
            return $"{ScreenSection.FormSection.FormRequestClass}.cs";
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
                properties.Add(ModelFormRequestPropertyTemplate.Evaluate(formField));
            }
            

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Insert/Update Model
    /// </summary>
    public class {ScreenSection.FormSection.FormRequestClass}
    {{
{string.Join(Environment.NewLine, properties)}
    }}
}}";
        }


    }
}
