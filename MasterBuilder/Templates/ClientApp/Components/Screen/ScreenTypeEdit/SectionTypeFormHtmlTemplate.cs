using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humanizer;

namespace MasterBuilder.Templates.ClientApp.Components.Screen.ScreenTypeEdit
{
    public class SectionTypeFormHtmlTemplate
    {
        public static string Evaluate(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            var formGroups = new List<string>();

            var entity = project.Entities.SingleOrDefault(p => p.Id == (screenSection.EntityId ?? screen.EntityId));

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
                            propertyValidators.Add($@"{new String(' ', 24)}<div *ngIf=""{property.InternalName.ToCamlCase()}.errors.{selector}"">
{new String(' ', 28)}{validationItem.GetMessage(property.Title)}
{new String(' ', 24)}</div>");
                        }
                    }

                    if (propertyValidators.Any())
                    {
                        validationDiv = $@"{new String(' ', 20)}<div *ngIf=""{property.InternalName.ToCamlCase()}.invalid && ({property.InternalName.ToCamlCase()}.dirty || {property.InternalName.ToCamlCase()}.touched)"" class=""alert alert-danger"">
{string.Join(Environment.NewLine, propertyValidators)}
{new String(' ', 20)}</div>";
                    }
                }

                string control = null;
                switch (property.Type)
                {
                    case PropertyTypeEnum.String:
                        control = $@"{new String(' ', 20)}<label for=""{property.InternalName.ToCamlCase()}"">{property.Title}</label>
{new String(' ', 20)}<input *ngIf=""{screen.InternalName.ToCamlCase()}"" type=""text"" class=""form-control"" id=""{property.Id}""
{new String(' ', 22)}formControlName=""{property.InternalName.ToCamlCase()}"" name=""{property.InternalName.ToCamlCase()}"" {(property.ValidationItems.Where(v => v.ValidationType == ValidationTypeEnum.Required).Any() ? "required" : "")}>";
                        break;
                    case PropertyTypeEnum.Integer:
                        break;
                    case PropertyTypeEnum.DateTime:
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

            return $@"{new String(' ', 12)}<fieldset>
{ string.Join(Environment.NewLine, formGroups)}
{new String(' ', 12)}</fieldset>";

        }
    }
}
