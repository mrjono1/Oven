using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.DataAccessLayer.Models.Reference
{
    /// <summary>
    /// Model Reference Response Template
    /// </summary>
    public class ModelReferenceResponseTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly FormField FormField;

        /// <summary>
        /// Constructor
        /// </summary>
        public ModelReferenceResponseTemplate(Project project, Screen screen, FormField formField)
        {
            Project = project;
            Screen = screen;
            FormField = formField;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{FormField.ReferenceResponseClass}.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Models", Screen.InternalName, "Reference" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"using System;

namespace {Project.InternalName}.DataAccessLayer.Models.{Screen.InternalName}.Reference
{{
    /// <summary>
    /// {FormField.TitleValue} Reference Response
    /// </summary>
    public class {FormField.ReferenceResponseClass}
    {{
        /// <summary>
        /// Total Pages
        /// </summary>
        public int TotalPages {{ get; set; }}
        /// <summary>
        /// Total Items
        /// </summary>
        public int TotalItems {{ get; set; }}
        /// <summary>
        /// {FormField.ReferenceItemClass} Array
        /// </summary>
        public {Screen.InternalName}.Reference.{FormField.ReferenceItemClass}[] Items {{ get; set; }}
    }}
}}";
        }
    }
}
