using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;

namespace MasterBuilder.Templates.Services.Contracts
{
    /// <summary>
    /// Web Service Service Contract Template
    /// </summary>
    public class WebServiceServiceContractTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Service Service;
        private readonly WebService WebService;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebServiceServiceContractTemplate(Project project, Service service, WebService webService)
        {
            Project = project;
            Service = service;
            WebService = webService;
        }
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"I{Service.InternalName}Service.cs";
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
            var functions = new List<string>();

            if (WebService.Operations != null)
            {
                foreach (var operation in WebService.Operations)
                {
                    functions.Add($@"        /// <summary>
        /// {operation.Title} Async
        /// </summary>
        Task<IRestResponse> {operation.InternalName}Async(Models.Project.Export.Project body);");
                }
            }

            return $@"using System;
using System.Threading.Tasks;
using RestSharp;
using {Project.InternalName}.DataAccessLayer;
using {Project.InternalName}.DataAccessLayer.Entities;

namespace {Project.InternalName}.Services.Contracts
{{
    /// <summary>
    /// {Service.InternalName} Service Contract
    /// </summary>
    public interface I{Service.InternalName}Service
    {{
{string.Join(Environment.NewLine, functions)}
    }}
}}";
        }

    }
}
