using Humanizer;
using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oven.Templates.DataAccessLayer.ProjectFiles
{
    /// <summary>
    /// Entity Framework Core Context
    /// </summary>
    public class DatabaseContextTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseContextTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "DatabaseContext.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[0];
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            // Create Entity Services
            var services = new List<string>();
            foreach (var entity in Project.Entities)
            {
                if (Project.Screens.Any(_ => _.EntityId == entity.Id))
                {
                    services.Add($"{{ typeof(I{entity.InternalName}Service), typeof({entity.InternalName}Service) }}");
                }
            }

            return $@"using Kitchen.DataAccessLayer.Services;
using Kitchen.DataAccessLayer.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace {Project.InternalName}.DataAccessLayer
{{
    public class DatabaseContext
    {{
        public Dictionary<Type, Type> GetServices(IServiceCollection services, IConfiguration configuration)
        {{
            services.Configure<Settings>(configuration.GetSection(""DatabaseSettings""));

            // Services
            var servicesList = new Dictionary<Type, Type>
            {{
                {{typeof(IApplicationDbContext), typeof(ApplicationDbContext)}},
                {string.Join(string.Concat(",", Environment.NewLine, "                "), services)}
            }};

            return servicesList;
        }}
    }}
}}";
        }
    }
}
