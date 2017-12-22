using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Services
{
    /// <summary>
    /// Export Service
    /// </summary>
    public class ExportServiceTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project"></param>
        public ExportServiceTemplate(Project project)
        {
            Project = project;
        }
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "ExportService.cs";
        }

        public string[] GetFilePath()
        {
            return new string[] { "Services" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var exportFunctions = new List<string>();
            
            return $@"using System;
using {Project.InternalName}.Entities;

namespace {Project.InternalName}.Services
{{
    /// <summary>
    /// Export Service
    /// </summary>
    public class ExportService
    {{
        /// <summary>
        /// Database Context
        /// </summary>
        private readonly {Project.InternalName}Context _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExportService({Project.InternalName}Context context)
        {{
            _context = context;
        }}

{string.Join(Environment.NewLine, exportFunctions)}
    }}
}}";
        }

    }
}
