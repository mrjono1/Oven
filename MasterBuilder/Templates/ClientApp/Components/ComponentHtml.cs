using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components
{
    public class ComponentHtml
    {
        public static string FileName(string folder, Screen screen)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", screen.InternalName.ToLowerInvariant())), $"{screen.InternalName.ToLowerInvariant()}.component.html");
        }

        public static string Evaluate(Project project, Screen screen)
        {
            if (screen.ScreenType == ScreenTypeEnum.Html)
            {
                return screen.Html;
            }
            else
            {
                return "<p>todo</p>";
            }
        }
    }
}