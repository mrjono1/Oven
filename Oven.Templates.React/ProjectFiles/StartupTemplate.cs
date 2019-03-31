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

            var dbConnection = $@"options.UseSqlServer(Configuration.GetConnectionString(""DefaultConnection"")));";
            
            var serviceSection = "";
            var services = new List<string>();
            serviceNames.ForEach(name => services.Add($"{{ typeof(I{name}), typeof({name}) }}"));
            
            serviceSection = $@"            var servicesDictionary = new System.Collections.Generic.Dictionary<Type, Type>
            {{
                {string.Join(string.Concat(",", Environment.NewLine, "                "), services)}
            }};

            var dalServices = new DataAccessLayer.DatabaseContext().GetServices(services, Configuration);
            foreach (var item in dalServices)
            {{
                servicesDictionary.Add(item.Key, item.Value);
            }}

            var extensionPoint = new Api.Custom.ExtensionPoint();
            var serviceExtensions = extensionPoint.GetServices();

            foreach (var service in servicesDictionary)
            {{
                if (serviceExtensions.ContainsKey(service.Key))
                {{
                    var customServiceType = serviceExtensions[service.Key];
                    services.AddTransient(service.Key, customServiceType);
                }}
                else
                {{
                    services.AddTransient(service.Key, service.Value);
                }}
            }}";


            return $@"using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        /// Startup
        /// </summary>
        public Startup(IConfiguration configuration)
        {{
            Configuration = configuration;
        }}

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration {{ get; }}


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {{
            // Add framework services.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {{
                configuration.RootPath = ""ClientApp/build"";
            }});

            /*var xmlfilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, ""{Project.InternalName}.xml"");

            // Add Swagger service
            services.AddSwaggerGen(c =>
            {{
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc(""v1"", new Info {{ Title = ""{Project.Title} API"", Version = ""v1"" }});
                c.IncludeXmlComments(xmlfilePath);
            }});*/

            // Services
{serviceSection}
        }}

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {{
            if (env.IsDevelopment())
            {{
                app.UseDeveloperExceptionPage();
            }}
            else
            {{
                app.UseExceptionHandler(""/Error"");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {{
                routes.MapRoute(
                    name: ""default"",
                    template: ""{{controller}}/{{action=Index}}/{{id?}}"");
            }});

            app.UseSpa(spa =>
            {{
                spa.Options.SourcePath = ""ClientApp"";

                if (env.IsDevelopment())
                {{
                    spa.UseReactDevelopmentServer(npmScript: ""start"");
                }}
            }});

            /*// Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {{
                c.SwaggerEndpoint(""/swagger/v1/swagger.json"", ""{Project.Title} API V1"");
            }});*/

            /*app.MapWhen(x => !x.Request.Path.Value.StartsWith(""/swagger"", StringComparison.OrdinalIgnoreCase), builder =>
            {{
                builder.UseMvc(routes =>
                {{
                    routes.MapSpaFallbackRoute(
                        name: ""spa-fallback"",
                        defaults: new {{ controller = ""Home"", action = ""Index"" }});
                }});
            }});*/
        }}
    }}
}}";
        }
    }
}
