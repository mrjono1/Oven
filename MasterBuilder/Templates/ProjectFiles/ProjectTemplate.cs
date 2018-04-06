using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.ProjectFiles
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
            return $"{Project.InternalName}.csproj";
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
                { "Microsoft.AspNetCore.All", "2.0.5" },
                { "Microsoft.EntityFrameworkCore", "2.0.2"},
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
                nugetReferences.Add("Microsoft.EntityFrameworkCore.SqlServer", "2.0.2");
            }

            foreach (var item in nugetReferences)
            {
                packageReferences.AppendLine($@"    <PackageReference Include=""{item.Key}"" Version=""{item.Value}"" />");
            }

            return $@"<Project Sdk=""Microsoft.NET.Sdk.Web"">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked>
    <TypeScriptToolsVersion>Latest</TypeScriptToolsVersion>
    <IsPackable>false</IsPackable>
    <Version>{Project.MajorVersion}.{Project.MinorVersion}.{Project.BuildVersion}</Version>
  </PropertyGroup>

  <Target Name=""PrepublishScript"" BeforeTargets=""PrepareForPublish""> 
    <ItemGroup>
      <DocFile Include=""bin\$(Configuration)\$(TargetFramework)\*.xml"" />
    </ItemGroup>
    <Copy SourceFiles=""@(DocFile)"" DestinationFolder=""$(PublishDir)"" SkipUnchangedFiles=""false"" />
  </Target>
  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='Debug|AnyCPU'"">
    <DocumentationFile>bin\Debug\netcoreapp2.0\{Project.InternalName}.xml</DocumentationFile>
  </PropertyGroup> 
  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='Release|AnyCPU'"">
    <DocumentationFile>bin\Release\netcoreapp2.0\{Project.InternalName}.xml</DocumentationFile>
  </PropertyGroup>

  <ItemGroup>
{packageReferences}
  </ItemGroup>

  <ItemGroup>
    <!-- Files not to show in IDE -->
    <None Remove=""yarn.lock"" />
    <Content Remove=""wwwroot\dist\**"" />
    <None Remove=""ClientApp\dist\**"" />
    <Content Remove=""coverage\**"" />

    <!-- Files not to publish (note that the 'dist' subfolders are re-added below) -->
    <Content Remove=""ClientApp\**"" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include=""..\{Project.InternalName}.DataAccessLayer\{Project.InternalName}.DataAccessLayer.csproj"" />
  </ItemGroup>

  <Target Name=""DebugRunWebpack"" BeforeTargets=""Build"" Condition="" '$(Configuration)' == 'Debug' And !Exists('wwwroot\dist') "">
    <!-- Ensure Node.js is installed -->
    <Exec Command=""node --version"" ContinueOnError=""true"">
      <Output TaskParameter=""ExitCode"" PropertyName=""ErrorCode"" />
    </Exec>
    <Error Condition=""'$(ErrorCode)' != '0'"" Text=""Node.js is required to build and run this project. To continue, please install Node.js from https://nodejs.org/, and then restart your command prompt or IDE."" />

    <!-- In development, the dist files won't exist on the first run or when cloning to
         a different machine, so rebuild them if not already present. -->
    <Message Importance=""high"" Text=""Performing first-run Webpack build..."" />
    <Exec Command=""npm install"" />
    <Exec Command=""node node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js"" />
    <Exec Command=""node node_modules/webpack/bin/webpack.js --env.dev"" />
  </Target>
  <Target Name=""RunWebpack"" BeforeTargets=""Build"" Condition="" '$(Configuration)' == 'Release' "">
    <!-- As part of publishing, ensure the JS resources are freshly built in production mode -->
    <Message Importance=""high"" Text=""Performing Release Webpack build..."" />
    <Exec Command=""npm install"" />
    <Exec Command=""node node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js --env.prod"" />
    <Exec Command=""node node_modules/webpack/bin/webpack.js --env.prod"" />

    <!-- Include the newly-built files in the publish output -->
    <ItemGroup>
      <DistFiles Include=""wwwroot\dist\**; ClientApp\dist\**"" />
      <ResolvedFileToPublish Include=""@(DistFiles->'%(FullPath)')"" Exclude=""@(ResolvedFileToPublish)"">
        <RelativePath>%(DistFiles.Identity)</RelativePath>
        <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      </ResolvedFileToPublish>
    </ItemGroup>
  </Target>
  <Target Name=""CleanDist"" AfterTargets=""Clean"">
    <ItemGroup>
      <FilesToDelete Include=""ClientApp\dist\**; wwwroot\dist\**"" />
    </ItemGroup>
    <Delete Files=""@(FilesToDelete)"" />
    <RemoveDir Directories=""ClientApp\dist; wwwroot\dist"" />
  </Target>
</Project>";
        }
    }
}
