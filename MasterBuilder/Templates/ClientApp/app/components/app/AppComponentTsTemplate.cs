using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.app.components.app
{
    public class AppComponentTsTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", "app")), "app.component.ts");
        }

        public static string Evaluate(Project project)
        {
            return $@"import {{ Component }} from '@angular/core';

@Component({{
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
}})
export class AppComponent {{
}}";
        }
    }
}