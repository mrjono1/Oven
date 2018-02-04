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
        private readonly IEnumerable<ScreenSection> ChildScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelFormResponseTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections, IEnumerable<ScreenSection> childScreenSections)
        {
            Project = project;
            Screen = screen;
            ScreenSections = screenSections;
            ChildScreenSections = childScreenSections;
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
            
            foreach (var formField in (from screenSection in ScreenSections
                                       from ff in screenSection.FormSection.FormFields
                                       select ff))
            {
                properties.AddRange(ModelFormResponsePropertyPartial.Evaluate(formField));
            }

            // TODO: Validation
            foreach (var childProperty in (from child in ChildScreenSections
                        select child.ParentEntityProperty).Distinct())
            {
                properties.Add($@"        /// <summary>
        /// {childProperty.Title}
        /// </summary>
        [Display(Name = ""{childProperty.Title}"")]
        public {childProperty.InternalName}{Screen.FormResponseClass} {childProperty.InternalName} {{ get; set; }}");
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
