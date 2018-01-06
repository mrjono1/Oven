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
                string validationDiv = null;
                if (property.ValidationItems != null && property.ValidationItems.Any())
                {
                    var propertyValidators = new List<string>();
                    foreach (var validationItem in property.ValidationItems)
                    {
                        string selector = null;
                        switch (validationItem.ValidationType)
                        {
                            case ValidationTypeEnum.Required:
                                selector = "required";
                                break;
                            case ValidationTypeEnum.MaximumLength:
                                selector = "maxlength";
                                break;
                            case ValidationTypeEnum.MinimumLength:
                                selector = "minlength";
                                break;
                            case ValidationTypeEnum.MaximumValue:
                                selector = "max";
                                break;
                            case ValidationTypeEnum.MinimumValue:
                                selector = "min";
                                break;
                            case ValidationTypeEnum.Unique:
                                break;
                            case ValidationTypeEnum.Email:
                                selector = "email";
                                break;
                            case ValidationTypeEnum.RequiredTrue:
                                selector = "requiredtrue";
                                break;
                            case ValidationTypeEnum.Pattern:
                                selector = "pattern";
                                break;
                            default:
                                break;
                        }
                        if (selector != null)
                        {
                            propertyValidators.Add($@"{new String(' ', 24)}<div *ngIf=""{property.InternalName.Camelize()}.errors.{selector}"">
{new String(' ', 28)}{validationItem.GetMessage(property.Title)}
{new String(' ', 24)}</div>");
                        }
                    }

                    if (propertyValidators.Any())
                    {
                        validationDiv = $@"{new String(' ', 20)}<div *ngIf=""{property.InternalName.Camelize()}.invalid && ({property.InternalName.Camelize()}.dirty || {property.InternalName.Camelize()}.touched)"" class=""alert alert-danger"">
{string.Join(Environment.NewLine, propertyValidators)}
{new String(' ', 20)}</div>";
                    }
                }

                string control = null;
                bool dontWrap = false;
                switch (property.Type)
                {
                    case PropertyTypeEnum.String:
                        control = $@"{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""text"" matInput id=""{property.Id}"" placeholder=""{property.Title}""
{new String(' ', 22)}[formControl]=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>";
                        break;

                    case PropertyTypeEnum.Integer:
                        control = $@"{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""number"" matInput id=""{property.Id}"" placeholder=""{property.Title}""
{new String(' ', 22)}[formControl]=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>";
                        break;

                    case PropertyTypeEnum.Double:
                        control = $@"{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""number"" matInput id=""{property.Id}"" placeholder=""{property.Title}""
{new String(' ', 22)}[formControl]=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>";
                        break;

                    case PropertyTypeEnum.DateTime:
                        control = $@"{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""datetime"" matInput id=""{property.Id}"" placeholder=""{property.Title}""
{new String(' ', 22)}[formControl]=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>";
                        break;

                    case PropertyTypeEnum.Boolean:
                        dontWrap = true;
                        control = $@"{new String(' ', 16)}<mat-checkbox *ngIf=""{Screen.InternalName.Camelize()}"" id=""{property.Id}""
{new String(' ', 22)}[formControl]=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>{property.Title}</mat-checkbox>";
                        break;
                        
                    case PropertyTypeEnum.ReferenceRelationship:
                        var parentEntity = (from e in Project.Entities
                                            where e.Id == property.ParentEntityId.Value
                                            select e).SingleOrDefault();

                        control = $@"{new String(' ', 20)}<mat-select placeholder=""{property.Title}"" [compareWith]=""referenceCompare"" [formControl]=""{property.InternalName.Camelize()}Id"" {(property.Required ? "required" : "")}>
                           {(property.Required ? string.Empty : "<mat-option>--</mat-option>")}
                           <mat-option *ngFor=""let option of {parentEntity.InternalName.Camelize()}Reference.items"" [value]=""option.id"">
                                <span>{{{{ option.title }}}}</span>
                            </mat-option>
                        </mat-select>";
                        break;
                    default:
                        break;
                }

                if (control != null)
                {
                    if (dontWrap)
                    {
                        formGroups.Add(string.Join(Environment.NewLine, control, validationDiv));
                    }
                    else
                    {
                        formGroups.Add($@"{new String(' ', 16)}<mat-form-field class=""example-full-width"">
{string.Join(Environment.NewLine, control, validationDiv)}
{new String(' ', 16)}</mat-form-field>");
                    }
                }
            }

            var menuItems = new List<string>
            {
                @"<button type=""submit"" class=""btn btn-success"">Submit</button>"
            };
            if (Screen.MenuItems != null)
            {
                foreach (var menuItem in Screen.MenuItems)
                {
                    menuItems.Add($@"<button type=""button"" class=""btn btn-primary"" (click)=""{menuItem.InternalName.Camelize()}()"">{menuItem.Title}</button>");
                }
            }

            return $@"{new String(' ', 4)}<div class=""screen-section-form"">
{new String(' ', 8)}<form *ngIf=""{Screen.InternalName.Camelize()}"" [formGroup]=""{Screen.InternalName.Camelize()}Form"" #formDir=""ngForm"" (ngSubmit)=""onSubmit()"" novalidate>
{new String(' ', 12)}<nav>
{new String(' ', 16)}{ string.Join(Environment.NewLine, menuItems)}
{new String(' ', 12)}</nav>
{new String(' ', 12)}<fieldset>
{ string.Join(Environment.NewLine, formGroups)}
{new String(' ', 12)}</fieldset>
{new String(' ', 8)}</form>
{new String(' ', 4)}</div>";
        }
        }
}
