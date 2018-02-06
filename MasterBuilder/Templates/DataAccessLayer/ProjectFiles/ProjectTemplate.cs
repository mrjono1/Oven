using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.DataAccessLayer.ProjectFiles
{
    /// <summary>
    /// Project
    /// </summary>
    public class ProjectTemplate: ITemplate
    {
        private readonly Project Project;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Project.InternalName}.DataAccessLayer.csproj";
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
            StringBuilder packageReferences = new StringBuilder();
            
            var nugetReferences = new Dictionary<string, string>
            {
                { "Microsoft.EntityFrameworkCore", "2.0.1"},
                { "Microsoft.EntityFrameworkCore.Design", "2.0.1"},
                { "Newtonsoft.Json", "10.0.3"}
            };
            
            if (Project.UseMySql)
            {
                nugetReferences.Add("Pomelo.EntityFrameworkCore.MySql", "2.0.1");
            }
            else
            {
                nugetReferences.Add("Microsoft.EntityFrameworkCore.SqlServer", "2.0.1");
            }

            foreach (var item in nugetReferences)
            {
                packageReferences.AppendLine($@"<PackageReference Include=""{item.Key}"" Version=""{item.Value}"" />");
            }

            return $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    {packageReferences}
  </ItemGroup>

</Project>";
        }
    }
}
