using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humanizer;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Html
{
    /// <summary>
    /// Form Property Template
    /// </summary>
    public class FormFieldTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly FormField FormField;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormFieldTemplate(Project project, Screen screen, ScreenSection screenSection, FormField formField)
        {
            Project = project;
            Screen = screen;
            FormField = formField;
            ScreenSection = screenSection;
        }

        internal string GetFormField()
        {
            var attributes = new List<string>();
            var propertyValidators = new List<string>();
            var formGroup = $"{Screen.InternalName.Camelize()}Form{(ScreenSection.EntityId == Screen.EntityId ? string.Empty : $".controls.{ScreenSection.Entity.InternalName.Camelize()}")}";

            if (FormField.Property.ValidationItems != null && FormField.Property.ValidationItems.Any())
            {
                foreach (var validationItem in FormField.Property.ValidationItems)
                {
                    string selector = null;
                    switch (validationItem.ValidationType)
                    {
                        case ValidationType.Required:
                            // Adding required validation at this level for boolean enforces true not enforcing not null
                            if (FormField.PropertyType != PropertyType.Boolean)
                            {
                                selector = "required";
                                attributes.Add("required");
                            }
                            break;

                        case ValidationType.RequiredTrue:
                            // This is only applicable to boolean
                            if (FormField.PropertyType == PropertyType.Boolean)
                            {
                                selector = "requiredtrue";
                                attributes.Add("required");
                            }
                            break;

                        case ValidationType.MaximumLength:
                            selector = "maxlength";
                            attributes.Add($@"maxlength=""{validationItem.IntegerValue}""");
                            break;

                        case ValidationType.MinimumLength:
                            selector = "minlength";
                            break;

                        case ValidationType.MaximumValue:
                            selector = "max";
                            if (FormField.PropertyType == PropertyType.Integer)
                            {
                                attributes.Add($@"max=""{validationItem.IntegerValue}""");
                            }
                            else if (FormField.PropertyType == PropertyType.Double)
                            {
                                attributes.Add($@"max=""{validationItem.DoubleValue}""");
                            }
                            break;

                        case ValidationType.MinimumValue:
                            selector = "min";
                            if (FormField.PropertyType == PropertyType.Integer)
                            {
                                attributes.Add($@"min=""{validationItem.IntegerValue}""");
                            }
                            else if (FormField.PropertyType == PropertyType.Double)
                            {
                                attributes.Add($@"min=""{validationItem.DoubleValue}""");
                            }
                            break;

                        case ValidationType.Unique:
                            break;

                        case ValidationType.Email:
                            selector = "email";
                            break;

                        case ValidationType.Pattern:
                            selector = "pattern";
                            break;

                        default:
                            break;
                    }
                    if (selector != null)
                    {
                        propertyValidators.Add($@"{new String(' ', 20)}<mat-error *ngIf=""{formGroup}.controls.{FormField.InternalNameTypeScript}.hasError('{selector}')"">
{new String(' ', 24)}{validationItem.GetMessage(FormField.TitleValue)}
{new String(' ', 20)}</mat-error>");
                    }
                }
            }

            propertyValidators.Add($@"                    <mat-error>
                        {{{{serverErrorMessages.{FormField.InternalNameTypeScript}}}}}
                    </mat-error>");

            string control = null;
            string wrapAttributes = $@" *ngIf=""{FormField.InternalNameTypeScript}Visible()""";
            bool dontWrap = false;
            switch (FormField.Property.PropertyType)
            {
                case PropertyType.String:
                    control = $@"{new String(' ', 20)}<input type=""text"" matInput id=""{FormField.Property.Id}"" placeholder=""{FormField.TitleValue}""
{new String(' ', 22)}formControlName=""{FormField.InternalNameTypeScript}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>";
                    break;

                case PropertyType.Integer:
                    control = $@"{new String(' ', 20)}<input type=""number"" matInput id=""{FormField.Property.Id}"" placeholder=""{FormField.TitleValue}""
{new String(' ', 22)}formControlName=""{FormField.InternalNameTypeScript}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>";
                    break;

                case PropertyType.Double:
                    control = $@"{new String(' ', 20)}<input type=""number"" matInput id=""{FormField.Property.Id}"" placeholder=""{FormField.TitleValue}""
{new String(' ', 22)}formControlName=""{FormField.InternalNameTypeScript}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>";
                    break;

                case PropertyType.DateTime:
                    control = $@"{new String(' ', 20)}<input matInput [matDatepicker]=""{FormField.InternalNameTypeScript}Control"" id=""{FormField.Property.Id}"" placeholder=""{FormField.TitleValue}""
{new String(' ', 22)}formControlName=""{FormField.InternalNameTypeScript}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>
                    <mat-datepicker-toggle matSuffix [for]=""{FormField.InternalNameTypeScript}Control""></mat-datepicker-toggle>
                    <mat-datepicker #{FormField.InternalNameTypeScript}Control></mat-datepicker>";
                    break;

                case PropertyType.Boolean:
                    dontWrap = true;
                    control = $@"{new String(' ', 16)}<mat-checkbox *ngIf=""{FormField.InternalNameTypeScript}Visible()"" id=""{FormField.Property.Id}""
{new String(' ', 22)}formControlName=""{FormField.InternalNameTypeScript}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>{FormField.TitleValue}</mat-checkbox>";
                    break;

                case PropertyType.ReferenceRelationship:
                    var parentEntity = (from e in Project.Entities
                                        where e.Id == FormField.Property.ParentEntityId.Value
                                        select e).SingleOrDefault();

                    control = $@"{new String(' ', 20)}<mat-select placeholder=""{FormField.TitleValue}"" [compareWith]=""referenceCompare"" formControlName=""{FormField.InternalNameTypeScript}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>
                           {(FormField.Property.Required ? string.Empty : "<mat-option>--</mat-option>")}
                           <mat-option *ngFor=""let option of {parentEntity.InternalName.Camelize()}Reference.items"" [value]=""option.id"">
                                <span>{{{{ option.title }}}}</span>
                            </mat-option>
                        </mat-select>";

                    wrapAttributes = $@" *ngIf=""{parentEntity.InternalName.Camelize()}Reference && {parentEntity.InternalName.Camelize()}Reference.items && {FormField.InternalNameTypeScript}Visible()""";
                    break;

                case PropertyType.Spatial:
                    control = $@"{new String(' ', 20)}<input type=""text"" matInput id=""{FormField.Property.Id}""
{new String(' ', 22)}formControlName=""{FormField.InternalNameTypeScript}"" {(attributes.Any() ? string.Join(" ", attributes) : "")} />
{new String(' ', 20)}<mangol></mangol>"; // *ngIf=""{FormField.InternalNameTypeScript}Visible()""
                    break;
            }

            if (control != null)
            {
                if (dontWrap)
                {
                    return $@"{control}{string.Join(Environment.NewLine, propertyValidators)}";
                }
                else
                {
                    return $@"{new String(' ', 16)}<mat-form-field{wrapAttributes}>
{control}
{string.Join(Environment.NewLine, propertyValidators)}
{new String(' ', 16)}</mat-form-field>";
                }
            }

            return string.Empty;
        }
    }
}
