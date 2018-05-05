using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.Angular.ClientApp.app.components.navmenu
{
    /// <summary>
    /// navmenu component
    /// </summary>
    public class NavmenuComponentTsTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public NavmenuComponentTsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "navmenu.component.ts";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "components", "navmenu" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        /// <returns></returns>
        public string GetFileContent()
        {
            return $@"import {{ Component }} from '@angular/core';

@Component({{
    selector: 'app-nav-menu',
    templateUrl: './navmenu.component.html'
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