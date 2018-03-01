using System;
using System.Collections.Generic;
using MasterBuilder.Interfaces;
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
            var hasFormSection = false;

            foreach (var screenSection in Screen.ScreenSections)
            {
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionType.Form:
                        var formSection = new FormSectionBuilder(Project, Screen, screenSection);
                        sections.Add(formSection.Evaluate());
                        hasFormSection = true;
                        break;
                    case ScreenSectionType.Search:
                        var searchSection = new SearchSectionBuilder(Project, Screen, screenSection);
                        sections.Add(searchSection.Evaluate());
                        break;
                    case ScreenSectionType.MenuList:
                        var menuListSection = new MenuListSectionBuilder(Project, Screen, screenSection);
                        sections.Add(menuListSection.Evaluate());
                        break;
                    case ScreenSectionType.Html:
                        sections.Add($@"        <div class=""screen-section-html container mat-elevation-z2"" fxFlex>
{screenSection.Html}
        </div>");
                        break;
                    default:
                        break;
                }
            }

            var screenActionsPartial = new ScreenActionsPartial(Screen, hasFormSection);
            var screenActionsSection = screenActionsPartial.GetScreenActions();

            if (hasFormSection)
            {
                return $@"<form #formDir=""ngForm"" (ngSubmit)=""onSubmit()"" novalidate class=""screen"">{(string.IsNullOrEmpty(screenActionsSection) ? string.Empty : string.Concat(Environment.NewLine, screenActionsSection))}
    <div fxLayout=""column"" fxLayoutGap=""20px"" fxLayoutAlign=""start center"">
{string.Join(Environment.NewLine, sections)}
    </div>
</form>";
            }
            else
            {
                return $@"<div class=""screen"">{(string.IsNullOrEmpty(screenActionsSection) ? string.Empty : string.Concat(Environment.NewLine, screenActionsSection))}
    <div fxLayout=""column"" fxLayoutGap=""20px"" fxLayoutAlign=""start center"">
{string.Join(Environment.NewLine, sections)}
    </div>
</div>";
            }
        }
    }
}
