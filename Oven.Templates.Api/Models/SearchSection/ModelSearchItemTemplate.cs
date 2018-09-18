using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oven.Templates.Api.Models
{
    /// <summary>
    /// Model Search Item Template
    /// </summary>
    public class ModelSearchItemTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelSearchItemTemplate(Project project, Screen screen, ScreenSection screenSection)
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
            return $"{ScreenSection.SearchSection.SearchItemClass}.cs";
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
            
            foreach (var searchColumn in ScreenSection.SearchSection.SearchColumns)
            {
                properties.Add(ModelSearchItemPropertyTemplate.Evaluate(searchColumn));
            }

            var className = $"{Screen.InternalName}Item";
            if (Screen.ScreenType != ScreenType.Search)
            {
                className = $"{Screen.InternalName}{ScreenSection.InternalName}Item";
            }
            return $@"using System;
using System.ComponentModel.DataAnnotations;

namespace {Project.InternalName}.Models
{{
    /// <summary>
    /// {Screen.InternalName} Screen Search Item
    /// </summary>
    public class {className}
    {{
{string.Join(Environment.NewLine, properties)}
    }}
}}";
        }


    }
}
