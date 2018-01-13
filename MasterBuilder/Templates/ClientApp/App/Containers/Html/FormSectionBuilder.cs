using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Html
{
    /// <summary>
    /// Form Section Builder
    /// </summary>
    public class FormSectionBuilder
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormSectionBuilder(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Evaluate
        /// </summary>
        public string Evaluate()
        {
            var formGroups = new List<string>();

            var entity = Project.Entities.SingleOrDefault(p => p.Id == ScreenSection.EntityId);

            foreach (var property in entity.Properties)
            {
                var formPropertyTemplate = new FormPropertyTemplate(Project, Screen, property);
                formGroups.Add(formPropertyTemplate.FormField());
            }

            var menuItems = new List<string>
            {
                @"<button type=""submit"" mat-raised-button>Submit</button>"
            };
            if (Screen.MenuItems != null)
            {
                foreach (var menuItem in Screen.MenuItems)
                {
                    menuItems.Add($@"<button mat-raised-button (click)=""{menuItem.InternalName.Camelize()}()"">{menuItem.Title}</button>");
                }
            }

            return $@"        <div class=""screen-section-form container mat-elevation-z2"" fxFlex>
            <form *ngIf=""{Screen.InternalName.Camelize()}"" [formGroup]=""{Screen.InternalName.Camelize()}Form"" #formDir=""ngForm"" (ngSubmit)=""onSubmit()"" novalidate fxLayout=""column"">
        <mat-toolbar class=""primary"">
            <mat-toolbar-row>
            <span>{ScreenSection.Title}</span>
{string.Join(Environment.NewLine, menuItems)}
            </mat-toolbar-row>
        </mat-toolbar>
{ string.Join(Environment.NewLine, formGroups)}
            </form>
        </div>";
        }
        }
}
