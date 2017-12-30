using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Html
{
    public class FormSectionBuilder
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        public FormSectionBuilder(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

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
                switch (property.Type)
                {
                    case PropertyTypeEnum.String:
                        control = $@"{new String(' ', 20)}<label for=""{property.InternalName.Camelize()}"">{property.Title}</label>
{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""text"" class=""form-control"" id=""{property.Id}""
{new String(' ', 22)}formControlName=""{property.InternalName.Camelize()}"" name=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>";
                        break;
                    case PropertyTypeEnum.Integer:
                        control = $@"{new String(' ', 20)}<label for=""{property.InternalName.Camelize()}"">{property.Title}</label>
{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""number"" class=""form-control"" id=""{property.Id}""
{new String(' ', 22)}formControlName=""{property.InternalName.Camelize()}"" name=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>";
                        break;
                    case PropertyTypeEnum.Double:
                        control = $@"{new String(' ', 20)}<label for=""{property.InternalName.Camelize()}"">{property.Title}</label>
{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""number"" class=""form-control"" id=""{property.Id}""
{new String(' ', 22)}formControlName=""{property.InternalName.Camelize()}"" name=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>";
                        break;
                    case PropertyTypeEnum.DateTime:
                        control = $@"{new String(' ', 20)}<label for=""{property.InternalName.Camelize()}"">{property.Title}</label>
{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""datetime"" class=""form-control"" id=""{property.Id}""
{new String(' ', 22)}formControlName=""{property.InternalName.Camelize()}"" name=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>";
                        break;
                    case PropertyTypeEnum.Boolean:
                        control = $@"{new String(' ', 20)}<label>
{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""checkbox"" id=""{property.Id}""
{new String(' ', 22)}formControlName=""{property.InternalName.Camelize()}"" name=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>{property.Title}
{new String(' ', 20)}</label>";
                        break;
                    case PropertyTypeEnum.ReferenceRelationship:
                        control = $@"<label for=""{property.InternalName.Camelize()}"">{property.Title}</label>
<select *ngIf=""{Screen.InternalName.Camelize()}"" class=""form-control"" id=""{property.Id}""
formControlName=""{property.InternalName.Camelize()}"" name=""{property.InternalName.Camelize()}"" {(property.Required ? "required" : "")}>
<option *ngFor=""let option of {property.InternalName.Camelize()}Options"" [value]=""option.id"">{{{{option.title}}}}</option>
</select>";
                        break;
                    default:
                        break;
                }

                if (control != null)
                {
                    formGroups.Add($@"{new String(' ', 16)}<div class=""form-group"">
{string.Join(Environment.NewLine, control, validationDiv)}
{new String(' ', 16)}</div>");
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

            return $@"{new String(' ', 4)}<div class=""screen-type-edit"">
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
