using Oven.Interfaces;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oven.Templates.React.ProjectFiles
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
                { "Microsoft.AspNetCore.All", null },
                { "Swashbuckle.AspNetCore", "3.0.0" },
                { "Swashbuckle.AspNetCore.Swagger", "3.0.0" },
                { "Swashbuckle.AspNetCore.SwaggerUi", "3.0.0" }
            };
            
            foreach (var item in nugetReferences)
            {
                packageReferences.AppendLine($@"    <PackageReference Include=""{item.Key}""{(item.Value == null ? "" : $@" Version=""{item.Value}"" ")}/>");
            }

            return $@"<Project Sdk=""Microsoft.NET.Sdk.Web"">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
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
    <ProjectReference Include=""..\{Project.InternalName}.Api\{Project.InternalName}.Api.csproj"" />
    <ProjectReference Include=""..\{Project.InternalName}.DataAccessLayer\{Project.InternalName}.DataAccessLayer.csproj"" />
  </ItemGroup>
  {(Project.EnableCustomCode ? $@"
  <ItemGroup>
    <ProjectReference Include=""..\{Project.InternalName}.Api.Custom\{Project.InternalName}.Api.Custom.csproj"" />
  </ItemGroup>" : "")}

  <Target Name=""DebugRunWebpack"" BeforeTargets=""Build"" Condition="" '$(Configuration)' == 'Debug' And !Exists('wwwroot\dist') "">
    <!-- Ensure Node.js is installed -->
    <Exec Command=""node --version"" ContinueOnError=""true"">
      <Output TaskParameter=""ExitCode"" PropertyName=""ErrorCode"" />
    </Exec>
    <Error Condition=""'$(ErrorCode)' != '0'"" Text=""Node.js is required to build and run this project. To continue, please install Node.js from https://nodejs.org/, and then restart your command prompt or IDE."" />

    <!-- In development, the dist files won't exist on the first run or when cloning to
         a different machine, so rebuild them if not already present. -->
    <Message Importance=""high"" Text=""Performing first-run Webpack build..."" />
    <Exec Command=""node node_modules/webpack/bin/webpack.js --display-error-details --mode development"" />
  </Target>

  <Target Name=""PublishRunWebpack"" AfterTargets=""ComputeFilesToPublish"">
    <!-- As part of publishing, ensure the JS resources are freshly built in production mode -->
    <Exec Command=""npm install"" />
    <Exec Command=""node node_modules/webpack/bin/webpack.js --colors --display-error-details --mode production"" />

    <!-- Include the newly-built files in the publish output -->
    <ItemGroup>
      <DistFiles Include=""wwwroot\dist\**; src\dist\**"" />
      <ResolvedFileToPublish Include=""@(DistFiles->'%(FullPath)')"" Exclude=""@(ResolvedFileToPublish)"">
        <RelativePath>%(DistFiles.Identity)</RelativePath>
        <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      </ResolvedFileToPublish>
    </ItemGroup>
  </Target>

</Project>";
        }
    }
}
