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
    internal class ProjectWriter
    {
        private List<Task<TemplateResult>> FilesToWrite;
        private string[] CleanDirectoryIngnoreList;
        private string ProjectDirectory;
        private string[] FilePaths;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectWriter(string projectDirectory, string[] cleanDirectoryIngnoreList)
        {
            FilesToWrite = new List<Task<TemplateResult>>();
            ProjectDirectory = projectDirectory;
            CleanDirectoryIngnoreList = cleanDirectoryIngnoreList;
        }

        /// <summary>
        /// Add a template
        /// </summary>
        internal void AddTemplate(params ITemplate[] templates)
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
        internal void AddTemplate(ITemplateBuilder templateBuilder)
        {
            foreach (var template in templateBuilder.GetTemplates())
            {
                FilesToWrite.Add(FileHelper.WriteTemplate(ProjectDirectory, template));
            }
        }

        /// <summary>
        /// Final Step
        /// </summary>
        internal async Task<string> WriteAndClean()
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
                FilePaths = result.Select(a => a.FilePath).ToArray();

                DeleteFiles(ProjectDirectory);
            }
            return null;
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
