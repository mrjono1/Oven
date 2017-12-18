using MasterBuilder.Request;
using MasterBuilder.Templates.Controllers;
using MasterBuilder.Templates.Entities;
using MasterBuilder.Templates.Models;
using MasterBuilder.Templates.ProjectFiles;
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

            var fullBuild = true;

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
            }

            // Create Directories
            var binPath = FileHelper.CreateFolder(projectDirectory, "bin");
            var clientAppPath = FileHelper.CreateFolder(projectDirectory, "ClientApp");
            var controllersPath = FileHelper.CreateFolder(projectDirectory, "Controllers");
            var entitiesPath = FileHelper.CreateFolder(projectDirectory, "Entities");
            var extensionsPath = FileHelper.CreateFolder(projectDirectory, "Extensions");
            var entityTypeConfigsPath = FileHelper.CreateFolder(projectDirectory, "EntityTypeConfigurations");
            var modelsPath = FileHelper.CreateFolder(projectDirectory, "Models");
            var nodeModulesPath = FileHelper.CreateFolder(projectDirectory, "node_modules");
            var objPath = FileHelper.CreateFolder(projectDirectory, "obj");
            var propertiesPath = FileHelper.CreateFolder(projectDirectory, "Properties");
            var wwwrootPath = FileHelper.CreateFolder(projectDirectory, "wwwroot");

            if (fullBuild)
            {
                // Artifacts
                FileHelper.CopyFile("favicon.ico", Path.Combine(GetProjectRootFolder(), "CopyFiles"), wwwrootPath);
                FileHelper.CopyFolderContent(Path.Combine(GetProjectRootFolder(), "CopyFiles", "ClientApp"), clientAppPath);

                filesToWrite.AddRange(new Task[]
                {
                    // Create Solution File
                    FileHelper.WriteTemplate(projectDirectory, new Templates.SolutionTemplate(project)),
                    
                    // Create Project Files
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, AngularCliJsonTemplate.FileName()), AngularCliJsonTemplate.Evaluate(project)),
                    FileHelper.WriteTemplate(projectDirectory, new ProjectTemplate(project)),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, StartupTemplate.FileName()), StartupTemplate.Evaluate(project)),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, GitIgnoreTemplate.FileName()), GitIgnoreTemplate.Evaluate()),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, AppSettingsTemplate.FileName()), AppSettingsTemplate.Evaluate(project)),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, TypeScriptLintTemplate.FileName()), TypeScriptLintTemplate.Evaluate()),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, TypeScriptConfigTemplate.FileName()), TypeScriptConfigTemplate.Evaluate()),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, WebPackAdditionsTemplate.FileName()), WebPackAdditionsTemplate.Evaluate()),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, WebPackConfigTemplate.FileName()), WebPackConfigTemplate.Evaluate()),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, WebPackConfigVendorTemplate.FileName()), WebPackConfigVendorTemplate.Evaluate()),
                    FileHelper.WriteAllText(Path.Combine(projectDirectory, WebConfigTemplate.FileName()), WebConfigTemplate.Evaluate(project)),

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
                FileHelper.WriteAllText(Path.Combine(projectDirectory, PackageJsonTemplate.FileName()), PackageJsonTemplate.Evaluate(project)),
             
                // ClientApp/App
                FileHelper.WriteTemplate(projectDirectory, new Templates.ClientApp.App.AppModuleTemplate(project)),

                //
                
            });


            // ClientApp/app/models
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Models.ModelTemplateBuilder(project)));

            // ClientApp/app/shared
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Shared.ServiceTemplateBuilder(project)));

            // ClientApp/app/containers
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Containers.Ts.ContainerTsTemplateBuilder(project)));
            filesToWrite.AddRange(FileHelper.WriteTemplates(projectDirectory, new Templates.ClientApp.App.Containers.Html.ContainerHtmlTemplateBuilder(project)));

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
                        foreach (var screen in project.Screens)
                        {
                            if (screen.EntityId.HasValue && screen.EntityId.Value == item.Id)
                            {
                                foreach (var screenSection in screen.ScreenSections)
                                {
                                    switch (screenSection.ScreenSectionType)
                                    {
                                        case ScreenSectionTypeEnum.Form:
                                            filesToWrite.AddRange(
                                                new Task[] {
                                            // Server Side
                                            FileHelper.WriteAllText(ModelEditResponseTemplate.FileName(modelsPath, item, screen, screenSection), ModelEditResponseTemplate.Evaluate(project, item, screen, screenSection)),
                                            FileHelper.WriteAllText(ModelEditRequestTemplate.FileName(modelsPath, item, screen, screenSection), ModelEditRequestTemplate.Evaluate(project, item, screen, screenSection))
                                                });
                                            break;
                                        case ScreenSectionTypeEnum.Search:
                                            filesToWrite.AddRange(
                                                new Task[] {
                                            // Server Side
                                            FileHelper.WriteAllText(ModelSearchRequestTemplate.FileName(modelsPath, item, screen, screenSection), ModelSearchRequestTemplate.Evaluate(project, item, screen, screenSection)),
                                            FileHelper.WriteAllText(ModelSearchResponseTemplate.FileName(modelsPath, item, screen, screenSection), ModelSearchResponseTemplate.Evaluate(project, item, screen, screenSection)),
                                            FileHelper.WriteAllText(ModelSearchItemTemplate.FileName(modelsPath, item, screen, screenSection), ModelSearchItemTemplate.Evaluate(project, item, screen, screenSection))
                                                });
                                            break;
                                        case ScreenSectionTypeEnum.Grid:
                                            break;
                                        case ScreenSectionTypeEnum.Html:
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            // Client Side
                            filesToWrite.AddRange(
                            new Task[] {
                        FileHelper.WriteAllText(Templates.ClientApp.Components.Screen.ScreenComponentHtmlTemplate.FileName(clientAppPath, screen), Templates.ClientApp.Components.Screen.ScreenComponentHtmlTemplate.Evaluate(project, screen)),
                        FileHelper.WriteAllText(Templates.ClientApp.Components.Screen.ScreenComponentTsTemplate.FileName(clientAppPath, screen), Templates.ClientApp.Components.Screen.ScreenComponentTsTemplate.Evaluate(project, screen))
                            });
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
                }
            );

            await Task.WhenAll(filesToWrite.ToArray());
        }
    }
}
