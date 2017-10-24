using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ProjectFiles
{
    public class ProgramTemplate
    {
        public static string FileName()
        {
            return "Program.cs";
        }
        public static string Evaluate(Project project)
        {
            return $@"using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace {project.InternalName}
{{
    public class Program
    {{
        public static void Main(string[] args)
        {{
            BuildWebHost(args).Run();
        }}

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }}
}}";
        }
    }
}
