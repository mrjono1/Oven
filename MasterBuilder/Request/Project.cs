﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Request
{
    public class Project
    {
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

        public bool CleanDirectory { get; set; }
        public Guid Id { get; set; }

        public string InternalName { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }

        public IEnumerable<Entity> Entities { get; set; }
        public IEnumerable<Screen> Screens { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }

        public string[] CleanDirectoryIgnoreDirectories { get; set; }
        public string DatabaseConnectionString { get; set; }
        public bool ImutableDatabase { get; set; }
        public Guid DefaultScreenId { get; set; }
        internal Dictionary<string, string> DefaultNugetReferences { get; set; }
    }
}
