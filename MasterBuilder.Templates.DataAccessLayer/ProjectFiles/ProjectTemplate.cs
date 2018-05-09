using MasterBuilder.Interfaces;
using MasterBuilder.Request;
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
            var packageReferences = new StringBuilder();

            var efVersion = "2.0.3";
            var nugetReferences = new Dictionary<string, string>
            {
                { "Microsoft.EntityFrameworkCore", efVersion},
                { "Microsoft.EntityFrameworkCore.Design", efVersion},
                { "Microsoft.EntityFrameworkCore.SqlServer", efVersion },
                { "Newtonsoft.Json", "11.0.2"}
            };
            
            if (Project.UseMySql)
            {
                nugetReferences.Add("Pomelo.EntityFrameworkCore.MySql", "2.0.1");
            }

            if (Project.IncludeSupportForSpatial)
            {
                // This is the spatial support provided by the OData team - it is not the perfect solution,
                // but until https://github.com/aspnet/EntityFrameworkCore/issues/1100 and perhaps https://github.com/dotnet/corefx/issues/12034
                // are implemented this is the next best option.
                nugetReferences.Add("Microsoft.Spatial", "7.4.3");
            }

            foreach (var item in nugetReferences)
            {
                packageReferences.AppendLine($@"    <PackageReference Include=""{item.Key}"" Version=""{item.Value}"" />");
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
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
{packageReferences}
  </ItemGroup>
  <ItemGroup>
    <DotNetCliToolReference Include=""Microsoft.EntityFrameworkCore.Tools.DotNet"" Version=""{efVersion}"" />
  </ItemGroup>
  <PropertyGroup>
    <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
  </PropertyGroup>
</Project>";
        }
    }
}
