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


            var errors = await solutionWriter.WriteAndClean();
            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            return null;
        }
    }
}