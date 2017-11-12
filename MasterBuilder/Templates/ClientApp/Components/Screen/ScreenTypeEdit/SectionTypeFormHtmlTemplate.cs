using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen.ScreenTypeEdit
{
    public class SectionTypeFormHtmlTemplate
    {
        public static string Evaluate(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            var formGroups = new List<string>();

            var entity = project.Entities.SingleOrDefault(p => p.Id == screenSection.EntityId);

            foreach (var property in entity.Properties)
            {
                switch (property.Type)
                {
                    case PropertyTypeEnum.String:
                        formGroups.Add($@"        <div class=""form-group"">
            <label for=""{property.InternalName.ToCamlCase()}"">{property.Title}</label>
            <input *ngIf=""{screen.InternalName.ToCamlCase()}"" type=""text"" class=""form-control"" id=""{property.Id}""
                formControlName=""{property.InternalName.ToCamlCase()}"" name=""{property.InternalName.ToCamlCase()}"" {(property.ValidationItems.Where(v => v.ValidationType == ValidationTypeEnum.Required).Any() ? "required" : "")}>
        </div>");
                        break;
                    case PropertyTypeEnum.Integer:
                        break;
                    case PropertyTypeEnum.DateTime:
                        break;
                    default:
                        break;
                }
            }

            return $@"<fieldset>
{ string.Join(Environment.NewLine, formGroups)}
</fieldset>";

        }
    }
}
