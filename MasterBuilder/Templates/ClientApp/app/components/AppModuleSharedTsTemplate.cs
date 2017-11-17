using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.app
{
    public class AppModuleSharedTsTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, "app"), "app.module.shared.ts");
        }

        public static IEnumerable<string> BuildRoutes(Project project, Screen screen, Screen parentScreen = null)
        {
            var menuItems = new List<string>();
            if (string.IsNullOrEmpty(screen.Path))
            {
                return menuItems;
            }

            if (screen.ScreenType != ScreenTypeEnum.Edit)
            {
                menuItems.Add($"            {{ path: '{screen.Path}', component: {screen.InternalName}Component }}");
                return menuItems;
            }

            Screen[] childScreens = null;

            var entity = (from e in project.Entities
                          where e.Id == screen.EntityId
                          select e).SingleOrDefault();
            if (entity != null)
            {
                // children
                if (screen.EntityId.HasValue)
                {
                    Guid[] childEntityIds = (from e in project.Entities
                                             where e.Id != entity.Id
                                             from p in e.Properties
                                             where p.Type == PropertyTypeEnum.ParentRelationship &&
                                             p.ParentEntityId == screen.EntityId
                                             select e.Id).ToArray();

                    if (childEntityIds != null && childEntityIds.Any())
                    {
                        childScreens = (from s in project.Screens
                                                 where s.Id != screen.Id &&
                                                 s.EntityId.HasValue &&
                                                 childEntityIds.Contains(s.EntityId.Value)
                                                 select s).ToArray();
                    }
                }
            }
            if (childScreens != null && childScreens.Any())
            {
                var childRoutes = new List<string>();
                foreach (var childScreen in childScreens)
                {
                    childRoutes.AddRange(AppModuleSharedTsTemplate.BuildRoutes(project, childScreen, screen));
                }

                menuItems.Add($@"            {{ path: '{screen.Path}/:id', component: {screen.InternalName}Component, 
                children: [
{string.Join(String.Concat(",", Environment.NewLine), childRoutes)}
                ] }}");
            }
            else
            {
                menuItems.Add($"            {{ path: '{screen.Path}/:id', component: {screen.InternalName}Component }}");
            }

            // TODO make optional
            menuItems.Add($"            {{ path: '{screen.Path}', component: {screen.InternalName}Component }}");
            
            
            return menuItems;
        }


        public static string Evaluate(Project project)
        {
            var defaultScreen = project.Screens.Where(s => s.Id == project.DefaultScreenId).FirstOrDefault();
            var menuItems = new List<string>
            {
                $"            {{ path: '', redirectTo: '{defaultScreen.Path}', pathMatch: 'full' }}"
            };


            var componentImports = new Dictionary<string, string>
            {
                { "CommonModule", "import { CommonModule } from '@angular/common';" },
                { "HttpModule", "import { HttpModule } from '@angular/http';" },
                { "FormsModule", "import { FormsModule } from '@angular/forms';" },
                { "ReactiveFormsModule", "import { ReactiveFormsModule } from '@angular/forms';" },
            };
            var declarations = new Dictionary<string, string>
            {
                { "AppComponent", "import { AppComponent } from './components/app/app.component';"},
                { "NavMenuComponent", "import { NavMenuComponent } from './components/navmenu/navmenu.component';"}
            };

            // Delarations
            foreach (var screen in project.Screens)
            {
                declarations.Add($"{screen.InternalName}Component", $"import {{ {screen.InternalName}Component }} from './components/{screen.InternalName.ToCamlCase()}/{screen.InternalName.ToCamlCase()}.component';");
            }

            foreach (var screen in project.Screens)
            {
                menuItems.AddRange(AppModuleSharedTsTemplate.BuildRoutes(project, screen));
            }

            menuItems.Add($"            {{ path: '**', redirectTo: '{defaultScreen.Path}' }}");

            return $@"import {{ NgModule }} from '@angular/core';
{string.Join(Environment.NewLine, componentImports.Values)}
import {{ RouterModule }} from '@angular/router';

{string.Join(Environment.NewLine, declarations.Values)}

@NgModule({{
    declarations: [
        {string.Join(string.Concat(",", Environment.NewLine, "        "), declarations.Keys)}
    ],
    imports: [
        {string.Join(string.Concat(",", Environment.NewLine, "        "), componentImports.Keys)},
        RouterModule.forRoot([
{string.Join(string.Concat(",", Environment.NewLine), menuItems)}
        ])
    ]
}})
export class AppModuleShared {{
}}
";
        }
    }
}