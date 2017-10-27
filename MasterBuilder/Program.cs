using MasterBuilder.Request;
using MasterBuilder.Templates;
using MasterBuilder.Templates.Controllers;
using MasterBuilder.Templates.Entities;
using MasterBuilder.Templates.Models;
using MasterBuilder.Templates.ProjectFiles;
using MasterBuilder.Templates.Views;
using System;
using System.IO;

namespace MasterBuilder
{
    class Program
    {
        public string ProjectFilePath { get; }
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            new Program("E:\\Temp\\Generated");
        }


        public static string GetProjectRootFolder()
        {
            return Path.GetFullPath($"{AppDomain.CurrentDomain.BaseDirectory}/../../../");
        }

        public string GetFileContent(string fileName)
        {
            return File.ReadAllText(fileName);
        }
        
        public Program(string outputDirectory)
        {
            var project = new Test().Project;

            // Validate & Pre Process Project

            // Create Solution Directory
            var solutionDirectory = FileHelper.CreateFolder(outputDirectory, project.InternalName);
            var projectDirectory = FileHelper.CreateFolder(solutionDirectory, project.InternalName);

            // Clean out directory
            FileHelper.CleanProject(projectDirectory, null, project);

            // Create Directories
            var binPath = FileHelper.CreateFolder(projectDirectory, "bin");
            var clientAppPath = FileHelper.CreateFolder(projectDirectory, "ClientApp");
            var controllersPath = FileHelper.CreateFolder(projectDirectory, "Controllers");
            var entitiesPath = FileHelper.CreateFolder(projectDirectory, "Entities");
            var entityTypeConfigsPath = FileHelper.CreateFolder(projectDirectory, "EntityTypeConfigurations");
            var modelsPath = FileHelper.CreateFolder(projectDirectory, "Models");
            var nodeModulesPath = FileHelper.CreateFolder(projectDirectory, "node_modules");
            var objPath = FileHelper.CreateFolder(projectDirectory, "obj");
            var propertiesPath = FileHelper.CreateFolder(projectDirectory, "Properties");
            var viewsPath = FileHelper.CreateFolder(projectDirectory, "Views");
            var wwwrootPath = FileHelper.CreateFolder(projectDirectory, "wwwroot");
            var distPath = FileHelper.CreateFolder(wwwrootPath, "dist");
            
            // Create Solution File
            File.WriteAllText(Path.Combine(solutionDirectory, SolutionTemplate.FileName(project)), SolutionTemplate.Evaluate(project));
            
            // Create Project Files
            File.WriteAllText(Path.Combine(projectDirectory, ProjectTemplate.FileName(project)), ProjectTemplate.Evaluate(project));
            File.WriteAllText(Path.Combine(projectDirectory, StartupTemplate.FileName()), StartupTemplate.Evaluate(project));
            File.WriteAllText(Path.Combine(projectDirectory, GitIgnoreTemplate.FileName()), GitIgnoreTemplate.Evaluate());
            File.WriteAllText(Path.Combine(projectDirectory, ProgramTemplate.FileName()), ProgramTemplate.Evaluate(project));
            File.WriteAllText(Path.Combine(projectDirectory, AppSettingsTemplate.FileName()), AppSettingsTemplate.Evaluate(project));
            File.WriteAllText(Path.Combine(projectDirectory, TypeScriptConfigTemplate.FileName()), TypeScriptConfigTemplate.Evaluate());
            File.WriteAllText(Path.Combine(projectDirectory, WebPackConfigTemplate.FileName()), WebPackConfigTemplate.Evaluate());
            File.WriteAllText(Path.Combine(projectDirectory, WebPackConfigVendorTemplate.FileName()), WebPackConfigVendorTemplate.Evaluate());
            File.WriteAllText(Path.Combine(projectDirectory, PackageJsonTemplate.FileName()), PackageJsonTemplate.Evaluate(project));

            // Generic Views & Controllers
            File.WriteAllText(HomeControllerTemplate.FileName(controllersPath), HomeControllerTemplate.Evaluate(project));
            File.WriteAllText(ViewImportsTemplate.FileName(viewsPath), ViewImportsTemplate.Evaluate(project));
            File.WriteAllText(ViewStartTemplate.FileName(viewsPath), ViewStartTemplate.Evaluate());
            File.WriteAllText(Templates.Views.Home.IndexTemplate.FileName(viewsPath), Templates.Views.Home.IndexTemplate.Evaluate(project));
            File.WriteAllText(Templates.Views.Shared.ErrorTemplate.FileName(viewsPath), Templates.Views.Shared.ErrorTemplate.Evaluate(project));
            File.WriteAllText(Templates.Views.Shared.LayoutTemplate.FileName(viewsPath), Templates.Views.Shared.LayoutTemplate.Evaluate(project));

            // Artifacts
            FileHelper.CopyFile("favicon.ico", Path.Combine(GetProjectRootFolder(), "CopyFiles"), wwwrootPath);
            FileHelper.CopyFolderContent(Path.Combine(GetProjectRootFolder(), "CopyFiles", "ClientApp"), clientAppPath);

            if (project.Entities != null)
            {
                foreach (var item in project.Entities)
                {
                    // Create Entity & Map
                    File.WriteAllText(EntityTemplate.FileName(entitiesPath, item), EntityTemplate.Evaluate(project, item));
                    File.WriteAllText(Templates.EntityTypeConfigurations.EntityTypeConfigTemplate.FileName(entityTypeConfigsPath, item), Templates.EntityTypeConfigurations.EntityTypeConfigTemplate.Evaluate(project, item));
                }
            }
            File.WriteAllText(EntityFrameworkContextTemplate.FileName(entitiesPath, project), EntityFrameworkContextTemplate.Evaluate(project));


            if (project.Screens != null)
            {
                foreach (var item in project.Screens)
                {
                    // Create Controller & Models for UI
                    File.WriteAllText(ControllerTemplate.FileName(controllersPath, item), ControllerTemplate.Evaluate(project, item));
                    File.WriteAllText(ModelTemplate.FileName(modelsPath, item), ModelTemplate.Evaluate(project, item));
                }
            }

            // Nav menu
            File.WriteAllText(Templates.ClientApp.app.components.navmenu.NavmenuComponentHtmlTemplate.FileName(clientAppPath), Templates.ClientApp.app.components.navmenu.NavmenuComponentHtmlTemplate.Evaluate(project));

        }
    }
}
