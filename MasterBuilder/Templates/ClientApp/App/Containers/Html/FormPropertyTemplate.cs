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
                case PropertyType.ReferenceRelationship:
                    propertyName = $"{Property.InternalName.Camelize()}Id";
                    break;
                default:
                    propertyName = Property.InternalName.Camelize();
                    break;
            }

            var attributes = new List<string>();
            var propertyValidators = new List<string>();
            if (Property.ValidationItems != null && Property.ValidationItems.Any())
            {
                foreach (var validationItem in Property.ValidationItems)
                {
                    string selector = null;
                    switch (validationItem.ValidationType)
                    {
                        case ValidationType.Required:
                            selector = "required";
                            attributes.Add("required");
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
                            if (Property.PropertyType == PropertyType.Integer)
                            {
                                attributes.Add($@"max=""{validationItem.IntegerValue}""");
                            }
                            else if (Property.PropertyType == PropertyType.Double)
                            {
                                attributes.Add($@"max=""{validationItem.DoubleValue}""");
                            }
                            break;
                        case ValidationType.MinimumValue:
                            selector = "min";
                            if (Property.PropertyType == PropertyType.Integer)
                            {
                                attributes.Add($@"min=""{validationItem.IntegerValue}""");
                            }
                            else if (Property.PropertyType == PropertyType.Double)
                            {
                                attributes.Add($@"min=""{validationItem.DoubleValue}""");
                            }
                            break;
                        case ValidationType.Unique:
                            break;
                        case ValidationType.Email:
                            selector = "email";
                            break;
                        case ValidationType.RequiredTrue:
                            selector = "requiredtrue";
                            break;
                        case ValidationType.Pattern:
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

            propertyValidators.Add($@"                    <mat-error>
                        {{{{serverErrorMessages.{propertyName}}}}}
                    </mat-error>");

            string control = null;
            string wrapAttributes = $@" *ngIf=""{Property.InternalName.Camelize()}Visible()""";
            bool dontWrap = false;
            switch (Property.PropertyType)
            {
                case PropertyType.String:
                    control = $@"{new String(' ', 20)}<input type=""text"" matInput id=""{Property.Id}"" placeholder=""{Property.Title}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>";
                    break;

                case PropertyType.Integer:
                    control = $@"{new String(' ', 20)}<input type=""number"" matInput id=""{Property.Id}"" placeholder=""{Property.Title}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>";
                    break;

                case PropertyType.Double:
                    control = $@"{new String(' ', 20)}<input type=""number"" matInput id=""{Property.Id}"" placeholder=""{Property.Title}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>";
                    break;

                case PropertyType.DateTime:
                    control = $@"{new String(' ', 20)}<input matInput [matDatepicker]=""{propertyName}Control"" id=""{Property.Id}"" placeholder=""{Property.Title}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>
                    <mat-datepicker-toggle matSuffix [for]=""{propertyName}Control""></mat-datepicker-toggle>
                    <mat-datepicker #{propertyName}Control></mat-datepicker>";
                    break;

                case PropertyType.Boolean:
                    dontWrap = true;
                    control = $@"{new String(' ', 16)}<mat-checkbox *ngIf=""{Property.InternalName.Camelize()}Visible()"" id=""{Property.Id}""
{new String(' ', 22)}[formControl]=""{propertyName}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>{Property.Title}</mat-checkbox>";
                    break;

                case PropertyType.ReferenceRelationship:
                    var parentEntity = (from e in Project.Entities
                                        where e.Id == Property.ParentEntityId.Value
                                        select e).SingleOrDefault();

                    control = $@"{new String(' ', 20)}<mat-select placeholder=""{Property.Title}"" [compareWith]=""referenceCompare"" [formControl]=""{propertyName}"" {(attributes.Any() ? string.Join(" ", attributes) : "")}>
                           {(Property.Required ? string.Empty : "<mat-option>--</mat-option>")}
                           <mat-option *ngFor=""let option of {parentEntity.InternalName.Camelize()}Reference.items"" [value]=""option.id"">
                                <span>{{{{ option.title }}}}</span>
                            </mat-option>
                        </mat-select>";

                    wrapAttributes = $@" *ngIf=""{parentEntity.InternalName.Camelize()}Reference && {parentEntity.InternalName.Camelize()}Reference.items && {Property.InternalName.Camelize()}Visible()""";
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
