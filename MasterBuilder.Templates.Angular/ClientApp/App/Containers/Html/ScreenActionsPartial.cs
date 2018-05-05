using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Containers.Html
{
    /// <summary>
    /// Screen actions, like save and buttons
    /// </summary>
    public class ScreenActionsPartial
    {
        private readonly Screen Screen;
        private readonly bool HasFormSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScreenActionsPartial(Screen screen, bool hasFormSection)
        {
            Screen = screen;
            HasFormSection = hasFormSection;
        }

        internal string GetScreenActions()
        {
            var menuItems = new List<string>();
            if (HasFormSection)
            {
                menuItems.Add(@"            <button type=""submit"" mat-raised-button>Submit</button>");
            }
            if (Screen.MenuItems != null)
            {
                foreach (var menuItem in Screen.MenuItems)
                {
                    menuItems.Add($@"            <button mat-raised-button (click)=""{menuItem.InternalName.Camelize()}()"">{menuItem.Title}</button>");
                }
            }

            return $@"    <mat-toolbar class=""primary"">
        <mat-toolbar-row>
            <span>{Screen.Title}</span>
{string.Join(Environment.NewLine, menuItems)}
        </mat-toolbar-row>
    </mat-toolbar>";
        }
    }
}
