using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.Entities
{
    public class EntityFrameworkContextTemplate
    {
        public static string FileName(string folder, Project project)
        {
            return Path.Combine(folder, $"{project.InternalName}Context.cs");
        }

        public static string Evaluate(Project project)
        {
            StringBuilder properties = null;
            StringBuilder configurations = null;
            if (project.Entities != null)
            {
                properties = new StringBuilder();
                configurations = new StringBuilder();
                foreach (var item in project.Entities)
                {
                    properties.AppendLine($"        public DbSet<{item.InternalName}> {item.InternalNamePlural} {{ get; set; }}");
                    configurations.AppendLine($"            builder.ApplyConfiguration(new {item.InternalName}Config());");
                }
            }

            return $@"
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using {project.InternalName}.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace {project.InternalName}.Entities
{{
    public class {project.InternalName}Context : DbContext
    {{
        public {project.InternalName}Context(DbContextOptions<{project.InternalName}Context> options) : base(options)
        {{
        }}
{properties}

        protected override void OnModelCreating(ModelBuilder builder)
        {{
            base.OnModelCreating(builder);
{configurations}
        }}

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

            var operations = differ.GetDifferences(databaseModel, currentModel);
            if (operations.Any(o => o.IsDestructiveChange))
            {{
                throw new InvalidOperationException(
                    ""Automatic migration was not applied because it would result in data loss."");
            }}

            var sqlGenerator = this.GetService<IMigrationsSqlGenerator>();
            var commands = sqlGenerator.Generate(operations, currentModel);
            var executor = this.GetService<IMigrationCommandExecutor>();
            executor.ExecuteNonQuery(commands, this.GetService<IRelationalConnection>());
        }}
    }}
}}";
        }


    }
}
