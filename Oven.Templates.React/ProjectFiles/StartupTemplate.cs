using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// Startup
    /// </summary>
    public class StartupTemplate :ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public StartupTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Startup.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        /// <returns></returns>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var serviceNames = new List<string>
            {
                "ExportService"
            };

            // TODO: Create application setting service

            if (Project.Services != null)
            {
                foreach (var service in Project.Services)
                {
                    switch (service.ServiceType)
                    {
                        case ServiceType.WebService:
                            serviceNames.Add($"{service.InternalName}Service");
                            break;
                        case ServiceType.ExportService:
                            break;
                        default:
                            break;
                    }
                }
            }

            var services = new List<string>();
            serviceNames.ForEach(name => services.Add($"services.AddTransient<I{name}, {name}>();"));

            string dbConnection = null;
            string dbConnectionSetup = null;
            if (Project.UseMySql)
            {
                dbConnectionSetup = $@"
            var useMySqlInApp = Configuration[""useMySqlInApp""];
            var connectionString = ""Server=localhost;database={Project.InternalName};uid=root;pwd=password;"";
            if (!string.IsNullOrEmpty(useMySqlInApp) && useMySqlInApp.Equals(""true"", StringComparison.OrdinalIgnoreCase))
            {{
                connectionString = Environment.GetEnvironmentVariable(""MYSQLCONNSTR_localdb"").ToString();
            }}";
                dbConnection = $@"options.UseMySql(connectionString));";
            }
            else
            {
                dbConnection = $@"options.UseSqlServer(Configuration.GetConnectionString(""DefaultConnection"")));";
            }

            // Create Entity Services
            foreach (var entity in Project.Entities)
            {
                if (Project.Screens.Any(_ => _.EntityId == entity.Id))
                {
                    services.Add($"services.AddTransient<I{entity.InternalName}Service, {entity.InternalName}Service>();");
                }
            }

            return $@"using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using {Project.InternalName}.Services;
using {Project.InternalName}.Services.Contracts;

namespace {Project.InternalName}
{{
    
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {{
        /// <summary>
        /// Main
        /// </summary>
        public static void Main(string[] args)
        {{
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }}

        /// <summary>
        /// Startup
        /// </summary>
        public Startup(IHostingEnvironment env)
        {{
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(""appsettings.json"", optional: true, reloadOnChange: true)
                .AddJsonFile($""appsettings.{{env.EnvironmentName}}.json"", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }}

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfigurationRoot Configuration {{ get; }}

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {{
            // Add framework services.
            services.AddMvc();
            services.AddNodeServices();

            // Add Entity Framework service{dbConnectionSetup}
            services.AddDbContext<DataAccessLayer.ApplicationDbContext>(options =>
                {dbConnection}

            var xmlfilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, ""{Project.InternalName}.xml"");

            // Add Swagger service
            services.AddSwaggerGen(c =>
            {{
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc(""v1"", new Info {{ Title = ""{Project.Title} API"", Version = ""v1"" }});
                c.IncludeXmlComments(xmlfilePath);
            }});

            // Services
            {string.Join(string.Concat(Environment.NewLine, "            "), services)}
        }}

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DataAccessLayer.ApplicationDbContext context)
        {{
            loggerFactory.AddConsole(Configuration.GetSection(""Logging""));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            // Initialize database
            context.Initialize().Wait();

            if (env.IsDevelopment())
            {{
                app.UseDeveloperExceptionPage();
            }}
            else
            {{
                app.UseExceptionHandler(""/Home/Error"");
            }}

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {{
                c.SwaggerEndpoint(""/swagger/v1/swagger.json"", ""{Project.Title} API V1"");
            }});

            app.MapWhen(x => !x.Request.Path.Value.StartsWith(""/swagger"", StringComparison.OrdinalIgnoreCase), builder =>
            {{
                builder.UseMvc(routes =>
                {{
                    routes.MapSpaFallbackRoute(
                        name: ""spa-fallback"",
                        defaults: new {{ controller = ""Home"", action = ""Index"" }});
                }});
            }});
        }}
    }}
}}";
        }
    }
}
