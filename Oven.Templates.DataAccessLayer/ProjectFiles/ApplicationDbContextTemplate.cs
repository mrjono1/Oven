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
    public class ApplicationDbContextTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationDbContextTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "ApplicationDbContext.cs";
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
            StringBuilder properties = null;
            StringBuilder configurations = null;
            if (Project.Entities != null)
            {
                properties = new StringBuilder();
                configurations = new StringBuilder();
                foreach (var entity in Project.Entities)
                {
                    properties.AppendLine($@"            /// <summary>
        /// {entity.Title}
        /// </summary>
        public DbSet<Entities.{entity.InternalName}> {entity.InternalNamePlural} {{ get; set; }}");
                    configurations.AppendLine($"            builder.ApplyConfiguration(new {entity.InternalName}Config());");
                }
            }

            var seed = new Dictionary<string, string>();
            foreach (var entity in Project.Entities.Where(e => e.Seed != null))
            {
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(entity.Seed.JsonData);
                
                var seedStringBuilder = new StringBuilder($@"        private async Task {entity.InternalName}Seed(){{
            var content = {content};
            // TODO: possibly could convert this to delegate so only executed if needed
            var items = JsonConvert.DeserializeObject<List<Entities.{entity.InternalName}>>(content);");

                seedStringBuilder.AppendLine($@"
            if (!{entity.InternalNamePlural}.Any())
            {{
                await {entity.InternalNamePlural}.AddRangeAsync(items);
            }}");

                switch (entity.Seed.SeedType)
                {
                    case SeedType.EnsureAllAdded:
                        // TODO: can this be done better with AttachRange?
                seedStringBuilder.AppendLine($@"           else
            {{
                foreach (var item in items)
                {{
                    var existing = await {entity.InternalNamePlural}.AnyAsync(p => p.Id == item.Id);
                    if (!existing){{
                        await {entity.InternalNamePlural}.AddAsync(item);
                    }}
                }}
            }}");
                        break;
                    case SeedType.EnsureAllUpdated:
                        seedStringBuilder.AppendLine($@"            else
            {{
                foreach (var item in items)
                {{
                    var existing = await {entity.InternalNamePlural}.AsNoTracking().SingleOrDefaultAsync(a => a.Id == item.Id);
                    if (existing == null){{
                       await {entity.InternalNamePlural}.AddAsync(item);
                    }}
                    else
                    {{
                        {entity.InternalNamePlural}.Attach(item);
                    }}
                }}
            }}");
                        break;
                }

                seedStringBuilder.AppendLine(@"            await SaveChangesAsync();
        }");
                seed.Add($"            await {entity.InternalName}Seed();", seedStringBuilder.ToString());
            }
            string seedData = null;
            if (seed.Any())
            {
                seedData = $@"        #region Seed
        internal async Task Seed()
        {{
{string.Join(Environment.NewLine, seed.Keys)}
        }}

{string.Join(Environment.NewLine, seed.Values)}
        #endregion";
            }

            string dbConnection = null;
            if (Project.UseMySql)
            {
#if DEBUG
                dbConnection = $@"""Server=localhost;database={Project.InternalName};uid=root;pwd=password;""";
#else
                dbConnection = $@"Environment.GetEnvironmentVariable(""MYSQLCONNSTR_localdb"").ToString()";
#endif
            }
            else
            {
                dbConnection = $@"Database.GetDbConnection().ConnectionString";
            }
            return $@"using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using {Project.InternalName}.DataAccessLayer.Entities;
using {Project.InternalName}.DataAccessLayer.EntityTypeConfigurations;
using Newtonsoft.Json;

namespace {Project.InternalName}.DataAccessLayer
{{
    /// <summary>
    /// Application Database Context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {{

        /// <summary>
        /// Application Database Context
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {{
        }}

        #region DBSet Properties
{properties}
        #endregion

        /// <summary>
        /// On Model Creating
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {{
            base.OnModelCreating(builder);
{configurations}
        }}

        /// <summary>
        /// Initialize Database
        /// </summary>
        public async Task Initialize()
        {{
            await Database.MigrateAsync();
            await Database.EnsureCreatedAsync();
            {(seed.Any() ? "await Seed();" : string.Empty)}
        }}
{seedData}
    }}
}}";
        }
    }
}
