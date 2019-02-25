using Humanizer;
using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oven.Templates.DataAccessLayer.ProjectFiles
{
    /// <summary>
    /// Settings Template
    /// </summary>
    public class SettingsTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Settings.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[0];
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {

            return $@"namespace {Project.InternalName}.DataAccessLayer
{{
    public class Settings
    {{
        public string ConnectionString {{ get; set; }}
        public string DatabaseName {{ get; set; }}
    }}
}}";
        }
    }
}
