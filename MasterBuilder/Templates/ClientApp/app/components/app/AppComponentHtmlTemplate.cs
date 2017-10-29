using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.app.components.app
{
    public class AppComponentHtmlTemplate
    {
        public static string FileName(string folder)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", "app")), "app.component.html");
        }

        public static string Evaluate(Project project)
        {
            return $@"<div class='container-fluid'>
    <div class='row'>
        <div class='col-sm-3'>
            <nav-menu></nav-menu>
        </div>
        <div class='col-sm-9 body-content'>
            <router-outlet></router-outlet>
        </div>
    </div>
</div>";
        }
    }
}