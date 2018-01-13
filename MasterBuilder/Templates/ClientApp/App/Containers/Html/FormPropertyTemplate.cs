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
    public class FormPropertyTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly Property Property;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormPropertyTemplate(Project project, Screen screen, Property property)
        {
            Project = project;
            Screen = screen;
            Property = property;
        }

        internal string FormField()
        {
            string propertyName = null;
            switch (Property.PropertyType)
            {
                case PropertyTypeEnum.ReferenceRelationship:
                    propertyName = $"{Property.InternalName.Camelize()}Id";
                    break;
                default:
                    propertyName = Property.InternalName.Camelize();
                    break;
            }

            var propertyValidators = new List<string>();
            if (Property.ValidationItems != null && Property.ValidationItems.Any())
            {
                foreach (var validationItem in Property.ValidationItems)
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
                        propertyValidators.Add($@"{new String(' ', 20)}<mat-error *ngIf=""{propertyName}.hasError('{selector}')"">
{new String(' ', 24)}{validationItem.GetMessage(Property.Title)}
{new String(' ', 20)}</mat-error>");
                    }
                }
            }

            string control = null;
            string wrapAttributes = null;
            bool dontWrap = false;
            switch (Property.PropertyType)
            {
                case PropertyTypeEnum.String:
                    control = $@"{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""text"" matInput id=""{Property.Id}"" placeholder=""{Property.Title}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(Property.Required ? "required" : "")}>";
                    break;

                case PropertyTypeEnum.Integer:
                    control = $@"{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""number"" matInput id=""{Property.Id}"" placeholder=""{Property.Title}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(Property.Required ? "required" : "")}>";
                    break;

                case PropertyTypeEnum.Double:
                    control = $@"{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""number"" matInput id=""{Property.Id}"" placeholder=""{Property.Title}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(Property.Required ? "required" : "")}>";
                    break;

                case PropertyTypeEnum.DateTime:
                    control = $@"{new String(' ', 20)}<input *ngIf=""{Screen.InternalName.Camelize()}"" type=""datetime"" matInput id=""{Property.Id}"" placeholder=""{Property.Title}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(Property.Required ? "required" : "")}>";
                    break;

                case PropertyTypeEnum.Boolean:
                    dontWrap = true;
                    control = $@"{new String(' ', 16)}<mat-checkbox *ngIf=""{Screen.InternalName.Camelize()}"" id=""{Property.Id}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(Property.Required ? "required" : "")}>{Property.Title}</mat-checkbox>";
                    break;

                case PropertyTypeEnum.ReferenceRelationship:
                    var parentEntity = (from e in Project.Entities
                                        where e.Id == Property.ParentEntityId.Value
                                        select e).SingleOrDefault();

                    control = $@"{new String(' ', 20)}<mat-select placeholder=""{Property.Title}"" [compareWith]=""referenceCompare"" [formControl]=""{propertyName}"" {(Property.Required ? "required" : "")}>
                           {(Property.Required ? string.Empty : "<mat-option>--</mat-option>")}
                           <mat-option *ngFor=""let option of {parentEntity.InternalName.Camelize()}Reference.items"" [value]=""option.id"">
                                <span>{{{{ option.title }}}}</span>
                            </mat-option>
                        </mat-select>";

                    wrapAttributes = $@" *ngIf=""{parentEntity.InternalName.Camelize()}Reference && {parentEntity.InternalName.Camelize()}Reference.items""";
                    break;
                default:
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
