using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Api.ProjectFiles
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
            return $"{Project.InternalName}.Api.csproj";
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
                { "Microsoft.AspNetCore.All", "2.0.8" },
                { "Microsoft.EntityFrameworkCore", "2.1.3"},
                { "Swashbuckle.AspNetCore", "1.1.0" },
                { "Swashbuckle.AspNetCore.Swagger", "1.1.0" },
                { "Swashbuckle.AspNetCore.SwaggerUi", "1.1.0" },
                { "RestSharp", "106.2.1"}
            };

            if (!Project.UsePutForUpdate)
            {
                nugetReferences.Add("Microsoft.AspNetCore.JsonPatch", "2.0.0");
            }

            if (Project.UseMySql)
            {
                nugetReferences.Add("Pomelo.EntityFrameworkCore.MySql", "2.0.1");
            }
            else
            {
                nugetReferences.Add("Microsoft.EntityFrameworkCore.SqlServer", "2.1.3");
            }

            foreach (var item in nugetReferences)
            {
                packageReferences.AppendLine($@"    <PackageReference Include=""{item.Key}"" Version=""{item.Value}"" />");
            }

            return $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <Version>{Project.MajorVersion}.{Project.MinorVersion}.{Project.BuildVersion}</Version>
  </PropertyGroup>

  <Target Name=""PrepublishScript"" BeforeTargets=""PrepareForPublish""> 
    <ItemGroup>
      <DocFile Include=""bin\$(Configuration)\$(TargetFramework)\*.xml"" />
    </ItemGroup>
    <Copy SourceFiles=""@(DocFile)"" DestinationFolder=""$(PublishDir)"" SkipUnchangedFiles=""false"" />
  </Target>

  <ItemGroup>
{packageReferences}
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include=""..\{Project.InternalName}.DataAccessLayer\{Project.InternalName}.DataAccessLayer.csproj"" />
  </ItemGroup>

</Project>";
        }
    }
}
