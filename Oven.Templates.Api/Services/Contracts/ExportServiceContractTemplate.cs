using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;

namespace Oven.Templates.Api.Services.Contracts
{
    /// <summary>
    /// Export Service Contract
    /// </summary>
    public class ExportServiceContractTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="project"></param>
        public ExportServiceContractTemplate(Project project)
        {
            Project = project;
        }
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "IExportService.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Services", "Contracts" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var exportFunctions = new List<string>();

            if (Project.Id == Project.KitchenId)
            {
                exportFunctions.Add($@"        /// <summary>
        /// Export Project Async
        /// </summary>
        Task<Models.Project.Export.Project> ExportProjectAsync(Guid id);");
            }

            return $@"using System;
using System.Threading.Tasks;
using {Project.InternalName}.DataAccessLayer.Entities;

namespace {Project.InternalName}.Services.Contracts
{{
    /// <summary>
    /// Export Service contract
    /// </summary>
    public interface IExportService
    {{
{string.Join(Environment.NewLine, exportFunctions)}
    }}
}}";
        }

    }
}
