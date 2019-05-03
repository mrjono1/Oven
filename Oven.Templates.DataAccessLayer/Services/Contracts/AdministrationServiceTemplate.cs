using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.DataAccessLayer.Services.Contracts
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
        public string GetFileName() => $"IAdministrationService.cs";

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath() => new string[] { "Services", "Contracts" };
        
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

namespace {Project.InternalName}.DataAccessLayer.Services.Contracts
{{
    /// <summary>
    /// Administration Service Interface
    /// </summary>
    public interface IAdministrationService
    {{
        /// <summary>
        /// Seed
        /// </summary>
        Task Seed();
    }}
}}";
        }
    }
}
