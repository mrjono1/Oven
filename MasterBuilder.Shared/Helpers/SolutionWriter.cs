using MasterBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MasterBuilder.Helpers
{
    /// <summary>
    /// Write a project to the file system
    /// </summary>
    public class SolutionWriter
    {
        private List<Task<TemplateResult>> FilesToWrite;
        private string[] CleanDirectoryIngnoreList;
        private string ProjectDirectory;
        private List<string> FilePaths;

        /// <summary>
        /// Constructor
        /// </summary>
        public SolutionWriter(string projectDirectory, string[] cleanDirectoryIngnoreList)
        {
            FilesToWrite = new List<Task<TemplateResult>>();
            ProjectDirectory = projectDirectory;
            CleanDirectoryIngnoreList = cleanDirectoryIngnoreList;
            FilePaths = new List<string>();
        }

        /// <summary>
        /// Change the set project directory
        /// </summary>
        /// <param name="projectDirectory">Project directory</param>
        public void SetProjectDirectory(string projectDirectory)
        {
            ProjectDirectory = projectDirectory;
        }

        /// <summary>
        /// Add a template
        /// </summary>
        public void AddTemplate(params ITemplate[] templates)
        {
            foreach (var template in templates)
            {
                FilesToWrite.Add(FileHelper.WriteTemplate(ProjectDirectory, template));
            }
        }

        /// <summary>
        /// Add a template from a custom directory
        /// </summary>
        public void AddTemplate(string customDirectory, params ITemplate[] templates)
        {
            foreach (var template in templates)
            {
                FilesToWrite.Add(FileHelper.WriteTemplate(customDirectory, template));
            }
        }

        /// <summary>
        /// Add templates from a template builder
        /// </summary>
        public void AddTemplate(ITemplateBuilder templateBuilder)
        {
            foreach (var template in templateBuilder.GetTemplates())
            {
                FilesToWrite.Add(FileHelper.WriteTemplate(ProjectDirectory, template));
            }
        }

        /// <summary>
        /// Final Step
        /// </summary>
        public async Task<string> WriteAndClean()
        {
            var result = await Task.WhenAll(FilesToWrite.ToArray());

            if (result.Any(a => a.HasError))
            {
                return string.Join(Environment.NewLine, (from r in result
                                                         where r.HasError
                                                         select r.Error));
            }
            else
            {
                FilePaths.AddRange(result.Select(a => a.FilePath));

                DeleteFiles(ProjectDirectory);
            }
            return null;
        }

        public void AddFolder(string[] sourceDirectory, string[] destinationDirectory)
        {
            var rootPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

            var directory = new DirectoryInfo(Path.Combine(rootPath, Path.Combine(sourceDirectory)));
            foreach (var file in directory.EnumerateFiles())
            {
                var destinationPath = FileHelper.CreateFolder(ProjectDirectory, destinationDirectory);
                var destinationFile = Path.Combine(destinationPath, file.Name);
                if (!new FileInfo(destinationFile).Exists)
                {
                    file.CopyTo(destinationFile);
                }
                FilePaths.Add(destinationFile);
            }
        }


        /// <summary>
        /// Delete files that were not written and not in the exclude list
        /// </summary>
        private void DeleteFiles(string directory)
        {
            var directoryInfo = new DirectoryInfo(directory);

            if (!CleanDirectoryIngnoreList.Contains(directoryInfo.Name))
            {
                try
                {
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        if (!FilePaths.Contains(file.FullName))
                        {
                            file.Delete();
                        }
                    }
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                    {
                        DeleteFiles(dir.FullName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
