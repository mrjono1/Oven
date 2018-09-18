using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.Api.Services
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

        /// <summary>
        /// Get file path
        /// </summary>
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

            if (Project.Id == Project.KitchenId)
            {
                var projectEntity = Project.Entities.SingleOrDefault(a => a.Id == new Guid("{89920EA4-9099-487A-AEBB-390E401FEC26}"));
                exportFunctions.Add(new ExportFunctionTemplate(Project, projectEntity).Function());
            }

            return $@"using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using {Project.InternalName}.DataAccessLayer;
using {Project.InternalName}.DataAccessLayer.Entities;
using {Project.InternalName}.Services.Contracts;

namespace {Project.InternalName}.Services
{{
    /// <summary>
    /// Export Service
    /// </summary>
    public class ExportService : IExportService
    {{
        /// <summary>
        /// Database Context
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExportService(ApplicationDbContext context)
        {{
            _context = context;
        }}

{string.Join(Environment.NewLine, exportFunctions)}
    }}
}}";
        }

    }
}
