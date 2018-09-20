using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Oven.Helpers;
using System.Collections.Generic;
using Oven.Interfaces;

namespace Oven
{
    /// <summary>
    /// Functions for interacting with the file system
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Is Directory Empty
        /// </summary>
        public static bool IsDirectoryEmpty(string path)
        {
            IEnumerable<string> items = Directory.EnumerateFileSystemEntries(path);
            using (IEnumerator<string> en = items.GetEnumerator())
            {
                return !en.MoveNext();
            }
        }

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
        
        private static void DeleteFiles(string directory, string[] excludeFolders)
        {
            var directoryInfo = new DirectoryInfo(directory);

            if (!excludeFolders.Contains(directoryInfo.Name))
            {
                try
                {
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                    {
                        DeleteFiles(dir.FullName, excludeFolders);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }


        /// <summary>
        /// Delete files in a directory
        /// </summary>
        public static void DeleteFilesInDirectory(string fileExtension, string directory, params string[] folder)
        {
            var paths = new List<string>
            {
                directory
            };
            paths.AddRange(folder);
            var path = Path.Combine(paths.ToArray());
            var info = new DirectoryInfo(path);
            if (info.Exists)
            {
                FileInfo[] files = null;
                if (string.IsNullOrEmpty(fileExtension))
                {
                    files = info.GetFiles();
                }
                else
                {
                    files = info.GetFiles($"*.{fileExtension}");
                }
                foreach (FileInfo file in files)
                {
                    file.Delete();
                }
            }
        }
        
        /// <summary>
        /// Write text to a file
        /// </summary>
        internal static async Task<TemplateResult> WriteAllText(string path, string contents)
        {
            var result = new TemplateResult
            {
                FilePath = path
            };
            try
            {
                await File.WriteAllTextAsync(path, contents);
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
                result.HasError = true;

                Console.WriteLine(ex.ToString());
            }
            return result;
        }
        
        /// <summary>
        /// Write a template to storage
        /// </summary>
        internal static async Task<TemplateResult> WriteTemplate(string baseDirectory, ITemplate template)
        {
            var result = new TemplateResult
            {
                FilePath = Path.Combine(CreateFolder(baseDirectory, template.GetFilePath()), template.GetFileName())
            };

            string content = null;
            try
            {
                content = template.GetFileContent();
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
                result.HasError = true;
                
                Console.WriteLine(ex.ToString());

                return result;
            }
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(result.FilePath))
            {
                // Skip if file is empty
            }
            else
            {
                result = await WriteAllText(result.FilePath, content);
            }

            return result;
        }
    }
}
