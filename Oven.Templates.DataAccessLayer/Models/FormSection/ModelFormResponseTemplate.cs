using Oven.Interfaces;
using Oven.Request;
using Oven.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Models
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
        public ModelFormResponseTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections, IEnumerable<ScreenSection> childScreenSection = null)
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
            return $"{ScreenSections.First().FormResponseClass}.cs";
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
                var formField = group.First();
                properties.Add(ModelFormResponsePropertyTemplate.Evaluate(formField));
            }

            if (ChildScreenSections != null)
            {
                foreach (var childScreenSection in ChildScreenSections)
                {
                    properties.Add($@"        /// <summary>
        /// {childScreenSection.Entity.Title} Response
        /// </summary>
        [Display(Name = ""{childScreenSection.Entity.Title}"")]
        public {childScreenSection.FormResponseClass} {childScreenSection.Entity.InternalName} {{ get; set; }}");
                }
            }

            var parentEntities = Screen.Entity.GetParentEntites(Project);
            if (parentEntities.Any())
            {
                foreach (var pe in parentEntities.Skip(1))
                {
                    properties.Add($@"        /// <summary>
        /// {pe.Title}
        /// </summary>
        [Display(Name = ""{pe.Title}"")]
        public string {pe.InternalName}Id {{ get; set; }}");
                    properties.Add($@"        /// <summary>
        /// {pe.Title}
        /// </summary>
        internal ObjectId Object{pe.InternalName}Id
        {{
            get
            {{
                return ObjectId.Parse({pe.InternalName}Id);
            }}
            set
            {{
                {pe.InternalName}Id = value.ToString();
            }}
        }}");
                }
            }

            return $@"using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace {Project.InternalName}.DataAccessLayer.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Load
    /// </summary>
    public class {ScreenSections.First().FormResponseClass}
    {{
{string.Join(Environment.NewLine, properties.OrderBy(a => a))}
    }}
}}";
        }
    }
}
