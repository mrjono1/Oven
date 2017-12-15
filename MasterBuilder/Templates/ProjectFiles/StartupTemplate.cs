using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ProjectFiles
{
    public class StartupTemplate
    {
        public static string FileName()
        {
            return "Startup.cs";
        }
        public static string Evaluate(Project project)
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

namespace {project.InternalName}
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
            services.AddDbContext<Entities.{project.InternalName}Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(""DefaultConnection"")));

            // Add Swagger service
            services.AddSwaggerGen(c =>
            {{
                c.SwaggerDoc(""v1"", new Info {{ Title = ""{project.Title} API"", Version = ""v1"" }});
            }});
        }}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, Entities.{project.InternalName}Context context)
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
                    c.SwaggerEndpoint(""/swagger/v1/swagger.json"", ""{project.Title} API V1"");
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
