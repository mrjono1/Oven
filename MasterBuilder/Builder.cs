using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterBuilder
{
    /// <summary>
    /// Master Builder Entry point
    /// </summary>
    public class Builder
    {
        private readonly BuilderSettings builderSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        public Builder(BuilderSettings builderSettings)
        {
            this.builderSettings = builderSettings;
        }

        /// <summary>
        /// Run Master Builder
        /// </summary>
        public async Task<string> Run(Request.Project project)
        {
            if (project == null)
            {
                return "null project";
            }
            
            // Validate & Pre Process Project
            if (!project.ValidateAndResolve(out string messages))
            {
                return messages;
            }

            var topProjectDirectory = FileHelper.CreateFolder(builderSettings.OutputDirectory, project.InternalName);

            SourceControl.Git git = null;
            Dictionary<string, SourceControl.Models.GetRepository> repos = null;
            if (builderSettings.GitPushOn || builderSettings.GitPullOn || builderSettings.CreateMigrations)
            {
                git = new SourceControl.Git(topProjectDirectory, project, builderSettings.GitUserName, builderSettings.GitEmail, builderSettings.VstsPersonalAccessToken);
            }

            if (builderSettings.GitPullOn)
            {
                repos = await git.SetupAndGetRepos();
            }
            
            // Create Solution Directory
            var solutionDirectory = FileHelper.CreateFolder(topProjectDirectory, project.InternalName);
            var webProjectDirectory = FileHelper.CreateFolder(solutionDirectory, project.InternalName);
            var dalProjectDirectory = FileHelper.CreateFolder(solutionDirectory, $"{project.InternalName}.DataAccessLayer");

            var projectWriter = new Helpers.SolutionWriter(webProjectDirectory, project.CleanDirectoryIgnoreDirectories);

            // Create Solution File
            projectWriter.AddTemplate(solutionDirectory, new Templates.SolutionTemplate(project));

            #region Web Project
            // Create Directories
            var wwwrootPath = FileHelper.CreateFolder(webProjectDirectory, "wwwroot");

            // Artifacts
            projectWriter.AddFolder(new string[] { "CopyFiles", "assets", "favicons" }, new string[] { "wwwroot", "assets", "favicons" });
            projectWriter.AddTemplate(new Templates.Assets.Favicons.ManifestTemplate(project));

            // Create Project Files
            projectWriter.AddTemplate(new Templates.ProjectFiles.PackageJsonTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.StartupTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.AngularCliJsonTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.ProjectTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.GitAttributesTemplate());
            projectWriter.AddTemplate(solutionDirectory, new Templates.ProjectFiles.GitAttributesTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.GitIgnoreTemplate());
            projectWriter.AddTemplate(solutionDirectory, new Templates.ProjectFiles.GitIgnoreTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.AppSettingsTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.TypeScriptLintTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.TypeScriptConfigTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.WebPackAdditionsTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.WebPackConfigTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.WebPackConfigVendorTemplate(project));
            projectWriter.AddTemplate(new Templates.ProjectFiles.WebConfigTemplate());
            projectWriter.AddTemplate(new Templates.ProjectFiles.NuGetconfigTemplate(project));

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
            projectWriter.AddTemplate(new Templates.ClientApp.App.AppComponentScssTemplate(project));
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
            #endregion

            #region Data Access Layer Project
            projectWriter.SetProjectDirectory(dalProjectDirectory);

            // Project Files
            projectWriter.AddTemplate(new Templates.DataAccessLayer.ProjectFiles.ProjectTemplate(project));
            projectWriter.AddTemplate(new Templates.DataAccessLayer.ProjectFiles.ApplicationDbContextTemplate(project));
            projectWriter.AddTemplate(new Templates.DataAccessLayer.ProjectFiles.ApplicationDbContextFactoryTemplate(project));

            // Entities
            projectWriter.AddTemplate(new Templates.DataAccessLayer.Entities.EntityTemplateBuilder(project));

            // Entity Type Config
            projectWriter.AddTemplate(new Templates.DataAccessLayer.EntityTypeConfigurations.EntityTypeConfigTemplateBuilder(project));

            errors = await projectWriter.WriteAndClean();

            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }
            #endregion

            if (builderSettings.CreateMigrations)
            {
                var dalProjectChanged = git.FolderChanged(repos[project.InternalName], $"{project.InternalName}.DataAccessLayer");

                if (dalProjectChanged)
                {
                    var migration = new Migrations.Migration(dalProjectDirectory);
                    var migrationResult = await migration.Migrate();
                    if (!migrationResult.Success)
                    {
                        return $"Migration Generation Failed: {migrationResult.Message}";
                    }
                }
            }

            if (builderSettings.GitPushOn)
            {
                var rtf = new SourceControl.RequestToFileSystem(topProjectDirectory, project);
                await rtf.Write();

                git.StageCommitPush(repos["Json"], "Build");
                git.StageCommitPush(repos[project.InternalName], "Build");
            }
            return "Success";
        }
    }
}
