using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.Api.Extensions
{
    /// <summary>
    /// Http Request Extensions Template
    /// </summary>
    public class HttpRequestExtensionsTemplate: ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public HttpRequestExtensionsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "HttpRequestExtensions.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "Extensions" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"using {Project.InternalName}.CoreModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.SpaServices.Prerendering;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace {Project.InternalName}.Extensions
{{
    /// <summary>
    /// Http Request Extensions
    /// </summary>
    public static class HttpRequestExtensions
    {{
        /// <summary>
        /// AbstractRequestInfo
        /// </summary>
        public static IRequest AbstractRequestInfo(this HttpRequest request)
        {{
            var requestSimplified = new IRequest
            {{
                Cookies = request.Cookies,
                Headers = request.Headers,
                Host = request.Host
            }};

            return requestSimplified;
        }}

        /// <summary>
        /// Build Prerender
        /// </summary>
        public static async Task<RenderToStringResult> BuildPrerender(this HttpRequest Request)
        {{
            var nodeServices = Request.HttpContext.RequestServices.GetRequiredService<INodeServices>();
            var hostEnv = Request.HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>();

            var applicationBasePath = hostEnv.ContentRootPath;
            var requestFeature = Request.HttpContext.Features.Get<IHttpRequestFeature>();
            var unencodedPathAndQuery = requestFeature.RawTarget;
            var unencodedAbsoluteUrl = $""{{Request.Scheme}}://{{Request.Host}}{{unencodedPathAndQuery}}"";

            // ** TransferData concept **
            // Here we can pass any Custom Data we want !

            // By default we're passing down Cookies, Headers, Host from the Request object here
            var transferData = new TransferData
            {{
                Request = Request.AbstractRequestInfo(),
                ThisCameFromDotNET = ""Hi Angular it's asp.net :)""
            }};
            // Add more customData here, add it to the TransferData class

            //Prerender now needs CancellationToken
            System.Threading.CancellationTokenSource cancelSource = new System.Threading.CancellationTokenSource();
            System.Threading.CancellationToken cancelToken = cancelSource.Token;

            // Prerender / Serialize application (with Universal)
            return await Prerenderer.RenderToString(
                      ""/"",
                      nodeServices,
                      cancelToken,
                      new JavaScriptModuleExport(applicationBasePath + ""/ClientApp/dist/main-server""),
                      unencodedAbsoluteUrl,
                      unencodedPathAndQuery,
                      transferData, // Our simplified Request object & any other CustommData you want to send!
                      30000,
                      Request.PathBase.ToString()
                  );
        }}
    }}
}}";
        }

    }
}
