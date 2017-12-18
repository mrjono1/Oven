using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;

namespace MasterBuilder.Templates.ProjectFiles
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
            return $@"using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace {Project.InternalName}
{{
    public class Startup
    {{
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

        public Startup(IHostingEnvironment env)
        {{
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(""appsettings.json"", optional: true, reloadOnChange: true)
                .AddJsonFile($""appsettings.{{env.EnvironmentName}}.json"", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }}

        public IConfigurationRoot Configuration {{ get; }}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {{
            // Add framework services.
            services.AddMvc();
            services.AddNodeServices();

            // Add Entity Framework service
            services.AddDbContext<Entities.{Project.InternalName}Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(""DefaultConnection"")));

            // Add Swagger service
            services.AddSwaggerGen(c =>
            {{
                c.SwaggerDoc(""v1"", new Info {{ Title = ""{Project.Title} API"", Version = ""v1"" }});
            }});
        }}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, Entities.{Project.InternalName}Context context)
        {{
            loggerFactory.AddConsole(Configuration.GetSection(""Logging""));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            // Initialize database
            context.Initialize();

            if (env.IsDevelopment())
            {{
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {{
                    HotModuleReplacement = true,
                    HotModuleReplacementEndpoint = ""/dist/""
                }});
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
            else
            {{
                app.UseMvc(routes =>
                {{
                    routes.MapRoute(
                     name: ""default"",
                     template: ""{{controller=Home}}/{{action=Index}}/{{id?}}"");

                    routes.MapRoute(
                     ""Sitemap"",
                     ""sitemap.xml"",
                     new {{ controller = ""Home"", action = ""SitemapXml"" }});

                    routes.MapSpaFallbackRoute(
                      name: ""spa-fallback"",
                      defaults: new {{ controller = ""Home"", action = ""Index"" }});

                }});
                app.UseExceptionHandler(""/Home/Error"");
            }}
        }}
    }}
}}";
        }
    }
}
