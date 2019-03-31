using Oven.Helpers;
using Oven.Shared;
using System.Threading.Tasks;

namespace Oven.Templates.React
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

            // dot net project files
            projectWriter.AddTemplate(new ProjectFiles.ProjectTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.ProgramTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.StartupTemplate(project));
            // Not sure if this file is needed
            //projectWriter.AddTemplate(new ProjectFiles.WebConfigTemplate());
            projectWriter.AddTemplate(new ProjectFiles.AppSettingsTemplate(project));

            // Create Project Files
            projectWriter.AddTemplate(new ProjectFiles.TsConfigTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.TsLintTemplate(project));
            //projectWriter.AddTemplate(new ProjectFiles.PackageLockJsonTemplate(project));

            projectWriter.AddTemplate(new ProjectFiles.EslintrcTemplate(project));
            projectWriter.AddTemplate(new ProjectFiles.EslintrcIgnoreTemplate(project));
            //projectWriter.AddTemplate(new ProjectFiles.BabelrcTemplate(project));
            //projectWriter.AddTemplate(new ProjectFiles.WebpackTemplate(project));

            // Server Pages
            projectWriter.AddTemplate(new Pages.PagesBuilder(project));

            // Client App
            projectWriter.AddTemplate(new ClientApp.ClientAppBuilder(project));

            var errors = await projectWriter.WriteAndClean();
            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }
            
            return null;
        }
    }
}
