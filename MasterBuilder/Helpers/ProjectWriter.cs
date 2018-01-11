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
        private List<Task<TemplateResult>> _filesToWrite;
        private string[] _cleanDirectoryIngnoreList;
        private string _projectDirectory;
        private string[] _filePaths;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectWriter(string projectDirectory, string[] cleanDirectoryIngnoreList)
        {
            _filesToWrite = new List<Task<TemplateResult>>();
            _projectDirectory = projectDirectory;
            _cleanDirectoryIngnoreList = cleanDirectoryIngnoreList;
        }

        /// <summary>
        /// Add a template
        /// </summary>
        internal void AddTemplate(params ITemplate[] templates)
        {
            foreach (var template in templates)
            {
                _filesToWrite.Add(FileHelper.WriteTemplate(_projectDirectory, template));
            }
        }

        /// <summary>
        /// Add a template from a custom directory
        /// </summary>
        public void AddTemplate(string customDirectory, params ITemplate[] templates)
        {
            foreach (var template in templates)
            {
                _filesToWrite.Add(FileHelper.WriteTemplate(customDirectory, template));
            }
        }

        /// <summary>
        /// Add templates from a template builder
        /// </summary>
        internal void AddTemplate(ITemplateBuilder templateBuilder)
        {
            foreach (var template in templateBuilder.GetTemplates())
            {
                _filesToWrite.Add(FileHelper.WriteTemplate(_projectDirectory, template));
            }
        }

        /// <summary>
        /// Final Step
        /// </summary>
        internal async Task<string> WriteAndClean()
        {
            var result = await Task.WhenAll(_filesToWrite.ToArray());

            if (result.Any(a => a.HasError))
            {
                return string.Join(Environment.NewLine, (from r in result
                                                         where r.HasError
                                                         select r.Error));
            }
            else
            {
                _filePaths = result.Select(a => a.FilePath).ToArray();

                DeleteFiles(_projectDirectory);
            }
            return null;
        }


        /// <summary>
        /// Delete files that were not written and not in the exclude list
        /// </summary>
        private void DeleteFiles(string directory)
        {
            var directoryInfo = new DirectoryInfo(directory);

            if (!_cleanDirectoryIngnoreList.Contains(directoryInfo.Name))
            {
                try
                {
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        if (!_filePaths.Contains(file.FullName))
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
