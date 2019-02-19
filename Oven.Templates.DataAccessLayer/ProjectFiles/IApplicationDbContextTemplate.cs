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
    /// Db Context
    /// </summary>
    public class IApplicationDbContextTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public IApplicationDbContextTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "IApplicationDbContext.cs";
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
                    properties.Add($@"        IMongoCollection<Entities.{entity.InternalName}> {entity.InternalNamePlural} {{ get; }}");
                }
            }

            return $@"using MongoDB.Driver;

namespace {Project.InternalName}.DataAccessLayer
{{
    /// <summary>
    /// Application Database Context
    /// </summary>
    public interface IApplicationDbContext
    {{
{string.Join(Environment.NewLine, properties)}
    }}
}}";
        }
    }
}
