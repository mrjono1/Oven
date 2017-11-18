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
            var menuItems = new List<string>();

            var entity = project.Entities.SingleOrDefault(p => p.Id == screenSection.EntityId);

            foreach (var property in entity.Properties)
            {
                if (property.PropertyTemplate == PropertyTemplateEnum.PrimaryKey)
                {
                    continue;
                }
                else if (property.Type == PropertyTypeEnum.ParentRelationship)
                {
                    continue;
                }
                headings.Add($@"             <th data-property-id=""{property.Id}"" >{property.Title}</th>");
                bindings.Add($"             <td>{{{{ {screen.InternalName.ToCamlCase()}Item.{property.InternalName.ToCamlCase()} }}}}</td>");
            }

            string navigateToScreen = null;
            Request.Screen foundParentScreen = null;
            if (screenSection.NavigateToScreenId.HasValue)
            {
                var navigateScreen = (from s in project.Screens
                                        where s.Id == screenSection.NavigateToScreenId
                                        select s).FirstOrDefault();

                
                var parentProperty = (from p in entity.Properties
                                        where p.Type == PropertyTypeEnum.ParentRelationship
                                        select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    foundParentScreen = (from s in project.Screens
                                            where s.EntityId == parentProperty.ParentEntityId &&
                                            s.ScreenType == ScreenTypeEnum.Edit
                                            select s).SingleOrDefault();
                }

                if (navigateScreen != null)
                {
                    navigateToScreen = $@"[routerLink]=""['{(foundParentScreen != null ? "." : string.Empty)}/{navigateScreen.Path}', {screen.InternalName.ToCamlCase()}Item.id]""";
                }
            }

            if (screenSection.MenuItems != null)
            {
                foreach (var menuItem in screenSection.MenuItems)
                {
                    var screenTo = project.Screens.SingleOrDefault(s => s.Id == menuItem.ScreenId);
                    menuItems.Add($@"<a [routerLink]=""['{(foundParentScreen != null ? "." : string.Empty)}/{screenTo.Path}']"">
                        <span class='{menuItem.Icon}'></span> {menuItem.Title}
                     </a>");
                }
            }
            
            return $@"<div>
{string.Join(Environment.NewLine, menuItems)}
<p *ngIf=""!{screenSection.InternalName.ToCamlCase()}Response""><em>Loading...</em></p>

<table class='table' *ngIf=""{screenSection.InternalName.ToCamlCase()}Response && {screenSection.InternalName.ToCamlCase()}Response.items"">
    <thead>
        <tr>
{string.Join(Environment.NewLine, headings)}
        </tr>
    </thead>
    <tbody>
        <tr [attr.data-id]=""{screen.InternalName.ToCamlCase()}Item.id"" *ngFor=""let {screen.InternalName.ToCamlCase()}Item of {screenSection.InternalName.ToCamlCase()}Response.items"" {navigateToScreen}>
{string.Join(Environment.NewLine, bindings)}
        </tr>
    </tbody>
</table>
</div>";

        }
    }
}