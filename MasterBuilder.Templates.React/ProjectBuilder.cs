using MasterBuilder.Helpers;
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
        public async Task<string> RunAsync(BuilderSettings builderSettings, Request.Project project, SolutionWriter solutionWriter, string solutionDirectory)
        {
            // Create Solution Directory
            var webProjectDirectory = FileHelper.CreateFolder(solutionDirectory, project.InternalName);

            var projectWriter = new SolutionWriter(webProjectDirectory, project.CleanDirectoryIgnoreDirectories);
            
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
            //projectWriter.AddTemplate(new ProjectFiles.PackageLockJsonTemplate(project));

            projectWriter.AddTemplate(new ProjectFiles.EslintrcTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.EslintrcIgnoreTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.BabelrcTemplate(project));

            projectWriter.AddTemplate(new ProjectFiles.Webpack.WebpackTemplate());
            projectWriter.AddTemplate(new ProjectFiles.Webpack.WebpackProductionTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.Webpack.WebpackDevelopmentTemplate(project));

            // Views
            projectWriter.AddTemplate(new Views.ViewImportsTemplate(project));
            projectWriter.AddTemplate(new Views.ViewStartTemplate());
            projectWriter.AddTemplate(new Views.Home.IndexTemplate(project));
            projectWriter.AddTemplate(new Views.Shared.ErrorTemplate(project));
            projectWriter.AddTemplate(new Views.Shared.LayoutTemplateTemplate(project));

            // Controller
            projectWriter.AddTemplate(new Controllers.HomeControllerTemplate(project));

            // src
            projectWriter.AddTemplate(new Src.SrcBuilder(project));

            // containers
           // projectWriter.AddTemplate(new Src.Containers.ContainerBuilder(project));

            // core
           // projectWriter.AddTemplate(new Src.Core.TypesTemplate(project));
           // projectWriter.AddTemplate(new Src.Core.Actions.ActionsUITemplate(project));
           // projectWriter.AddTemplate(new Src.Core.Reducers.ReducerUiTemplate(project));

            //components
            projectWriter.AddTemplate(new Src.Components.ComponentBuilder(project));

           // projectWriter.AddTemplate(new Src.Components.ComponentBuilder(project));

            // Reducers
         //   projectWriter.AddTemplate(new Src.Reducers.ReducerBuilder(project));

            // Store
         //   projectWriter.AddTemplate(new Src.Store.ConfigureStoreTemplate(project));
            
            // Actions
         //   projectWriter.AddTemplate(new Src.Actions.EntityActionsTemplate(project));

            projectWriter.AddTemplate(new Src.Resources.ResourceBuilder(project));

            var errors = await projectWriter.WriteAndClean();
            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }
            
            return null;
        }
    }
}
