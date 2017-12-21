using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MasterBuilder
{
    class Program
    {
        public string ProjectFilePath { get; }
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            new Program("C:\\Temp");
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
            Run(outputDirectory).Wait();
        }

        public async Task Run(string outputDirectory) {
            var project = new Test().Project;

            var fullBuild = false;

            // Validate & Pre Process Project
            if (!project.Validate(out string messages))
            {
                Console.WriteLine(messages);
                Console.Read();
                return;
            }
            var filesToWrite = new List<Task>();
            
            // Create Solution Directory
            var solutionDirectory = FileHelper.CreateFolder(outputDirectory, project.InternalName);
            var projectDirectory = FileHelper.CreateFolder(solutionDirectory, project.InternalName);

            if (fullBuild)
            {
                // Clean out directory
                FileHelper.CleanProject(projectDirectory, null, project);

                // Create Directories
                var clientAppPath = FileHelper.CreateFolder(projectDirectory, "ClientApp");
                var wwwrootPath = FileHelper.CreateFolder(projectDirectory, "wwwroot");

                // Artifacts
                FileHelper.CopyFile("favicon.ico", Path.Combine(GetProjectRootFolder(), "CopyFiles"), wwwrootPath);
                FileHelper.CopyFolderContent(Path.Combine(GetProjectRootFolder(), "CopyFiles", "ClientApp"), clientAppPath);

                filesToWrite.AddRange(new Task[]
                {
                    // Create Solution File
                    FileHelper.WriteTemplate(projectDirectory, new Templates.SolutionTemplate(project)),
                    
                    // Create Project Files
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.AngularCliJsonTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.ProjectTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.StartupTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.GitIgnoreTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.AppSettingsTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.TypeScriptLintTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.TypeScriptConfigTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.WebPackAdditionsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.WebPackConfigTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.WebPackConfigVendorTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.WebConfigTemplate()),

                    // Views
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.ViewImportsTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.ViewStartTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.Home.IndexTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.Shared.ErrorTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.Views.Shared.LayoutTemplate(project)),

                    FileHelper.WriteTemplate(projectDirectory, new Templates.Extensions.HttpRequestExtensionsTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.CoreModels.IRequestTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.CoreModels.TransferDataTemplate(project)),
                    
                    // ClientApp
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.BootBrowserTsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.BootServerTsTemplate()),

                    // ClientApp/Test
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Test.BootTestTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Test.KarmaConfigTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Test.WebpackConfigTest()),

                    // ClientApp/Polyfills
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Polyfills.BrowserPolyfillsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Polyfills.PolyfillsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Polyfills.RxImportsTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.Polyfills.ServerPolyfillsTemplate()),

                    // ClientApp/App
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppComponentHtmlTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppComponentScssTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppComponentTsTemplate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppModuleBrowserTemplate()),
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppModuleServerTemplate()),

                    // ClientApp/App/Shared
                    FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.Shared.LinkServiceTemplate()),

                    // wwwroot/assets/i18n
                    FileHelper.WriteTemplate(projectDirectory, new Templates.WwwRoot.i18n.LanguageTemplate()),
                });
            }


            filesToWrite.AddRange(new Task[]
            {
                // Create Project Files
                FileHelper.WriteTemplate(projectDirectory, new Templates.ProjectFiles.PackageJsonTemplate(project)),
             
                // ClientApp/App
                FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppModuleTemplate(project))
            });


            // ClientApp/app/models
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Models.ModelTemplateBuilder(project)));

            // ClientApp/app/shared
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Shared.ServiceTemplateBuilder(project)));

            // ClientApp/app/containers
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Containers.Ts.ContainerTsTemplateBuilder(project)));
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Containers.Html.ContainerHtmlTemplateBuilder(project)));

            // Entities
            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.Entities.EntityFrameworkContextTemplate(project)));
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Entities.EntityTemplateBuilder(project)));

            // Entity Type Config
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.EntityTypeConfigurations.EntityTypeConfigTemplateBuilder(project)));

            // Controllers
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Controllers.ControllerTemplateBuilder(project)));

            // Models
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.Models.ModelTemplateBuilder(project)));

            // Components

            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.app.components.navmenu.NavmenuComponentHtmlTemplate(project)));
            filesToWrite.Add(FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.app.components.navmenu.NavmenuComponentTsTemplate(project)));
         

            await Task.WhenAll(filesToWrite.ToArray());
        }
    }
}
