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
            foreach (var screen in project.Screens) //.Where(s => s.ScreenType == ScreenTypeEnum.Search || s.ScreenType == ScreenTypeEnum.Edit))
            {
                declarations.Add($"{screen.InternalName}Component", $"import {{ {screen.InternalName}Component }} from './components/{screen.InternalName.ToCamlCase()}/{screen.InternalName.ToCamlCase()}.component';");

                if (!string.IsNullOrEmpty(screen.Path))
                {
                    if (screen.ScreenType == ScreenTypeEnum.Edit)
                    {
                        menuItems.Add($"            {{ path: '{screen.Path}/:id', component: {screen.InternalName}Component }}");

                        // TODO make optional
                        menuItems.Add($"            {{ path: '{screen.Path}', component: {screen.InternalName}Component }}");
                    }
                    else
                    {
                        menuItems.Add($"            {{ path: '{screen.Path}', component: {screen.InternalName}Component }}");
                    }
                }
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