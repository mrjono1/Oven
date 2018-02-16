using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MasterBuilder.Migrations
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
        /// Build Project
        /// </summary>
        public bool Build()
        {
            var result = ExecuteCommand("dotnet", $"build {DalProjectPath}");

            if (result.ExitCode == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Build Project
        /// </summary>
        public bool InitialCreate()
        {
            var result = ExecuteCommand("dotnet", "ef migrations add InitialCreate", DalProjectPath);

            if (result.ExitCode == 0)
            {
                return true;
            }
            return false;
        }

        private CommandLineResult ExecuteCommand(string fileName, string command, string workingDirectory = null)
        {
            var proc = new Process();
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = command;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            var output = proc.StandardOutput.ReadToEnd();

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
