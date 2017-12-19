using MasterBuilder.Helpers;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App
{
    public class AppModuleTemplate : ITemplate
    {
        private readonly Project Project;

        public AppModuleTemplate(Project project)
        {
            Project = project;
        }

        public string GetFileName()
        {
            return "app.module.ts";
        }

        public string[] GetFilePath()
        {
            return new[] { "ClientApp", "app" };
        }
        public string GetFileContent()
        {
            // Routes
            var defaultScreen = Project.Screens.Where(s => s.Id == Project.DefaultScreenId).FirstOrDefault();
            var routes = new List<string>
            {
                $"            {{ path: '', redirectTo: '{defaultScreen.Path}', pathMatch: 'full' }}"
            };
            foreach (var screen in Project.Screens)
            {
                routes.AddRange(BuildRoutes(screen));
            }
            routes.Add($"            {{ path: '**', redirectTo: '{defaultScreen.Path}' }}");

            // Declarations
            var declarations = new Dictionary<string, string>
            {
                { "AppComponent", "import { AppComponent } from './app.component';"},
                { "NavMenuComponent", "import { NavMenuComponent } from './components/navmenu/navmenu.component';"}
            };
            foreach (var screen in Project.Screens)
            {
                declarations.Add($"{screen.InternalName}Component", $"import {{ {screen.InternalName}Component }} from './containers/{screen.InternalName.ToLowerInvariant()}/{screen.InternalName.ToLowerInvariant()}.component';");
            }

            // Providers
            var providers = new Dictionary<string, string>();
            foreach (var entity in Project.Entities)
            {
                providers.Add($"{entity.InternalName}Service", $"import {{ {entity.InternalName}Service }} from './shared/{entity.InternalName.ToLowerInvariant()}.service'");
            }
            
            return $@"import {{ NgModule, Inject }} from '@angular/core';
import {{ RouterModule, PreloadAllModules }} from '@angular/router';
import {{ CommonModule, APP_BASE_HREF }} from '@angular/common';
import {{ HttpModule, Http }} from '@angular/http';
import {{ HttpClientModule, HttpClient }} from '@angular/common/http';
import {{ FormsModule, ReactiveFormsModule }} from '@angular/forms';
import {{ BrowserModule, BrowserTransferStateModule }} from '@angular/platform-browser';
import {{ TransferHttpCacheModule }} from '@nguniversal/common';

import {{ Ng2BootstrapModule }} from 'ngx-bootstrap';

// i18n support
import {{ TranslateModule, TranslateLoader }} from '@ngx-translate/core';
import {{ TranslateHttpLoader }} from '@ngx-translate/http-loader';

{string.Join(Environment.NewLine, declarations.Values)}

import {{ LinkService }} from './shared/link.service';
{string.Join(Environment.NewLine, providers.Values)}
import {{ ORIGIN_URL }} from '@nguniversal/aspnetcore-engine';

export function createTranslateLoader(http: HttpClient, baseHref) {{
    // Temporary Azure hack
    if (baseHref === null && typeof window !== 'undefined') {{
        baseHref = window.location.origin;
    }}
    // i18n files are in `wwwroot/assets/`
    return new TranslateHttpLoader(http, `${{baseHref}}/assets/i18n/`, '.json');
}}

@NgModule({{
    declarations: [
        {string.Join(string.Concat(",", Environment.NewLine, "        "), declarations.Keys)}
    ],
    imports: [
        CommonModule,
        BrowserModule.withServerTransition({{
          appId: '{Project.Id.ToString().ToLowerInvariant()}' // make sure this matches with your Server NgModule
        }}),
        HttpClientModule,
        TransferHttpCacheModule,
        BrowserTransferStateModule,

        FormsModule,
        ReactiveFormsModule,
        Ng2BootstrapModule.forRoot(), // You could also split this up if you don't want the Entire Module imported

        // i18n support
        TranslateModule.forRoot({{
            loader: {{
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient, [ORIGIN_URL]]
            }}
        }}),

        // App Routing
        RouterModule.forRoot([
{string.Join(string.Concat(",", Environment.NewLine), routes)}
        ], {{
          // Router options
          useHash: false,
          preloadingStrategy: PreloadAllModules,
          initialNavigation: 'enabled'
        }})
    ],
    providers: [
        LinkService,
            {string.Join(string.Concat(",", Environment.NewLine, "        "), providers.Keys)},
        TranslateModule
    ],
    bootstrap: [AppComponent]
}})
export class AppModuleShared {{
}}";
        }

        public IEnumerable<string> BuildRoutes(Screen screen, Screen parentScreen = null)
        {
            // TODO: Add meta and links
            // *** SEO Magic ***
            // We're using "data" in our Routes to pass in our <title> <meta> <link> tag information
            // Note: This is only happening for ROOT level Routes, you'd have to add some additional logic if you wanted this for Child level routing
            // When you change Routes it will automatically append these to your document for you on the Server-side
            //  - check out app.component.ts to see how it's doing this
            /* 
             {
                path: 'ngx-bootstrap', component: NgxBootstrapComponent,
                data: {
                    title: 'Ngx-bootstrap demo!!',
                    meta: [{ name: 'description', content: 'This is an Demo Bootstrap page Description!' }],
                    links: [
                        { rel: 'canonical', href: 'http://blogs.example.com/bootstrap/something' },
                        { rel: 'alternate', hreflang: 'es', href: 'http://es.example.com/bootstrap-demo' }
                    ]
                }
            },
            */
            var menuItems = new List<string>();

            var fullEditPath = screen.EditFullPath(Project);
            if (!string.IsNullOrEmpty(fullEditPath))
            {
                menuItems.Add($@"            {{ path: '{fullEditPath}', component: {screen.InternalName}Component }}");
            }
            var fullPath = screen.FullPath(Project);
            if (!string.IsNullOrEmpty(fullPath))
            {
                menuItems.Add($"            {{ path: '{fullPath}', component: {screen.InternalName}Component }}");
            }

            return menuItems;
        }
    }
}
