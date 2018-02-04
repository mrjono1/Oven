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
    public class ModelChildFormResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly Property Property;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelChildFormResponseTemplate(Project project, Screen screen, Property property, IEnumerable<ScreenSection> screenSections)
        {
            Project = project;
            Screen = screen;
            Property = property;
            ScreenSections = screenSections;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Property.InternalName}{Screen.FormResponseClass}.cs";
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
            
            foreach (var formField in (from screenSection in ScreenSections
                                       from ff in screenSection.FormSection.FormFields
                                       select ff))
            {
                properties.AddRange(ModelFormResponsePropertyPartial.Evaluate(formField));
            }

            return $@"using System;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Load
    /// </summary>
    public class {Property.InternalName}{Screen.FormResponseClass}
    {{
{string.Join(Environment.NewLine, properties)}
    }}
}}";
        }
    }
}
