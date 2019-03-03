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
            var properties = new List<string>();

            if (Project.Entities != null)
            {
                foreach (var entity in Project.Entities)
                {
                    properties.Add($@"        private IMongoCollection<{entity.InternalName}> _{entity.InternalNamePlural.Camelize()};

        /// <summary>
        /// {entity.Title}
        /// </summary>
        public IMongoCollection<Entities.{entity.InternalName}> {entity.InternalNamePlural}
        {{ 
            get
            {{
                if (_{entity.InternalNamePlural.Camelize()} == null)
                {{
                    _{entity.InternalNamePlural.Camelize()} = _database.GetCollection<{entity.InternalName}>(""{entity.InternalNamePlural}"");
                }}
                return _{entity.InternalNamePlural.Camelize()};
            }}
        }}");
                }
            }

            var seed = new Dictionary<string, string>();
      /*      foreach (var entity in Project.Entities.Where(e => e.Seed != null))
            {
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(entity.Seed.JsonData);
                
                var seedStringBuilder = new StringBuilder($@"        private async Task {entity.InternalName}Seed()
        {{
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
            }*/
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

            string dbConnection = $@"Database.GetDbConnection().ConnectionString";
            
            return $@"using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using {Project.InternalName}.DataAccessLayer.Entities;
using Newtonsoft.Json;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace {Project.InternalName}.DataAccessLayer
{{
    /// <summary>
    /// Application Database Context
    /// </summary>
    public class ApplicationDbContext : IApplicationDbContext
    {{
        private MongoClient _mongoClient;
        private IMongoDatabase _database;

        /// <summary>
        /// Application Database Context
        /// </summary>
        public ApplicationDbContext(IOptionsMonitor<Settings> settings)
        {{
            _mongoClient = new MongoClient(settings.CurrentValue.ConnectionString);
            _database = _mongoClient.GetDatabase(settings.CurrentValue.DatabaseName);
        }}

        #region IMongoCollection Properties
{string.Join(Environment.NewLine, properties)}
        #endregion

        /// <summary>
        /// Initialize Database
        /// </summary>
        public async Task Initialize()
        {{
            {(seed.Any() ? "await Seed();" : string.Empty)}
        }}
{seedData}
    }}
}}";
        }
    }
}
