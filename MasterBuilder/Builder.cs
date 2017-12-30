using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MasterBuilder
{
    public class Builder
    {
        private string GetProjectRootFolder()
        {
            return Path.GetFullPath($"{AppDomain.CurrentDomain.BaseDirectory}/../../../");
        }
        
        public async Task<string> Run(string outputDirectory, Request.Project project) {
            
            var fullBuild = true;
            var gitOn = true;

            if (project == null)
            {
                return "null project";
            }
            
            // Validate & Pre Process Project
            if (!project.Validate(out string messages))
            {
                return messages;
            }

            var topProjectDirectory = FileHelper.CreateFolder(outputDirectory, project.InternalName);

            SourceControl.Git git = null;
            Dictionary<string, SourceControl.Models.GetRepository> repos = null;
            if (gitOn)
            {
                git = new SourceControl.Git(topProjectDirectory, project, "master-builder", "jonoclarnette@gmail.com", "wynpvam2lj24cvtw6y3vgghio32x5u5pwnyxiyg5zrmowntm4fwa");
                repos = await git.SetupAndGetRepos();
                
                var rtf = new SourceControl.RequestToFileSystem(topProjectDirectory, project);
                await rtf.Write();

                git.StageCommitPush(repos["Json"], "Build");
            }
            var filesToWrite = new List<Task>();


            // Create Solution Directory
            var solutionDirectory = FileHelper.CreateFolder(topProjectDirectory, project.InternalName);
            var projectDirectory = FileHelper.CreateFolder(solutionDirectory, project.InternalName);

            if (fullBuild)
            {
                // Clean out directory
                FileHelper.CleanProject(projectDirectory, project);

                // Create Directories
                var wwwrootPath = FileHelper.CreateFolder(projectDirectory, "wwwroot");

                // Artifacts
                //FileHelper.CopyFile("favicon.ico", Path.Combine(GetProjectRootFolder(), "CopyFiles"), wwwrootPath);

                filesToWrite.AddRange(new Task[]
                {
                    // Create Solution File
                    FileHelper.WriteTemplate(solutionDirectory, new Templates.SolutionTemplate(project)),
                    
                    // Create Project Files
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.AngularCliJsonTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.ProjectTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.GitIgnoreTemplate()),
                    FileHelper.WriteTemplate(solutionDirectory, new Templates.ProjectFiles.GitIgnoreTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.AppSettingsTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.TypeScriptLintTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.TypeScriptConfigTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.WebPackAdditionsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.WebPackConfigTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.WebPackConfigVendorTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.WebConfigTemplate()),

                    // Views
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.ViewImportsTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.ViewStartTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.Home.IndexTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.Shared.ErrorTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.Shared.LayoutTemplate(project)),

                    FileHelper.WriteTemplate(projectDirectory, new Templates.Extensions.HttpRequestExtensionsTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.CoreModels.IRequestTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.CoreModels.TransferDataTemplate(project)),
                    
                    // ClientApp
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.BootBrowserTsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.BootServerTsTemplate()),

                    // ClientApp/Test
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Test.BootTestTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Test.KarmaConfigTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Test.WebpackConfigTest()),

                    // ClientApp/Polyfills
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Polyfills.BrowserPolyfillsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Polyfills.PolyfillsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Polyfills.RxImportsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Polyfills.ServerPolyfillsTemplate()),

                    // ClientApp/App
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppComponentHtmlTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppComponentScssTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppComponentTsTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppModuleBrowserTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppModuleServerTemplate()),

                    // ClientApp/App/Shared
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.Shared.LinkServiceTemplate()),

                    // wwwroot/assets/i18n
                    FileHelper.WriteTemplate(projectDirectory, new Templates.WwwRoot.i18n.LanguageTemplate()),
                });
            }

            // Create Project Files
            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.PackageJsonTemplate(project)));
            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.StartupTemplate(project)));

            // ClientApp/App
            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppModuleTemplate(project)));


            // ClientApp/app/models
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Models.ModelTemplateBuilder(project)));

            // ClientApp/app/shared
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Shared.ServiceTemplateBuilder(project)));

            // ClientApp/app/containers
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Containers.Ts.ContainerTsTemplateBuilder(project)));
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Containers.Html.ContainerHtmlTemplateBuilder(project)));

            // Entities
            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.Entities.EntityFrameworkContextTemplate(project)));
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Entities.EntityTemplateBuilder(project)));

            // Entity Type Config
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.EntityTypeConfigurations.EntityTypeConfigTemplateBuilder(project)));

            // Controllers
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Controllers.ControllerTemplateBuilder(project)));

            // Models
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Models.ModelTemplateBuilder(project)));
            // Models/Export
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Models.Export.ModelTemplateBuilder(project)));

            // Components
            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.app.components.navmenu.NavmenuComponentHtmlTemplate(project)));
            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.app.components.navmenu.NavmenuComponentTsTemplate(project)));

            // Services
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Services.ServiceTemplateBuilder(project)));
            // Services/Contracts
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Services.Contracts.ServiceContractTemplateBuilder(project)));
            

            await Task.WhenAll(filesToWrite.ToArray());

            if (gitOn)
            {
                git.StageCommitPush(repos[project.InternalName], "Build");
            }
            return "Success";
        }
    }
}
