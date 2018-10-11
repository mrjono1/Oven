using Oven.Helpers;
using Oven.Shared;
using System;
using System.Threading.Tasks;

namespace Oven.Templates.Services
{
    /// <summary>
    /// Master Builder Api Shared Project
    /// </summary>
    public class ProjectBuilder
    {
        /// <summary>
        /// Build Master Builder Api Shared Project
        /// </summary>
        public async Task<string> RunAsync(BuilderSettings builderSettings, Request.Project project, SolutionWriter solutionWriter, string solutionDirectory, SourceControl.Git git, SourceControl.Models.GetRepository repository)
        {
            var apiProjectDirectory = FileHelper.CreateFolder(solutionDirectory, $"{project.InternalName}.Services");

            solutionWriter.SetProjectDirectory(apiProjectDirectory);

            // Create Project Files
            solutionWriter.AddTemplate(new ProjectFiles.ProjectTemplate(project));

            // Models
            solutionWriter.AddTemplate(new Models.ModelTemplateBuilder(project));

            // Models/Export
            solutionWriter.AddTemplate(new Models.Export.ModelTemplateBuilder(project));

            // Extensions
            solutionWriter.AddTemplate(new Extensions.NonDefaultAttributeTemplate(project));

            // Create Entity Service Interfaces
            foreach (var entity in project.Entities)
            {
                var service = new Entities.Contracts.EntityServiceTemplate(project, entity);
                if (service.HasEntityActions)
                {
                    solutionWriter.AddTemplate(service);
                }
            }

            // Create Entity Services
            foreach (var entity in project.Entities)
            {
                var service = new Entities.EntityServiceTemplate(project, entity);
                if (service.HasEntityActions)
                {
                    solutionWriter.AddTemplate(service);
                }
            }

            var errors = await solutionWriter.WriteAndClean();
            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            return null;
        }
    }
}