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

            foreach (var formField in (from ff in ScreenSection.FormSection.FormFields
                                       where !ff.IsHiddenFromUi
                                       select ff))
            { 
                var formPropertyTemplate = new FormFieldTemplate(Project, Screen, formField);
                formGroups.Add(formPropertyTemplate.GetFormField());
            }

            var visibile = ScreenSection.VisibilityExpression == null ? string.Empty : $@" *ngIf=""{ScreenSection.InternalName.Camelize()}ScreenSectionVisible()""";

            return $@"        <div class=""screen-section-form container mat-elevation-z2"" fxFlex{visibile}>
            <div *ngIf=""{Screen.InternalName.Camelize()}"" [formGroup]=""{Screen.InternalName.Camelize()}Form"" fxLayout=""column"">
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
