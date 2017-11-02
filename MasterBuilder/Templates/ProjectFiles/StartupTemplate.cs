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
            return $@"using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace {project.InternalName}
{{
    public class Startup
    {{
        public Startup(IConfiguration configuration)
        {{
            Configuration = configuration;
        }}

        public IConfiguration Configuration {{ get; }}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {{
            services.AddDbContext<Entities.{project.InternalName}Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(""DefaultConnection"")));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {{
                c.SwaggerDoc(""v1"", new Info {{ Title = ""{project.Title} API"", Version = ""v1"" }});
            }});
        }}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {{
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {{
                var context = serviceScope.ServiceProvider.GetRequiredService<Entities.{project.InternalName}Context>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }}
            if (env.IsDevelopment())
            {{
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {{
                    HotModuleReplacement = true
                }});
            }}
            else
            {{
                app.UseExceptionHandler(""/Home/Error"");
            }}

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {{
                c.SwaggerEndpoint(""/swagger/v1/swagger.json"", ""{project.Title} API V1"");
            }});

            app.UseMvc(routes =>
            {{
                routes.MapRoute(
                    name: ""default"",
                    template: ""{{controller=Home}}/{{action=Index}}/{{id?}}"");

                routes.MapSpaFallbackRoute(
                    name: ""spa-fallback"",
                    defaults: new {{ controller = ""Home"", action = ""Index"" }});
            }});
        }}
    }}
}}";
        }
    }
}
