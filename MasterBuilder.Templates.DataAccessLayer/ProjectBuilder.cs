using MasterBuilder.Shared;
using System.Threading.Tasks;

namespace MasterBuilder.Templates.DataAccessLayer
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
            solutionWriter.AddTemplate(new ProjectFiles.ApplicationDbContextTemplate(project));
            solutionWriter.AddTemplate(new ProjectFiles.ApplicationDbContextFactoryTemplate(project));

            // Entities
            solutionWriter.AddTemplate(new Entities.EntityTemplateBuilder(project));

            // Entity Type Config
            solutionWriter.AddTemplate(new EntityTypeConfigurations.EntityTypeConfigTemplateBuilder(project));

            var errors = await solutionWriter.WriteAndClean();

            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            if (builderSettings.CreateMigrations)
            {
                var dalProjectChanged = git.FolderChanged(repository, $"{project.InternalName}.DataAccessLayer");

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
            
            return null;
        }
    }
}
