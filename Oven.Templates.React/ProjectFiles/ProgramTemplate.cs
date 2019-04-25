using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// Program.cs configuration
    /// </summary>
    public class ProgramTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProgramTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Program.cs";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace {Project.InternalName}
{{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {{
        /// <summary>
        /// Main application entry point
        /// </summary>
        public static void Main(string[] args)
        {{
            CreateWebHostBuilder(args).Build().Run();
        }}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }}
}}";
        }
    }
}