using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;

namespace MasterBuilder.Templates.SolutionFiles
{
    /// <summary>
    /// Git ignore
    /// </summary>
    public class GitIgnoreTemplate :ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public GitIgnoreTemplate(Project project)
        {
            Project = project;
        }
        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileName()
        {
            return ".gitignore";
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
        /// <returns></returns>
        public string GetFileContent()
        {
            var gitIgnore = new List<string> { @"/Properties/launchSettings.json

## Ignore Visual Studio temporary files, build results, and
## files generated by popular Visual Studio add-ons.

# User-specific files
*.suo
*.user
*.userosscache
*.sln.docstates

# User-specific files (MonoDevelop/Xamarin Studio)
*.userprefs

# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
build/
bld/
bin/
Bin/
obj/
Obj/

# Visual Studio 2015 cache/options directory
.vs/
**/wwwroot/dist/
**/wwwroot/*.js
**/wwwroot/*.css
**/wwwroot/*.html
/ClientApp/dist/


# MSTest test Results
[Tt]est[Rr]esult*/
[Bb]uild[Ll]og.*

# NUNIT
*.VisualState.xml
TestResult.xml

# Build Results of an ATL Project
[Dd]ebugPS/
[Rr]eleasePS/
dlldata.c

*_i.c
*_p.c
*_i.h
*.ilk
*.meta
*.obj
*.pch
*.pdb
*.pgc
*.pgd
*.rsp
*.sbr
*.tlb
*.tli
*.tlh
*.tmp
*.tmp_proj
*.log
*.vspscc
*.vssscc
.builds
*.pidb
*.svclog
*.scc

# Chutzpah Test files
_Chutzpah*

# Visual Studio profiler
*.psess
*.vsp
*.vspx
*.sap

# Guidance Automation Toolkit
*.gpState

# ReSharper is a .NET coding add-in
_ReSharper*/
*.[Rr]e[Ss]harper
*.DotSettings.user

# JustCode is a .NET coding add-in
.JustCode

# TeamCity is a build add-in
_TeamCity*

# DotCover is a Code Coverage Tool
*.dotCover

# Web workbench (sass)
.sass-cache/

# DocProject is a documentation generator add-in
DocProject/buildhelp/
DocProject/Help/*.HxT
DocProject/Help/*.HxC
DocProject/Help/*.hhc
DocProject/Help/*.hhk
DocProject/Help/*.hhp
DocProject/Help/Html2
DocProject/Help/html

# NuGet Packages
*.nupkg
# The packages folder can be ignored because of Package Restore
**/packages/*
# except build/, which is used as an MSBuild target.
!**/packages/build/
# Uncomment if necessary however generally it will be regenerated when needed
#!**/packages/repositories.config

# Microsoft Azure Build Output
csx/
*.build.csdef

# Microsoft Azure Emulator
ecf/
rcf/

# Microsoft Azure ApplicationInsights config file
ApplicationInsights.config

# Windows Store app package directory
AppPackages/
BundleArtifacts/

# Visual Studio cache files
# files ending in .cache can be ignored
*.[Cc]ache
# but keep track of directories ending in .cache
!*.[Cc]ache/

# Others
ClientBin/
~$*
*~
*.dbmdl
*.dbproj.schemaview
*.pfx
*.publishsettings
orleans.codegen.cs

**/node_modules/

/yarn.lock

# Backup & report files from converting an old project file
# to a newer Visual Studio version. Backup files are not needed,
# because we have git ;-)
_UpgradeReport_Files/
Backup*/
UpgradeLog*.XML
UpgradeLog*.htm

# Node.js Tools for Visual Studio
.ntvs_analysis.dat

# Paket dependency manager
.paket/paket.exe" };

            if (Project.React)
            {
                gitIgnore.Add("_Layout.cshtml");
            }
            return string.Join(Environment.NewLine, gitIgnore);
        }
    }
}
