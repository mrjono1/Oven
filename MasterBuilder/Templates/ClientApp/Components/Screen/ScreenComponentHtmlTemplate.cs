using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen
{
    public class ScreenComponentHtmlTemplate
    {
        public static string FileName(string folder, Request.Screen screen)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", screen.InternalName.ToLowerInvariant())), $"{screen.InternalName.ToLowerInvariant()}.component.html");
        }

        public static string Evaluate(Project project, Request.Screen screen)
        {

            string screenTypeContent = null;
            switch (screen.ScreenType)
            {
                case ScreenTypeEnum.Search:
                    screenTypeContent = ScreenTypeSearchHtmlTemplate.Evaluate(project, screen);
                    break;
                case ScreenTypeEnum.Edit:
                    screenTypeContent = ScreenTypeEditHtmlTemplate.Evaluate(project, screen);
                    break;
                case ScreenTypeEnum.View:
                    break;
                case ScreenTypeEnum.Html:
                    break;
                default:
                    screenTypeContent = "Template not found";
                    break;
            }

            return $@"<div class=""screen"">
    <h1>{screen.Title}</h1>
{screenTypeContent}
</div>";

        }
    }
}