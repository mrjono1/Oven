using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Edit
{
    public class ComponentHtmlTemplate
    {
        public static string FileName(string folder, Screen screen)
        {
            return Path.Combine(FileHelper.CreateFolder(folder, Path.Combine("app", "components", screen.InternalName.ToLowerInvariant())), $"{screen.InternalName.ToLowerInvariant()}.component.html");
        }

        public static string Evaluate(Project project, Entity entity, Screen screen)
        {
            var headings = new List<string>();
            var bindings = new List<string>();
            var formGroups = new List<string>();
            foreach (var property in entity.Properties)
            {
                headings.Add($"             <th>{property.Title}</th>");
                bindings.Add($"             <td>{{{{ {screen.InternalName.ToCamlCase()}Item.{property.InternalName.ToCamlCase()} }}}}</td>");

                switch (property.Type)
                {
                    case PropertyTypeEnum.Uniqueidentifier:
                        break;
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
            
            return $@"<div>
    <h1>{screen.Title}</h1>
    <form *ngIf=""{screen.InternalName.ToCamlCase()}"" [formGroup]=""{screen.InternalName.ToCamlCase()}Form"" #formDir=""ngForm"" (ngSubmit)=""onSubmit()"" novalidate>
{ string.Join(Environment.NewLine, formGroups)}

      <button type=""submit"" class=""btn btn-success"">Submit</button>
 
    </form>
</div>";

        }
    }
}