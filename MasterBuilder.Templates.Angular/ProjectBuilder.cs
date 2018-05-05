using MasterBuilder.Shared;
using System.Threading.Tasks;

namespace MasterBuilder.Templates.Angular
{
    /// <summary>
    /// Master Builder Angular Project
    /// </summary>
    public class ProjectBuilder
    {
        /// <summary>
        /// Build Master Builder Angular Project
        /// </summary>
        public async Task<string> RunAsync(BuilderSettings builderSettings, Request.Project project, Helpers.SolutionWriter solutionWriter, string solutionDirectory)
        {
            // Create Solution Directory
            var webProjectDirectory = FileHelper.CreateFolder(solutionDirectory, project.InternalName);

            var projectWriter = new Helpers.SolutionWriter(webProjectDirectory, project.CleanDirectoryIgnoreDirectories);
            
            // Create Directories
            var wwwrootPath = FileHelper.CreateFolder(webProjectDirectory, "wwwroot");

            // Artifacts
            projectWriter.AddFolder(new string[] { "CopyFiles", "assets", "favicons" }, new string[] { "wwwroot", "assets", "favicons" });
            projectWriter.AddTemplate(new Assets.Favicons.ManifestTemplate(project));

            // Create Project Files
            projectWriter.AddTemplate(new ProjectFiles.PackageJsonTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.StartupTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.AngularCliJsonTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.ProjectTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.AppSettingsTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.TypeScriptLintTemplate());
            projectWriter.AddTemplate(new ProjectFiles.TypeScriptConfigTemplate());
            projectWriter.AddTemplate(new ProjectFiles.WebPackAdditionsTemplate());
            projectWriter.AddTemplate(new ProjectFiles.WebPackConfigTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.WebPackConfigVendorTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.WebConfigTemplate());
            projectWriter.AddTemplate(new ProjectFiles.NuGetconfigTemplate(project));
                        
            // wwwroot/assets/i18n
            projectWriter.AddTemplate(new WwwRoot.i18n.LanguageTemplate());

            // ClientApp
            projectWriter.AddTemplate(new ClientApp.BootBrowserTsTemplate());
            if (project.ServerSideRendering)
            {
                projectWriter.AddTemplate(new ClientApp.BootServerTsTemplate());
            }

            // ClientApp/Test
            projectWriter.AddTemplate(new ClientApp.Test.BootTestTemplate());
            projectWriter.AddTemplate(new ClientApp.Test.KarmaConfigTemplate());
            projectWriter.AddTemplate(new ClientApp.Test.WebpackConfigTestTemplate());

            // ClientApp/Polyfills
            projectWriter.AddTemplate(new ClientApp.Polyfills.BrowserPolyfillsTemplate());
            projectWriter.AddTemplate(new ClientApp.Polyfills.PolyfillsTemplate());
            projectWriter.AddTemplate(new ClientApp.Polyfills.RxImportsTemplate());
            if (project.ServerSideRendering)
            {
                projectWriter.AddTemplate(new ClientApp.Polyfills.ServerPolyfillsTemplate());
            }

            // ClientApp/App
            projectWriter.AddTemplate(new ClientApp.App.AppModuleTemplate(project));
            projectWriter.AddTemplate(new ClientApp.App.AppComponentHtmlTemplate());
            projectWriter.AddTemplate(new ClientApp.App.AppComponentScssTemplate(project));
            projectWriter.AddTemplate(new ClientApp.App.AppComponentTsTemplate(project));
            projectWriter.AddTemplate(new ClientApp.App.AppModuleBrowserTemplate());
            if (project.ServerSideRendering)
            {
                projectWriter.AddTemplate(new ClientApp.App.AppModuleServerTemplate());
            }
            projectWriter.AddTemplate(new ClientApp.App.MaterialModuleTemplate(project));

            // ClientApp/App/Shared
            projectWriter.AddTemplate(new ClientApp.App.Shared.ServiceTemplateBuilder(project));
            
            // ClientApp/app/models
            projectWriter.AddTemplate(new ClientApp.App.Models.ModelTemplateBuilder(project));
            
            // ClientApp/app/containers
            projectWriter.AddTemplate(new ClientApp.App.Containers.Ts.ContainerTsTemplateBuilder(project));
            projectWriter.AddTemplate(new ClientApp.App.Containers.Html.ContainerHtmlTemplateBuilder(project));
            
            // Components
            projectWriter.AddTemplate(new ClientApp.app.components.navmenu.NavmenuComponentHtmlTemplate(project));
            projectWriter.AddTemplate(new ClientApp.app.components.navmenu.NavmenuComponentTsTemplate(project));
            
            var errors = await projectWriter.WriteAndClean();
            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }
            
            return null;
        }
    }
}
