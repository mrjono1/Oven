using MasterBuilder.Request;
using System;
using System.IO;
using System.Linq;

namespace MasterBuilder
{
    public class FileHelper
    {
        public static string CreateFolder(string basePath, string folder)
        {
            var path = Path.Combine(basePath, folder);
            if (!System.IO.Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

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

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(fromDirectory, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(fromDirectory, toDirectory));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(fromDirectory, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(fromDirectory, toDirectory), true);
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
                        Console.ReadKey();
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
    }
}
