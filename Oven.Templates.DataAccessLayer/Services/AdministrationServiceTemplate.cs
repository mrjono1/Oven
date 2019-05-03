using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Services
{
    /// <summary>
    /// Controller Template
    /// </summary>
    public class AdministrationServiceTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AdministrationServiceTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName() => $"AdministrationService.cs";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "Services" };
        
        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using {Project.InternalName}.DataAccessLayer.Models;
using Kitchen.DataAccessLayer.Services.Contracts;

namespace {Project.InternalName}.DataAccessLayer.Services
{{
    /// <summary>
    /// Administration Service
    /// </summary>
    public class AdministrationService: IAdministrationService
    {{
        /// <summary>
        /// Database Context
        /// </summary>
        protected readonly IApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public AdministrationService(IApplicationDbContext context)
        {{
            _context = context;
        }}

        /// <summary>
        /// Seed
        /// </summary>
        public async Task Seed()
        {{
            await _context.Seed();
        }}
    }}
}}";
        }
    }
}
