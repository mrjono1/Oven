using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.app.components.navmenu
{
    public class NavmenuComponentTsTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", "navmenu")), "navmenu.component.ts");
        }

        public static string Evaluate(Project project)
        {
            return $@"import {{ Component }} from '@angular/core';

@Component({{
    selector: 'app-nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
}})
export class NavMenuComponent {{
    collapse: string = 'collapse';

    collapseNavbar(): void {{
        if (this.collapse.length > 1) {{
            this.collapse = '';
        }} else {{
            this.collapse = 'collapse';
        }}
    }}

    collapseMenu() {{
        this.collapse = 'collapse';
    }}
}}";
        }
    }
}