using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Models
{
    /// <summary>
    /// Model Form Request Template
    /// </summary>
    public class ModelFormRequestTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelFormRequestTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections)
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
            return $"{Screen.FormRequestClass}.cs";
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
                                       where !formSection.ParentEntityPropertyId.HasValue
                                       from ff in formSection.FormSection.FormFields
                                       select ff).GroupBy(ff => ff.EntityPropertyId))
            {
                properties.Add(ModelFormRequestPropertyTemplate.Evaluate(group.FirstOrDefault()));
            }
            

            return $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Insert/Update Model
    /// </summary>
    public class {Screen.FormRequestClass}
    {{
{string.Join(Environment.NewLine, properties)}
    }}
}}";
        }


    }
}
