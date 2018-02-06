using MasterBuilder.Interfaces;
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
        private readonly Service Service;
        private readonly WebService WebService;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebServiceServiceTemplate(Project project, Service service, WebService webService)
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
            return $"{Service.InternalName}Service.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Services" };
        }

        /// <summary>
        /// Get Class Name
        /// </summary>
        public string GetClassName()
        {
            return $"{Service.InternalName}Service";
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
                    functions.Add($@"
        /// <summary>
        /// {operation.Title}
        /// </summary>
        public async Task<IRestResponse> {operation.InternalName}Async(Models.Project.Export.Project body)
        {{
            var request = new RestRequest(""{operation.RelativeRoute}"", Method.{operation.Verb.ToString().ToUpperInvariant()})
            {{
                RequestFormat = DataFormat.Json
            }};
            request.AddHeader(""Content-Type"", ""application/json"");
            if (body != null){{
                var jsonString = JsonConvert.SerializeObject(body, new JsonSerializerSettings
                {{
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }});
                request.AddParameter(""application/json"", jsonString, ParameterType.RequestBody);
            }}
            return await _restClient.ExecuteTaskAsync(request);
        }}");
                }
            }

            return $@"using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using {Project.InternalName}.DataAccessLayer;
using {Project.InternalName}.DataAccessLayer.Entities;
using {Project.InternalName}.Services.Contracts;

namespace {Project.InternalName}.Services
{{
    /// <summary>
    /// {Service.InternalName} Service
    /// </summary>
    public class {Service.InternalName}Service : I{Service.InternalName}Service
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
        public {Service.InternalName}Service({Project.InternalName}Context context)
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
