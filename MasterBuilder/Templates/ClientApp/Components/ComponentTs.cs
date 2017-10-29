using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components
{
    public class ComponentTs
    {
        public static string FileName(string folder, Screen screen)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", screen.InternalName.ToLowerInvariant())), $"{screen.InternalName.ToLowerInvariant()}.component.ts");
        }

        public static string Evaluate(Project project, Screen screen)
        {
            return $@"import {{ Component }} from '@angular/core';

@Component({{
    selector: '{screen.InternalName.ToLowerInvariant()}',
    templateUrl: './{screen.InternalName.ToLowerInvariant()}.component.html'
}})
export class {screen.InternalName}Component {{
}}
";
        }
    }
}