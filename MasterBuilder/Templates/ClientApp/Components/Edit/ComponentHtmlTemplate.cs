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
                        formGroups.Add($@"<div class=""form-group"">
  <label for=""{property.Id}"">{property.Title}</label>
  <input *ngIf=""response"" type=""text"" class=""form-control"" id=""{property.Id}""
         {(property.ValidationItems.Where(v => v.ValidationType == ValidationTypeEnum.Required).Any() ? "required" : "")}
         [(ngModel)]=""response.{property.InternalName.ToCamlCase()}"" name=""{property.Id}"">
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
            
            return $@"<div class=""container"">
    <h1>Hero Form</h1>
    <form>
{string.Join(Environment.NewLine, formGroups)}

      <button type=""submit"" class=""btn btn-success"">Submit</button>
 
    </form>
</div>";

        }
    }
}