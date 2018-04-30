using MasterBuilder.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterBuilder.Templates.DataAccessLayer
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
        public async Task<string> Run(Request.Project project, Helpers.SolutionWriter solutionWriter, string solutionDirectory, SourceControl.Git git, SourceControl.Models.GetRepository repository)
        {                        
            var dalProjectDirectory = FileHelper.CreateFolder(solutionDirectory, $"{project.InternalName}.DataAccessLayer");

            var solutionWriter = new Helpers.SolutionWriter(dalProjectDirectory, project.CleanDirectoryIgnoreDirectories);
            
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
            
            return "Success";
        }
    }
}
