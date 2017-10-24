using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates
{
    public class SolutionTemplate
    {
        public static string FileName(Project project)
        {
            return $"{project.InternalName}.sln";
        }
        public static string Evaluate(Project project)
        {
            string solutionGuid = Guid.NewGuid().ToString();
            return $@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 15
VisualStudioVersion = 15.0.27004.2005
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}"") = ""{project.InternalName}"", ""{project.InternalName}\{project.InternalName}.csproj"", ""{{{project.Id.ToString()}}}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{{project.Id.ToString()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{project.Id.ToString()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{project.Id.ToString()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {{{project.Id.ToString()}}}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {{{solutionGuid}}}
	EndGlobalSection
EndGlobal
";
        }
    }
}
