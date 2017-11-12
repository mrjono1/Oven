using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MasterBuilder.Templates.ClientApp.Components.Screen.ScreenTypeSearch
{
    public class SectionTypeSearchHtmlTemplate
    {
        public static string Evaluate(Project project, Request.Screen screen, ScreenSection screenSection)
        {
            var headings = new List<string>();
            var bindings = new List<string>();

            var entity = project.Entities.SingleOrDefault(p => p.Id == screenSection.EntityId);

            foreach (var property in entity.Properties)
            {
                if (property.PropertyTemplate == PropertyTemplateEnum.PrimaryKey)
                {
                    continue;
                }
                headings.Add($@"             <th data-property-id=""{property.Id}"" >{property.Title}</th>");
                bindings.Add($"             <td>{{{{ {screen.InternalName.ToCamlCase()}Item.{property.InternalName.ToCamlCase()} }}}}</td>");
            }

            var navigateToScreenPath = (from s in project.Screens
                                        where s.Id == screen.NavigateToScreenId
                                        select s.Path).FirstOrDefault();
            
            return $@"<div>
<p *ngIf=""!response""><em>Loading...</em></p>

<table class='table' *ngIf=""response && response.items"">
    <thead>
        <tr>
{string.Join(Environment.NewLine, headings)}
        </tr>
    </thead>
    <tbody>
        <tr [attr.data-id]=""{screen.InternalName.ToCamlCase()}Item.id"" *ngFor=""let {screen.InternalName.ToCamlCase()}Item of response.items""  [routerLink]=""['/{navigateToScreenPath}', {screen.InternalName.ToCamlCase()}Item.id]"">
{string.Join(Environment.NewLine, bindings)}
        </tr>
    </tbody>
</table>
</div>";

        }
    }
}