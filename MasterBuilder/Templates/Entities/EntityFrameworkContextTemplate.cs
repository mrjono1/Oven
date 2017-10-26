﻿using MasterBuilder.Request;
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

namespace {project.InternalName}.Entities
{{
    public class {project.InternalName}Context : DbContext
    {{
{properties}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {{
            optionsBuilder.UseSqlServer(""{project.DatabaseConnectionString}"");
        }}

        protected override void OnModelCreating(ModelBuilder builder)
        {{
            base.OnModelCreating(builder);
{configurations}
        }}
    }}
}}";
        }


    }
}
