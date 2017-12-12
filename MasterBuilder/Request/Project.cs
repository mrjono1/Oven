using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Request
{
    /// <summary>
    /// Project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Constructor sets default values
        /// </summary>
        public Project()
        {
            CleanDirectory = true;
            CleanDirectoryIgnoreDirectories = new string[]
            {
                "bin",
                "obj",
                "node_modules",
                "wwwroot",
                "Properties"
            };

            DefaultNugetReferences = new Dictionary<string, string>
            {
                { "Microsoft.AspNetCore.All", "2.0.0" },
                { "Microsoft.EntityFrameworkCore", "2.0.0"},
                { "Microsoft.EntityFrameworkCore.SqlServer", "2.0.0"},
                { "Microsoft.AspNetCore.JsonPatch", "2.0.0" },
                { "Swashbuckle.AspNetCore", "1.0.0" },
                { "Swashbuckle.AspNetCore.Swagger", "1.0.0" },
                { "Swashbuckle.AspNetCore.SwaggerUi", "1.0.0" }
            };
            ImutableDatabase = true;
        }
        /// <summary>
        /// Uniqueidentifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Clean the output directory before build
        /// </summary>
        public bool CleanDirectory { get; set; }
        /// <summary>
        /// Internal Name
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Major version
        /// </summary>
        public int MajorVersion { get; set; }
        /// <summary>
        /// Minor Version
        /// </summary>
        public int MinorVersion { get; set; }
        /// <summary>
        /// Build Version
        /// </summary>
        public int BuildVersion { get; set; }

        /// <summary>
        /// Semantic Version
        /// </summary>
        internal string Version
        {
            get
            {
                return $"{MajorVersion}.{MinorVersion}.{BuildVersion}";
            }
        }
        /// <summary>
        /// Entities
        /// </summary>
        public IEnumerable<Entity> Entities { get; set; }
        /// <summary>
        /// Screens
        /// </summary>
        public IEnumerable<Screen> Screens { get; set; }
        /// <summary>
        /// Menu Items
        /// </summary>
        public IEnumerable<MenuItem> MenuItems { get; set; }

        /// <summary>
        /// Folders to ignore when cleaning out the directory on build
        /// </summary>
        public string[] CleanDirectoryIgnoreDirectories { get; set; }
        /// <summary>
        /// SQL Database Connection String
        /// </summary>
        public string DatabaseConnectionString { get; set; }
        /// <summary>
        /// If true database tables and columns are all uniqueidentifiers making database 
        /// hard to use but less chance of needing to change database columns which can result in data loss
        /// </summary>
        public bool ImutableDatabase { get; set; }
        /// <summary>
        /// Default Screen to load
        /// </summary>
        public Guid DefaultScreenId { get; set; }
        /// <summary>
        /// The List of Default Nuget Packages
        /// </summary>
        internal Dictionary<string, string> DefaultNugetReferences { get; set; }
        /// <summary>
        /// Allow Destructive Database Change, things like dropping columns, tables, keys
        /// </summary>
        public bool AllowDestructiveDatabaseChanges { get; set; }
        
        /// <summary>
        /// Validate a whole project, also may resolve some issues or perform upgrades
        /// </summary>
        /// <param name="messages">returns "Success" or details an issue found</param>
        internal bool Validate(out string messages)
        {
            if (Screens == null || !Screens.Any())
            {
                messages = "No Screens have been defined";
                return false;
            }
            foreach (var screen in Screens)
            {
                if (!screen.Validate(out messages))
                {
                    return false;
                }
            }

            if (Entities == null || !Entities.Any())
            {
                messages = "No Entities have been defined";
                return false;
            }
            foreach (var entity in Entities)
            {
                if (!entity.Validate(out messages))
                {
                    return false;
                }
            }

            messages = "Success";
            return true;
        }
    }
}
