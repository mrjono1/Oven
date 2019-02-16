using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Oven.Templates.DataAccessLayer.Migrations
{
    /// <summary>
    /// Entity framework migrations
    /// </summary>
    public class Migration
    {
        private readonly string DalProjectPath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dalProjectPath"></param>
        public Migration(string dalProjectPath)
        {
            DalProjectPath = dalProjectPath;
        }

        /// <summary>
        /// Generate Database migrations
        /// </summary>
        internal async Task<CommandLineResult> Migrate()
        {
            var buildResult = await Build();
            if (!buildResult.Success)
            {
                return buildResult;
            }
            var list = await ListMigrations();

            return await AddMigration(list);
        }

        /// <summary>
        /// Build Project
        /// </summary>
        internal async Task<CommandLineResult> Build()
        {
            var result = await ExecuteCommand("dotnet", $"build {DalProjectPath}");

            if (result.ExitCode == 0)
            {
                result.Success = true;
            }
            return result;
        }

        /// <summary>
        /// List Migrations
        /// </summary>
        public async Task<IEnumerable<string>> ListMigrations()
        {
            var result = await ExecuteCommand("dotnet", "ef migrations list", DalProjectPath);

            if (result.ExitCode == 0)
            {
                if (string.IsNullOrEmpty(result.Message) || result.Message.StartsWith("No migrations were found.", StringComparison.OrdinalIgnoreCase))
                {
                    return new string[0];
                }
                else
                {
                    var migrations = new List<string>();
                    foreach (var line in result.Message.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var segments = line.Split("_");
                        if (segments.Length == 2)
                        {
                            migrations.Add(segments[1]);
                        }
                    }
                    return migrations;
                }
            }
            return new string[0];
        }
        
        /// <summary>
        /// Build Project
        /// </summary>
        internal async Task<CommandLineResult> AddMigration(IEnumerable<string> list)
        {
            var number = 1;
            if (list.Any())
            {
                var migrationNumbers = new List<int>();
                list.ToList().ForEach(a => migrationNumbers.Add(Convert.ToInt32(a)));
                number = migrationNumbers.Max() + 1;
            }

            var result = await ExecuteCommand("dotnet", $"ef migrations add {number}", DalProjectPath);

            if (result.ExitCode == 0)
            {
                result.Success = true;
            }
            return result;
        }

        /// <summary>
        /// Execute command line function
        /// </summary>
        /// <param name="fileName">.exe or alias name</param>
        /// <param name="command">command parameters</param>
        /// <param name="workingDirectory">Optional: working directory</param>
        private async Task<CommandLineResult> ExecuteCommand(string fileName, string command, string workingDirectory = null)
        {
            var proc = new Process();
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = command;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            var output = await proc.StandardOutput.ReadToEndAsync();

            proc.WaitForExit();
            var exitCode = proc.ExitCode;
            proc.Close();

            return new CommandLineResult
            {
                ExitCode = exitCode,
                Message = output
            };
        }
    }
}
