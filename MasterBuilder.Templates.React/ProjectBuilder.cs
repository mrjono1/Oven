using MasterBuilder.Shared;
using System.Threading.Tasks;

namespace MasterBuilder.Templates.React
{
    /// <summary>
    /// Master Builder React Project
    /// </summary>
    public class ProjectBuilder
    {
        /// <summary>
        /// Build Master Builder React Project
        /// </summary>
        public async Task<string> RunAsync(BuilderSettings builderSettings, Request.Project project, Helpers.SolutionWriter solutionWriter, string solutionDirectory)
        {
            // Create Solution Directory
            var webProjectDirectory = FileHelper.CreateFolder(solutionDirectory, project.InternalName);

            var projectWriter = new Helpers.SolutionWriter(webProjectDirectory, project.CleanDirectoryIgnoreDirectories);
            
            // Create Directories
            var wwwrootPath = FileHelper.CreateFolder(webProjectDirectory, "wwwroot");

            // Artifacts
            //projectWriter.AddFolder(new string[] { "CopyFiles", "assets", "favicons" }, new string[] { "wwwroot", "assets", "favicons" });

            // dot net project files
            projectWriter.AddTemplate(new ProjectFiles.StartupTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.ProjectTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.WebConfigTemplate());
            projectWriter.AddTemplate(new ProjectFiles.AppSettingsTemplate(project));

            // Create Project Files
            projectWriter.AddTemplate(new ProjectFiles.PackageJsonTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.PackageLockJsonTemplate(project));

            projectWriter.AddTemplate(new ProjectFiles.EslintrcTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.EslintrcIgnoreTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.BabelrcTemplate(project));

            projectWriter.AddTemplate(new Webpack.WebpackCommonTemplate());
            projectWriter.AddTemplate(new Webpack.WebpackProdTemplate(project));
            projectWriter.AddTemplate(new Webpack.WebpackDevTemplate(project));

            // Views
            projectWriter.AddTemplate(new Views.ViewImportsTemplate(project));
            projectWriter.AddTemplate(new Views.ViewStartTemplate());
            projectWriter.AddTemplate(new Views.Home.IndexTemplate(project));
            projectWriter.AddTemplate(new Views.Shared.ErrorTemplate(project));
            projectWriter.AddTemplate(new Views.Shared.LayoutTemplateTemplate(project));

            // Controller
            projectWriter.AddTemplate(new Controllers.HomeControllerTemplate(project));

            // src
            projectWriter.AddTemplate(new Src.ManifestTemplate(project));
            projectWriter.AddTemplate(new Src.IndexJsTemplate(project));

            // containers
            projectWriter.AddTemplate(new Src.Containers.ContainerBuilder(project));


            // core
            projectWriter.AddTemplate(new Src.Core.TypesTemplate(project));
            projectWriter.AddTemplate(new Src.Core.Actions.ActionsUITemplate(project));
            projectWriter.AddTemplate(new Src.Core.Reducers.IndexJsTemplate(project));
            projectWriter.AddTemplate(new Src.Core.Reducers.ReducerUiTemplate(project));
            projectWriter.AddTemplate(new Src.Core.Store.ConfigureStoreTemplate(project));

            //components
            projectWriter.AddTemplate(new Src.Components.AppBar.IndexJsTemplate(project));
            projectWriter.AddTemplate(new Src.Components.AppBar.StylesScssTemplate(project));

            projectWriter.AddTemplate(new Src.Components.Button.IndexJsTemplate(project));
            projectWriter.AddTemplate(new Src.Components.Button.StylesScssTemplate(project));

            projectWriter.AddTemplate(new Src.Components.TextField.IndexJsTemplate(project));
            projectWriter.AddTemplate(new Src.Components.TextField.StylesScssTemplate(project));

            var errors = await projectWriter.WriteAndClean();
            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }
            
            return null;
        }
    }
}
