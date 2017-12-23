using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;

namespace MasterBuilder.Templates.Services
{
    /// <summary>
    /// Web Service Service Template
    /// </summary>
    public class WebServiceServiceTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly WebService WebService;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebServiceServiceTemplate(Project project, WebService webService)
        {
            Project = project;
            WebService = webService;
        }
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{WebService.InternalName}Service.cs";
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
            var functions = new List<string>();

            if (WebService.Operations != null)
            {
                foreach (var operation in WebService.Operations)
                {
                    functions.Add($@"        public async Task<IRestResponse> {operation.InternalName}(object body)
        {{
            var request = new RestRequest(""{operation.RelativeRoute}"", Method.{operation.Verb});
            if (body != null){{
                request.AddJsonBody(body);
            }}
            return await _restClient.ExecuteGetTaskAsync(request);
        }}");
                }
            }

            return $@"using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using {Project.InternalName}.Entities;
using {Project.InternalName}.Services.Contracts;

namespace {Project.InternalName}.Services
{{
    /// <summary>
    /// {WebService.InternalName} Service
    /// </summary>
    public class {WebService.InternalName}Service : I{WebService.InternalName}Service
    {{
        /// <summary>
        /// Database Context
        /// </summary>
        private readonly {Project.InternalName}Context _context;
        /// <summary>
        /// Rest Sharp Client
        /// </summary>
        private readonly RestClient _restClient;

        /// <summary>
        /// Constructor
        /// </summary>
        public {WebService.InternalName}Service({Project.InternalName}Context context)
        {{
            _context = context;
            _restClient = new RestClient(""{WebService.DefaultBaseEndpoint}"");
        }}

{string.Join(Environment.NewLine, functions)}
    }}
}}";
        }

    }
}
