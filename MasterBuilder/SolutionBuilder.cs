using MasterBuilder.Shared;
using System.Threading.Tasks;

namespace MasterBuilder
{
    /// <summary>
    /// Master Builder Entry point
    /// </summary>
    public class SolutionBuilder
    {
        /// <summary>
        /// Run Master Builder
        /// </summary>
        public async Task<string> RunAsync(BuilderSettings builderSettings, Request.Project project, Helpers.SolutionWriter solutionWriter, string solutionDirectory)
        {
            // Create Solution File
            solutionWriter.AddTemplate(solutionDirectory, new Templates.SolutionFiles.SolutionTemplate(project));

            // Create Project Files
            solutionWriter.AddTemplate(solutionDirectory, new Templates.SolutionFiles.GitAttributesTemplate());
            solutionWriter.AddTemplate(solutionDirectory, new Templates.SolutionFiles.GitIgnoreTemplate(project));

            var errors = await solutionWriter.WriteAndClean(false);
            if (!string.IsNullOrEmpty(errors))
            {
                return errors;
            }

            return null;
        }
    }
}