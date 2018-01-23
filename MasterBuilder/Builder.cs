using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterBuilder
{
    /// <summary>
    /// Master Builder Entry point
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Run Master Builder
        /// </summary>
        public async Task<string> Run(string outputDirectory, Request.Project project)
        {
            var gitOn = true;

#if DEBUG
            gitOn = false;
#endif

            if (project == null)
            {
                return "null project";
            }
            
            // Validate & Pre Process Project
            if (!project.ValidateAndResolve(out string messages))
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


            // Create Solution Directory
            var solutionDirectory = FileHelper.CreateFolder(topProjectDirectory, project.InternalName);
            var projectDirectory = FileHelper.CreateFolder(solutionDirectory, project.InternalName);

            var projectWriter = new Helpers.ProjectWriter(projectDirectory, project.CleanDirectoryIgnoreDirectories);
            
            // Create Directories
            var wwwrootPath = FileHelper.CreateFolder(projectDirectory, "wwwroot");

            // Artifacts
            projectWriter.AddFolder(new string[] { "CopyFiles", "assets", "favicons" }, new string[] { "wwwroot", "assets", "favicons" });
            projectWriter.AddTemplate(new Templates.Assets.Favicons.ManifestTemplate(project));
            
            // Create Solution File
            projectWriter.AddTemplate(solutionDirectory, new Templates.SolutionTemplate(project));

            // Create Project Files
            projectWriter.AddTemplate(new Templates.ProjectFiles.PackageJsonTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.StartupTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.AngularCliJsonTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.ProjectTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.GitIgnoreTemplate());
            projectWriter.AddTemplate(solutionDirectory, new Templates.ProjectFiles.GitIgnoreTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.AppSettingsTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.TypeScriptLintTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.TypeScriptConfigTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.WebPackAdditionsTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.WebPackConfigTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.WebPackConfigVendorTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.WebConfigTemplate());
 
            // Views
            projectWriter.AddTemplate(new Templates.Views.ViewImportsTemplate(project));
            projectWriter.AddTemplate(new Templates.Views.ViewStartTemplate());
            projectWriter.AddTemplate(new Templates.Views.Home.IndexTemplate(project));
            projectWriter.AddTemplate(new Templates.Views.Shared.ErrorTemplate(project));
            projectWriter.AddTemplate(new Templates.Views.Shared.LayoutTemplate(project));

            if (project.ServerSideRendering)
            {
                projectWriter.AddTemplate(new Templates.Extensions.HttpRequestExtensionsTemplate(project));
                projectWriter.AddTemplate(new Templates.CoreModels.IRequestTemplate(project));
                projectWriter.AddTemplate(new Templates.CoreModels.TransferDataTemplate(project));
            }
            
            // wwwroot/assets/i18n
            projectWriter.AddTemplate(new Templates.WwwRoot.i18n.LanguageTemplate());

            // ClientApp
            projectWriter.AddTemplate(new Templates.ClientApp.BootBrowserTsTemplate());
            if (project.ServerSideRendering)
            {
                projectWriter.AddTemplate(new Templates.ClientApp.BootServerTsTemplate());
            }

            // ClientApp/Test
            projectWriter.AddTemplate(new Templates.ClientApp.Test.BootTestTemplate());
            projectWriter.AddTemplate(new Templates.ClientApp.Test.KarmaConfigTemplate());
            projectWriter.AddTemplate(new Templates.ClientApp.Test.WebpackConfigTestTemplate());

            // ClientApp/Polyfills
            projectWriter.AddTemplate(new Templates.ClientApp.Polyfills.BrowserPolyfillsTemplate());
            projectWriter.AddTemplate(new Templates.ClientApp.Polyfills.PolyfillsTemplate());
            projectWriter.AddTemplate(new Templates.ClientApp.Polyfills.RxImportsTemplate());
            if (project.ServerSideRendering)
            {
                projectWriter.AddTemplate(new Templates.ClientApp.Polyfills.ServerPolyfillsTemplate());
            }

            // ClientApp/App
            projectWriter.AddTemplate(new Templates.ClientApp.App.AppModuleTemplate(project));
            projectWriter.AddTemplate(new Templates.ClientApp.App.AppComponentHtmlTemplate());
            projectWriter.AddTemplate(new Templates.ClientApp.App.AppComponentScssTemplate());
            projectWriter.AddTemplate(new Templates.ClientApp.App.AppComponentTsTemplate(project));
            projectWriter.AddTemplate(new Templates.ClientApp.App.AppModuleBrowserTemplate());
            if (project.ServerSideRendering)
            {
                projectWriter.AddTemplate(new Templates.ClientApp.App.AppModuleServerTemplate());
            }
            projectWriter.AddTemplate(new Templates.ClientApp.App.MaterialModuleTemplate(project));

            // ClientApp/App/Shared
            projectWriter.AddTemplate(new Templates.ClientApp.App.Shared.ServiceTemplateBuilder(project));
            
            // ClientApp/app/models
            projectWriter.AddTemplate(new Templates.ClientApp.App.Models.ModelTemplateBuilder(project));
            
            // ClientApp/app/containers
            projectWriter.AddTemplate(new Templates.ClientApp.App.Containers.Ts.ContainerTsTemplateBuilder(project));
            projectWriter.AddTemplate(new Templates.ClientApp.App.Containers.Html.ContainerHtmlTemplateBuilder(project));

            // Entities
            projectWriter.AddTemplate(new Templates.Entities.EntityFrameworkContextTemplate(project));
            projectWriter.AddTemplate(new Templates.Entities.EntityTemplateBuilder(project));

            // Entity Type Config
            projectWriter.AddTemplate(new Templates.EntityTypeConfigurations.EntityTypeConfigTemplateBuilder(project));

            // Controllers
            projectWriter.AddTemplate(new Templates.Controllers.ControllerTemplateBuilder(project));

            // Models
            projectWriter.AddTemplate(new Templates.Models.ModelTemplateBuilder(project));
            // Models/Export
            projectWriter.AddTemplate(new Templates.Models.Export.ModelTemplateBuilder(project));

            // Components
            projectWriter.AddTemplate(new Templates.ClientApp.app.components.navmenu.NavmenuComponentHtmlTemplate(project));
            projectWriter.AddTemplate(new Templates.ClientApp.app.components.navmenu.NavmenuComponentTsTemplate(project));

            // Services
            projectWriter.AddTemplate(new Templates.Services.ServiceTemplateBuilder(project));
            // Services/Contracts
            projectWriter.AddTemplate(new Templates.Services.Contracts.ServiceContractTemplateBuilder(project));

            var errors = await projectWriter.WriteAndClean();

            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            if (gitOn)
            {
                git.StageCommitPush(repos[project.InternalName], "Build");
            }
            return "Success";
        }
    }
}
