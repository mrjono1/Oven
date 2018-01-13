using System;
using System.Collections.Generic;
using MasterBuilder.Helpers;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Html
{
    /// <summary>
    /// Container html template
    /// </summary>
    public class ContainerHtmlTemplate: ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContainerHtmlTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Screen.InternalName.ToLowerInvariant()}.component.html";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "ClientApp", "app", "containers", Screen.InternalName.ToLowerInvariant() };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            var sections = new List<string>();

            foreach (var screenSection in Screen.ScreenSections)
            {
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionTypeEnum.Form:
                        var formSection = new FormSectionBuilder(Project, Screen, screenSection);
                        sections.Add(formSection.Evaluate());
                        break;
                    case ScreenSectionTypeEnum.Search:
                        var searchSection = new SearchSectionBuilder(Project, Screen, screenSection);
                        sections.Add(searchSection.Evaluate());
                        break;
                    case ScreenSectionTypeEnum.MenuList:
                        var menuListSection = new MenuListSectionBuilder(Project, Screen, screenSection);
                        sections.Add(menuListSection.Evaluate());
                        break;
                    case ScreenSectionTypeEnum.Html:
                        sections.Add($@"        <div class=""screen-section-html container mat-elevation-z6"" fxFlex>
{ screenSection.Html}
        </div>");
                        break;
                    default:
                        break;
                }
            }

            return $@" <div class=""screen"">
    <mat-toolbar class=""primary"">
        <mat-toolbar-row>
            <span>{Screen.Title}</span>
        </mat-toolbar-row>
    </mat-toolbar>
    <div fxLayout=""column"" fxLayoutGap=""20px"" fxLayoutAlign=""start center"">
{string.Join(Environment.NewLine, sections)}
    </div>
</div>";
        }
    }
}
