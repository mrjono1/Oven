using Oven.Interfaces;
using Oven.Request;
using System.Collections.Generic;
using System.Text;

namespace Oven.Templates.DataAccessLayer.ProjectFiles
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
            var packageReferences = new StringBuilder();
            var nugetReferences = new Dictionary<string, string>
            {
                { "Microsoft.AspNetCore.All", null },
                { "MongoDB.Driver", "2.8.0"},
                { "Newtonsoft.Json", "12.0.2"}
            };

            foreach (var item in nugetReferences)
            {
                packageReferences.AppendLine($@"    <PackageReference Include=""{item.Key}""{(item.Value == null ? "" : $@" Version=""{item.Value}"" ")}/>");
            }

            if (Project.NuGetDependencies != null)
            {
                foreach (var item in Project.NuGetDependencies)
                {
                    packageReferences.AppendLine($@"    <PackageReference Include=""{item.Include}"" Version=""{item.Version}"" IncludeAssets=""{item.IncludeAssets}"" ExcludeAssets=""{item.ExcludeAssets}"" PrivateAssets=""{item.PrivateAssets}"" />");
                }
            }

            return $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
{packageReferences}
  </ItemGroup>
  <PropertyGroup>
    <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
  </PropertyGroup>
</Project>";
        }
    }
}
