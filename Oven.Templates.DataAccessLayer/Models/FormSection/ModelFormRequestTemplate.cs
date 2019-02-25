using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Models
{
    /// <summary>
    /// Model Form Request Template
    /// </summary>
    public class ModelFormRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;
        private readonly IEnumerable<ScreenSection> ChildScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelFormRequestTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections, IEnumerable<ScreenSection> childScreenSection = null)
        {
            Project = project;
            Screen = screen;
            ScreenSections = screenSections;
            ChildScreenSections = childScreenSection;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{ScreenSections.First().FormRequestClass}.cs";
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
                                    from ff in formSection.FormSection.FormFields
                                    select ff).GroupBy(ff => ff.EntityPropertyId))
            {
                properties.Add(ModelFormRequestPropertyTemplate.Evaluate(group.FirstOrDefault()));
            }

            if (ChildScreenSections != null)
            {
                foreach (var childScreenSection in ChildScreenSections)
                {
                    properties.Add($@"        /// <summary>
        /// {childScreenSection.Entity.Title} Request
        /// </summary>
        [Display(Name = ""{childScreenSection.Entity.Title}"")]
        public {childScreenSection.FormRequestClass} {childScreenSection.Entity.InternalName} {{ get; set; }}");
                }
            }

            return $@"using System;
using MongoDB.Bson;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.DataAccessLayer.Models
{{
    /// <summary>
    /// {Screen.InternalName} Insert/Update Model
    /// </summary>
    public class {ScreenSections.First().FormRequestClass}
    {{
{string.Join(Environment.NewLine, properties.OrderBy(a => a))}
    }}
}}";
        }


    }
}
