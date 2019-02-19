using Oven.Shared;
using System.Threading.Tasks;

namespace Oven.Templates.DataAccessLayer
{
    /// <summary>
    /// Build This Project
    /// </summary>
    public class ProjectBuilder
    {
        /// <summary>
        /// Run
        /// </summary>
        public async Task<string> RunAsync(BuilderSettings builderSettings, Request.Project project, Helpers.SolutionWriter solutionWriter, string solutionDirectory, SourceControl.Git git, SourceControl.Models.GetRepository repository)
        {                        
            var dalProjectDirectory = FileHelper.CreateFolder(solutionDirectory, $"{project.InternalName}.DataAccessLayer");

            solutionWriter.SetProjectDirectory(dalProjectDirectory);

            // Project Files
            solutionWriter.AddTemplate(new ProjectFiles.ProjectTemplate(project));

            // Entites
            solutionWriter.AddTemplate(new Entities.EntityTemplateBuilder(project));

            solutionWriter.AddTemplate(new ProjectFiles.ApplicationDbContextTemplate(project));
            solutionWriter.AddTemplate(new ProjectFiles.IApplicationDbContextTemplate(project));

            var errors = await solutionWriter.WriteAndClean();

            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            return null;
        }
    }
}
