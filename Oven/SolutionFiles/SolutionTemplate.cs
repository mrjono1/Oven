using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.SolutionFiles
{
    /// <summary>
    /// Solution file
    /// </summary>
    public class SolutionTemplate: ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public SolutionTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Project.InternalName}.sln";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var customProjects = $@"Project(""{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}"") = ""{Project.InternalName}.Api.Custom"", ""{Project.InternalName}.Api.Custom\{Project.InternalName}.Api.Custom.csproj"", ""{{{Project.ProjectApiCustomId.ToString().ToUpperInvariant()}}}""
EndProject
";
            var customGlobalSection = $@"		{{{Project.ProjectApiCustomId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{Project.ProjectApiCustomId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{Project.ProjectApiCustomId.ToString().ToUpperInvariant()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{Project.ProjectApiCustomId.ToString().ToUpperInvariant()}}}.Release|Any CPU.Build.0 = Release|Any CPU
";

            return $@"Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 15
VisualStudioVersion = 15.0.27130.2027
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}"") = ""{Project.InternalName}"", ""{Project.InternalName}\{Project.InternalName}.csproj"", ""{{{Project.ProjectWebId.ToString().ToUpperInvariant()}}}""
EndProject
Project(""{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}"") = ""{Project.InternalName}.Shared"", ""{Project.InternalName}.Shared\{Project.InternalName}.Shared.csproj"", ""{{{Project.ProjectSharedId.ToString().ToUpperInvariant()}}}""
EndProject
Project(""{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}"") = ""{Project.InternalName}.Api"", ""{Project.InternalName}.Api\{Project.InternalName}.Api.csproj"", ""{{{Project.ProjectApiId.ToString().ToUpperInvariant()}}}""
EndProject
Project(""{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}"") = ""{Project.InternalName}.DataAccessLayer"", ""{Project.InternalName}.DataAccessLayer\{Project.InternalName}.DataAccessLayer.csproj"", ""{{{Project.ProjectDataAccessLayerId.ToString().ToUpperInvariant()}}}""
EndProject
{(!Project.EnableCustomCode ? "" : customProjects)}Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{{Project.ProjectWebId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{Project.ProjectWebId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{Project.ProjectWebId.ToString().ToUpperInvariant()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {{{Project.ProjectWebId.ToString().ToUpperInvariant()}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{Project.ProjectSharedId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{Project.ProjectSharedId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{Project.ProjectSharedId.ToString().ToUpperInvariant()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {{{Project.ProjectSharedId.ToString().ToUpperInvariant()}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{Project.ProjectApiId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{Project.ProjectApiId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{Project.ProjectApiId.ToString().ToUpperInvariant()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {{{Project.ProjectApiId.ToString().ToUpperInvariant()}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{Project.ProjectDataAccessLayerId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{Project.ProjectDataAccessLayerId.ToString().ToUpperInvariant()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{Project.ProjectDataAccessLayerId.ToString().ToUpperInvariant()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{Project.ProjectDataAccessLayerId.ToString().ToUpperInvariant()}}}.Release|Any CPU.Build.0 = Release|Any CPU
{(!Project.EnableCustomCode ? "" : customGlobalSection)}	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {{{Project.Id.ToString().ToUpperInvariant()}}}
	EndGlobalSection
EndGlobal
";
        }
    }
}
