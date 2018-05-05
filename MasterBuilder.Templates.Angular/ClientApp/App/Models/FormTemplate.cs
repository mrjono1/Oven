using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Models
{
    /// <summary>
    /// Form Template
    /// </summary>
    public class FormTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly IEnumerable<ScreenSection> ScreenSections;
        private readonly IEnumerable<ScreenSection> ChildScreenSections;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormTemplate(Project project, Screen screen, IEnumerable<ScreenSection> screenSections, IEnumerable<ScreenSection> childScreenSections)
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
            return $"{ScreenSections.First().Entity.InternalName}.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "models", $"{Screen.InternalName.ToLowerInvariant()}" };
        }

        /// <summary>
        /// Get Properties
        /// </summary>
        /// <param name="formField">The form field</param>
        /// <returns>List of properties for this form field</returns>
        private IEnumerable<string> GetProperties(FormField formField)
        {
            var properties = new List<string>();
            switch (formField.PropertyType)
            {
                case PropertyType.ReferenceRelationship:
                    properties.Add($"    {formField.InternalNameTypeScript}: {formField.TypeTypeScript};");
                    properties.Add($"    {formField.InternalNameAlternateTypeScript}: string;");
                    break;
                default:
                    properties.Add($"    {formField.InternalNameTypeScript}: {formField.TypeTypeScript};");
                    break;
            }

            return properties;
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var properties = new List<string>();
            var imports = new List<string>();

            foreach (var group in (from screenSection in ScreenSections
                                       from ff in screenSection.FormSection.FormFields
                                       select ff).GroupBy(ff => ff.EntityPropertyId))
            {
                properties.AddRange(GetProperties(group.FirstOrDefault()));
            }

            if (ChildScreenSections != null)
            {
                foreach (var childScreenSection in ChildScreenSections)
                {
                    properties.Add($@"    {childScreenSection.Entity.InternalName.Camelize()}: {childScreenSection.Entity.InternalName};");
                    imports.Add($@"import {{ {childScreenSection.Entity.InternalName} }} from './{childScreenSection.Entity.InternalName}';");
                }
            }

            if (imports.Any())
            {
                imports.Add(Environment.NewLine);
            }

            return $@"{string.Join(Environment.NewLine, imports)}export class {ScreenSections.First().Entity.InternalName} {{
{string.Join(Environment.NewLine, properties)}
}}";
        }
    }
}
