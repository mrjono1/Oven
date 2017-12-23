using MasterBuilder.Request;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MasterBuilder.Helpers;
using System.Collections.Generic;

namespace MasterBuilder
{
    /// <summary>
    /// Functions for interacting with the file system
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Create a folder
        /// </summary>
        public static string CreateFolder(string basePath, params string[] folder)
        {
            var paths = new List<string>
            {
                basePath
            };
            paths.AddRange(folder);
            var path = Path.Combine(paths.ToArray());
            if (!System.IO.Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// Copy a file
        /// </summary>
        internal static void CopyFile(string fileName, string fromDirectory, string toDirectory)
        {
            var from = Path.Combine(fromDirectory, fileName);
            var to = Path.Combine(toDirectory, fileName);

            if (!File.Exists(to))
            {
                File.Copy(from, to);
            }
        }

        internal static void CopyFolderContent(string fromDirectory, string toDirectory)
        {
            if (!System.IO.Directory.Exists(toDirectory))
            {
                Directory.CreateDirectory(toDirectory);
            }

            try
            { 
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(fromDirectory, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(fromDirectory, toDirectory));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(fromDirectory, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(fromDirectory, toDirectory), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void CleanProject(string baseDirectory, string subDirectory, Project project)
        {
            if (!project.CleanDirectory)
            {
                return;
            }
            var source = new DirectoryInfo(baseDirectory);

            // Delete Folders each subdirectory using recursion.
            foreach (DirectoryInfo directory in source.GetDirectories())
            {
                var relativePath = TrimStart(directory.FullName, baseDirectory + "\\");
                if (!project.CleanDirectoryIgnoreDirectories.Contains(relativePath))
                {
                    try
                    {
                        foreach (FileInfo file in directory.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in directory.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        public static string TrimStart(string target, string trimString)
        {
            string result = target;
            while (result.StartsWith(trimString))
            {
                result = result.Substring(trimString.Length);
            }

            return result;
        }

        /// <summary>
        /// Write text to a file
        /// </summary>
        public static async Task WriteAllText(string path, string contents)
        {
            try
            {
                await File.WriteAllTextAsync(path, contents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Write templates to storage
        /// </summary>
        internal static IEnumerable<Task> WriteTemplates(string baseDirectory, ITemplateBuilder templateBuilder)
        {
            var tasks = new List<Task>();
            foreach (var template in templateBuilder.GetTemplates())
            {
                tasks.Add(WriteTemplate(baseDirectory, template));
            }
            return tasks;
        }

        /// <summary>
        /// Write a template to storage
        /// </summary>
        internal static Task WriteTemplate(string baseDirectory, ITemplate template)
        {
            var path = Path.Combine(CreateFolder(baseDirectory, template.GetFilePath()), template.GetFileName());

            string content = null;
            try
            {
                content = template.GetFileContent();
            }
            catch (Exception ex)
            {
                content = ex.ToString();
                Console.WriteLine(ex.ToString());
            }
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(path))
            {
                return null;
            }
            return WriteAllText(path, content);
        }
    }
}
