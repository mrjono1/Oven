using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Form Response Template
    /// </summary>
    public class ModelFormResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelFormResponseTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
        {
            Project = project;
            Screen = screen;
            ScreenSections = screenSections;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Screen.FormResponseClass}.cs";
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
            
            foreach (var group in (from formSection in ScreenSections
                                       where !formSection.ParentScreenSectionId.HasValue
                                       from ff in formSection.FormSection.FormFields
                                       select ff).GroupBy(ff => ff.EntityPropertyId))
            {
                properties.Add(ModelFormResponsePropertyTemplate.Evaluate(group.FirstOrDefault()));
            }

            return $@"using System;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Load
    /// </summary>
    public class {Screen.FormResponseClass}
    {{
{string.Join(Environment.NewLine, properties)}
    }}
}}";
        }
    }
}
