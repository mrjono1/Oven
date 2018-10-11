using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oven.Templates.Api.Custom.ProjectFiles
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
            return $"{Project.InternalName}.Api.Custom.csproj";
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
            return $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <Version>{Project.MajorVersion}.{Project.MinorVersion}.{Project.BuildVersion}</Version>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include=""..\{Project.InternalName}.Services\{Project.InternalName}.Services.csproj"" />
  </ItemGroup>

</Project>";
        }
    }
}
