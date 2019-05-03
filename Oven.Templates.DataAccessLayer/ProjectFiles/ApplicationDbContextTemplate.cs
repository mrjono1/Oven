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

            var seedFunction = new SeedFunctionTemplate(Project).GetFunction();

            return $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using {Project.InternalName}.DataAccessLayer.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Oven.Shared;

namespace {Project.InternalName}.DataAccessLayer
{{
    /// <summary>
    /// Application Database Context
    /// </summary>
    public class ApplicationDbContext : IApplicationDbContext
    {{
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Application Database Context
        /// </summary>
        public ApplicationDbContext(IOptionsMonitor<Settings> settings)
        {{
            _mongoClient = new MongoClient(settings.CurrentValue.ConnectionString);
            _database = _mongoClient.GetDatabase(settings.CurrentValue.DatabaseName);
        }}
{seedFunction}

        #region IMongoCollection Properties
{string.Join(Environment.NewLine, properties)}
        #endregion

        /// <summary>
        /// Initialize Database
        /// </summary>
        public async Task Initialize()
        {{
            await Seed();
        }}
    }}
}}";
        }
    }
}
