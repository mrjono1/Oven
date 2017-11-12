using MasterBuilder.Request;
using MasterBuilder.Templates;
using MasterBuilder.Templates.Controllers;
using MasterBuilder.Templates.Entities;
using MasterBuilder.Templates.Models;
using MasterBuilder.Templates.ProjectFiles;
using MasterBuilder.Templates.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            Run(outputDirectory).Wait();
        }

        public async Task Run(string outputDirectory) {
            var project = new Test().Project;

            if (!project.Validate(out string messages))
            {
                Console.WriteLine(messages);
                return;
            }

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


            // Artifacts
            FileHelper.CopyFile("favicon.ico", Path.Combine(GetProjectRootFolder(), "CopyFiles"), wwwrootPath);
            FileHelper.CopyFolderContent(Path.Combine(GetProjectRootFolder(), "CopyFiles", "ClientApp"), clientAppPath);

            var filesToWrite = new List<Task>
            {
                // Create Solution File
                FileHelper.WriteAllText(Path.Combine(solutionDirectory, SolutionTemplate.FileName(project)), SolutionTemplate.Evaluate(project)),

                // Create Project Files
                FileHelper.WriteAllText(Path.Combine(projectDirectory, ProjectTemplate.FileName(project)), ProjectTemplate.Evaluate(project)),
                FileHelper.WriteAllText(Path.Combine(projectDirectory, StartupTemplate.FileName()), StartupTemplate.Evaluate(project)),
                FileHelper.WriteAllText(Path.Combine(projectDirectory, GitIgnoreTemplate.FileName()), GitIgnoreTemplate.Evaluate()),
                FileHelper.WriteAllText(Path.Combine(projectDirectory, ProgramTemplate.FileName()), ProgramTemplate.Evaluate(project)),
                FileHelper.WriteAllText(Path.Combine(projectDirectory, AppSettingsTemplate.FileName()), AppSettingsTemplate.Evaluate(project)),
                FileHelper.WriteAllText(Path.Combine(projectDirectory, TypeScriptConfigTemplate.FileName()), TypeScriptConfigTemplate.Evaluate()),
                FileHelper.WriteAllText(Path.Combine(projectDirectory, WebPackConfigTemplate.FileName()), WebPackConfigTemplate.Evaluate()),
                FileHelper.WriteAllText(Path.Combine(projectDirectory, WebPackConfigVendorTemplate.FileName()), WebPackConfigVendorTemplate.Evaluate()),
                FileHelper.WriteAllText(Path.Combine(projectDirectory, PackageJsonTemplate.FileName()), PackageJsonTemplate.Evaluate(project)),

                // Generic Views & Controllers
                //FileHelper.WriteAllText(HomeControllerTemplate.FileName(controllersPath), HomeControllerTemplate.Evaluate(project)),
                FileHelper.WriteAllText(ViewImportsTemplate.FileName(viewsPath), ViewImportsTemplate.Evaluate(project)),
                FileHelper.WriteAllText(ViewStartTemplate.FileName(viewsPath), ViewStartTemplate.Evaluate()),
                FileHelper.WriteAllText(Templates.Views.Home.IndexTemplate.FileName(viewsPath), Templates.Views.Home.IndexTemplate.Evaluate(project)),
                FileHelper.WriteAllText(Templates.Views.Shared.ErrorTemplate.FileName(viewsPath), Templates.Views.Shared.ErrorTemplate.Evaluate(project)),
                FileHelper.WriteAllText(Templates.Views.Shared.LayoutTemplate.FileName(viewsPath), Templates.Views.Shared.LayoutTemplate.Evaluate(project))
            };

            if (project.Entities != null)
            {
                foreach (var item in project.Entities)
                {
                    // Create Entity & Map
                    filesToWrite.AddRange(
                        new Task[] {
                            FileHelper.WriteAllText(EntityTemplate.FileName(entitiesPath, item), EntityTemplate.Evaluate(project, item)),
                            FileHelper.WriteAllText(Templates.EntityTypeConfigurations.EntityTypeConfigTemplate.FileName(entityTypeConfigsPath, item), Templates.EntityTypeConfigurations.EntityTypeConfigTemplate.Evaluate(project, item)),

                            FileHelper.WriteAllText(ControllerTemplate.FileName(controllersPath, item, null), ControllerTemplate.Evaluate(project, item, null))
                    });
                    if (project.Screens != null)
                    {
                        foreach (var screen in project.Screens.Where(p => p.EntityId.HasValue && p.EntityId.Value == item.Id))
                        {
                            switch (screen.ScreenType)
                            {
                                case ScreenTypeEnum.Search:
                                    filesToWrite.AddRange(
                                        new Task[] {
                                            // Server Side
                                            FileHelper.WriteAllText(ModelSearchRequestTemplate.FileName(modelsPath, item, screen), ModelSearchRequestTemplate.Evaluate(project, item, screen)),
                                            FileHelper.WriteAllText(ModelSearchResponseTemplate.FileName(modelsPath, item, screen), ModelSearchResponseTemplate.Evaluate(project, item, screen)),
                                            FileHelper.WriteAllText(ModelSearchItemTemplate.FileName(modelsPath, item, screen), ModelSearchItemTemplate.Evaluate(project, item, screen)),

                                            // Client Side
                                            FileHelper.WriteAllText(Templates.ClientApp.Components.Screen.ScreenComponentHtmlTemplate.FileName(clientAppPath, screen), Templates.ClientApp.Components.Screen.ScreenComponentHtmlTemplate.Evaluate(project, screen)),
                                            FileHelper.WriteAllText(Templates.ClientApp.Components.Screen.ScreenComponentTsTemplate.FileName(clientAppPath, screen), Templates.ClientApp.Components.Screen.ScreenComponentTsTemplate.Evaluate(project, screen))

          //                                  FileHelper.WriteAllText(Templates.ClientApp.Components.Search.ComponentHtmlTemplate.FileName(clientAppPath, screen), Templates.ClientApp.Components.Search.ComponentHtmlTemplate.Evaluate(project, item, screen)),
            //                                FileHelper.WriteAllText(Templates.ClientApp.Components.Search.ComponentTsTemplate.FileName(clientAppPath, screen), Templates.ClientApp.Components.Search.ComponentTsTemplate.Evaluate(project, item, screen))
                                        });
                                    break;
                                case ScreenTypeEnum.Edit:
                                    filesToWrite.AddRange(
                                        new Task[] {
                                            // Server Side
                                            FileHelper.WriteAllText(ModelEditResponseTemplate.FileName(modelsPath, item, screen), ModelEditResponseTemplate.Evaluate(project, item, screen)),
                                            FileHelper.WriteAllText(ModelEditRequestTemplate.FileName(modelsPath, item, screen), ModelEditRequestTemplate.Evaluate(project, item, screen)),

                                            // Client Side
                                            FileHelper.WriteAllText(Templates.ClientApp.Components.Edit.ComponentHtmlTemplate.FileName(clientAppPath, screen), Templates.ClientApp.Components.Edit.ComponentHtmlTemplate.Evaluate(project, item, screen)),
                                            FileHelper.WriteAllText(Templates.ClientApp.Components.Edit.ComponentTsTemplate.FileName(clientAppPath, screen), Templates.ClientApp.Components.Edit.ComponentTsTemplate.Evaluate(project, item, screen))
                                        });
                                    //File.WriteAllText(ModelTemplate.FileName(modelsPath, item, screen), ModelTemplate.Evaluate(project, item, screen));
                                    //File.WriteAllText(ModelTemplate.FileName(modelsPath, item, screen), ModelTemplate.Evaluate(project, item, screen));
                                    break;
                                case ScreenTypeEnum.View:

                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                }
            }
            filesToWrite.Add(FileHelper.WriteAllText(EntityFrameworkContextTemplate.FileName(entitiesPath, project), EntityFrameworkContextTemplate.Evaluate(project)));


            if (project.Screens != null)
            {
                foreach (var screen in project.Screens.Where(p => !p.EntityId.HasValue))
                {
                    filesToWrite.AddRange(
                        new Task[] {
                            // Create Controller & Models for UI
                            FileHelper.WriteAllText(ControllerTemplate.FileName(controllersPath, null, screen), ControllerTemplate.Evaluate(project, null, screen)),
                            //File.WriteAllText(ModelTemplate.FileName(modelsPath, item), ModelTemplate.Evaluate(project, item));                            
                        }
                    );

                    if (!string.IsNullOrEmpty(screen.Html))
                    {
                        filesToWrite.Add(FileHelper.WriteAllText(Templates.ClientApp.Components.ComponentHtml.FileName(clientAppPath, screen), Templates.ClientApp.Components.ComponentHtml.Evaluate(project, screen)));
                    }
                }

                foreach (var screen in project.Screens)
                {
                  //  filesToWrite.Add(FileHelper.WriteAllText(Templates.ClientApp.Components.ComponentTs.FileName(clientAppPath, screen), Templates.ClientApp.Components.ComponentTs.Evaluate(project, screen)));
                }
            }

            // Client App

            filesToWrite.AddRange(
                new Task[] {

                    #region Nav menu

                    FileHelper.WriteAllText(Templates.ClientApp.app.components.navmenu.NavmenuComponentHtmlTemplate.FileName(clientAppPath), Templates.ClientApp.app.components.navmenu.NavmenuComponentHtmlTemplate.Evaluate(project)),
                    FileHelper.WriteAllText(Templates.ClientApp.app.components.navmenu.NavmenuComponentTsTemplate.FileName(clientAppPath), Templates.ClientApp.app.components.navmenu.NavmenuComponentTsTemplate.Evaluate(project)),
                    FileHelper.WriteAllText(Path.Combine(clientAppPath, "app", "components", "navmenu", "navmenu.component.css"), @"li .glyphicon {
    margin-right: 10px;
}

/* Highlighting rules for nav menu items */
li.link-active a,
li.link-active a:hover,
li.link-active a:focus {
    background-color: #4189C7;
    color: white;
}

/* Keep the nav menu independent of scrolling and on top of other items */
.main-nav {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    z-index: 1;
}

@media (min-width: 768px) {
    /* On small screens, convert the nav menu to a vertical sidebar */
    .main-nav {
        height: 100%;
        width: calc(25% - 20px);
    }
    .navbar {
        border-radius: 0;
        border-width: 0;
        height: 100%;
    }
    .navbar-header {
        float: none;
    }
    .navbar-collapse {
        border-top: 1px solid #444;
        padding: 0;
    }
    .navbar ul {
        float: none;
    }
    .navbar li {
        float: none;
        font-size: 15px;
        margin: 6px;
    }
    .navbar li a {
        padding: 10px 16px;
        border-radius: 4px;
    }
    .navbar a {
        /* If a menu item's text is too long, truncate it */
        width: 100%;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }
}"),
                    #endregion

                    // app
                    FileHelper.WriteAllText(Templates.ClientApp.app.components.app.AppComponentTsTemplate.FileName(clientAppPath), Templates.ClientApp.app.components.app.AppComponentTsTemplate.Evaluate(project)),
                    FileHelper.WriteAllText(Templates.ClientApp.app.components.app.AppComponentHtmlTemplate.FileName(clientAppPath), Templates.ClientApp.app.components.app.AppComponentHtmlTemplate.Evaluate(project)),
                    
                    // shared
                    FileHelper.WriteAllText(Templates.ClientApp.app.AppModuleSharedTsTemplate.FileName(clientAppPath), Templates.ClientApp.app.AppModuleSharedTsTemplate.Evaluate(project)),

                    // Client App
                    FileHelper.WriteAllText(Templates.ClientApp.BootBrowserTsTemplate.FileName(clientAppPath), Templates.ClientApp.BootBrowserTsTemplate.Evaluate(project)),
                    FileHelper.WriteAllText(Templates.ClientApp.BootServerTsTemplate.FileName(clientAppPath), Templates.ClientApp.BootServerTsTemplate.Evaluate(project))
                }
            );

            await Task.WhenAll(filesToWrite.ToArray());
        }
    }
}
