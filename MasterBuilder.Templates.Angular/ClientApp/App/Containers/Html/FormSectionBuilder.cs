using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.Angular.ClientApp.App.Containers.Html
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

            foreach (var formField in (from ff in ScreenSection.FormSection.FormFields
                                       where !ff.IsHiddenFromUi
                                       select ff))
            { 
                var formPropertyTemplate = new FormFieldTemplate(Project, Screen, ScreenSection, formField);
                formGroups.Add(formPropertyTemplate.GetFormField());
            }

            var visibile = ScreenSection.VisibilityExpression == null ? string.Empty : $@" *ngIf=""{ScreenSection.InternalName.Camelize()}ScreenSectionVisible()""";
            var formGroup = $"{Screen.InternalName.Camelize()}Form{(ScreenSection.EntityId == Screen.EntityId ? string.Empty : $".controls.{ScreenSection.Entity.InternalName.Camelize()}")}";

            return $@"        <div class=""screen-section-form container mat-elevation-z2"" fxFlex{visibile}>
            <div *ngIf=""{Screen.InternalName.Camelize()}"" [formGroup]=""{formGroup}"" fxLayout=""column"">
                <mat-toolbar class=""primary"">
                    <mat-toolbar-row>
                        <span>{ScreenSection.Title}</span>
                    </mat-toolbar-row>
                </mat-toolbar>
{string.Join(Environment.NewLine, formGroups)}
            </div>
        </div>";
        }
        }
}
