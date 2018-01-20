using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.Entities
{
    /// <summary>
    /// Entity Framework Core Context
    /// </summary>
    public class EntityFrameworkContextTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public EntityFrameworkContextTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Project.InternalName}Context.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Entities" };
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
        public DbSet<{entity.InternalName}> {entity.InternalNamePlural} {{ get; set; }}");
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
            var items = JsonConvert.DeserializeObject<List<{entity.InternalName}>>(content);");

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
                seedData = $@"        internal async Task Seed()
        {{
{string.Join(Environment.NewLine, seed.Keys)}
        }}

{string.Join(Environment.NewLine, seed.Values)}";
            }

            return $@"using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using {Project.InternalName}.EntityTypeConfigurations;
using Newtonsoft.Json;

namespace {Project.InternalName}.Entities
{{
    /// <summary>
    /// {Project.InternalName} Entity Framework Database Context
    /// </summary>
    public class {Project.InternalName}Context : DbContext
    {{

        /// <summary>
        /// {Project.InternalName} Entity Framework Database Context
        /// </summary>
        public {Project.InternalName}Context(DbContextOptions<{Project.InternalName}Context> options) : base(options)
        {{
        }}
{properties}

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
        public void Initialize()
        {{
            MigrateDatabase();

            {(seed.Any() ? "Seed().Wait();" : string.Empty)}
        }}

        /// <summary>
        /// Migrate Database (not supported by EF Core)
        /// </summary>
        internal void MigrateDatabase()
        {{
            if (Database.EnsureCreated())
            {{
                return;
            }}

            var reporter = new OperationReporter(handler: null);
            var designTimeServiceCollection = new ServiceCollection()
                .AddSingleton<IOperationReporter>(reporter)
                .AddScaffolding(reporter);
            new SqlServerDesignTimeServices().ConfigureDesignTimeServices(designTimeServiceCollection);

            var designTimeServices = designTimeServiceCollection.BuildServiceProvider();

            // TODO: Just use db.Database.EnsureCreated() if the database doesn't exist
            var databaseModelFactory = designTimeServices.GetService<IScaffoldingModelFactory>();
            var databaseModel = (Model)databaseModelFactory.Create(
                Database.GetDbConnection().ConnectionString,
                tables: new string[0],
                schemas: new string[0],
                useDatabaseNames: false);

            var currentModel = Model;

            // Fix up the database model. It was never intended to be used like this. ;-)
            foreach (var entityType in databaseModel.GetEntityTypes())
            {{
                if (entityType.Relational().Schema == databaseModel.Relational().DefaultSchema)
                {{
                    entityType.Relational().Schema = null;
                }}
            }}
            databaseModel.Relational().DefaultSchema = null;
            databaseModel.SqlServer().ValueGenerationStrategy =
                currentModel.SqlServer().ValueGenerationStrategy;
            // TODO: ...more fix up as needed

            var differ = this.GetService<IMigrationsModelDiffer>();

            var operations = differ.GetDifferences(databaseModel, currentModel);{(!Project.AllowDestructiveDatabaseChanges ? @"

            if (operations.Any(o => o.IsDestructiveChange))
            {{
                throw new InvalidOperationException(
                    ""Automatic migration was not applied because it would result in data loss."");
            }}" : string.Empty)}

            var sqlGenerator = this.GetService<IMigrationsSqlGenerator>();
            var commands = sqlGenerator.Generate(operations, currentModel);
            var executor = this.GetService<IMigrationCommandExecutor>();
            executor.ExecuteNonQuery(commands, this.GetService<IRelationalConnection>());
        }}
{seedData}
    }}
}}";
        }
    }
}
