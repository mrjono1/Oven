using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.Api.Custom.ProjectFiles
{
    /// <summary>
    /// Project
    /// </summary>
    public class ExtensionPointTemplate : ITemplate
    {
        private readonly Project Project;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ExtensionPointTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"ExtensionPoint.cs";
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
            return $@"using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen.Api.Custom
{{
    /// <summary>
    /// Register custom code extension point
    /// </summary>
    public class ExtensionPoint
    {{
        /// <summary>
        /// Startup startup services
        /// </summary>
        public Dictionary<Type, Type> GetServices()
        {{
            return new Dictionary<Type, Type>();
        }}
    }}
}}";
        }
    }
}
