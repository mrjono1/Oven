using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates
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
            return $@"Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 15
VisualStudioVersion = 15.0.27130.2010
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}"") = ""{Project.InternalName}"", ""{Project.InternalName}\{Project.InternalName}.csproj"", ""{{{Project.Id.ToString()}}}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{{Project.Id.ToString().ToUpperInvariant()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{Project.Id.ToString().ToUpperInvariant()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{Project.Id.ToString().ToUpperInvariant()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {{{Project.Id.ToString().ToUpperInvariant()}}}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
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
