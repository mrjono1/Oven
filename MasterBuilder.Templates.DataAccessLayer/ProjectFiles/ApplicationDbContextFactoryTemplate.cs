using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.DataAccessLayer.ProjectFiles
{
    /// <summary>
    /// Entity Framework Core Context Factory for Migrations
    /// </summary>
    public class ApplicationDbContextFactoryTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationDbContextFactoryTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "ApplicationDbContextFactory.cs";
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
            return $@"using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace {Project.InternalName}.DataAccessLayer
{{
    /// <summary>
    /// Application Db Context Factory
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {{
        public ApplicationDbContext CreateDbContext(string[] args)
        {{
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(""x"");

            return new ApplicationDbContext(optionsBuilder.Options);
        }}
    }}
}}";
        }
    }
}
