using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.ClientApp.App.Containers.Html
{
    /// <summary>
    /// Search Section Builder
    /// </summary>
    public class SearchSectionBuilder
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchSectionBuilder(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        /// <summary>
        /// Evaluate
        /// </summary>
        public string Evaluate()
        {
            var menuItems = new List<string>();

            var columns = new List<string>();
            var entity = Project.Entities.SingleOrDefault(p => p.Id == ScreenSection.EntityId);

            foreach (var property in entity.Properties)
            {
                string columnName = null;
                if (property.PropertyType == PropertyTypeEnum.PrimaryKey)
                {
                    continue;
                }
                else if (property.PropertyType == PropertyTypeEnum.ParentRelationship)
                {
                    continue;
                }
                else if (property.PropertyType == PropertyTypeEnum.ReferenceRelationship)
                {
                    columnName = $"{property.InternalName.Camelize()}Title";
                }
                else
                {
                    columnName = property.InternalName.Camelize();
                }

                columns.Add($@"                <!-- {property.Title} Column -->
                <ng-container matColumnDef=""{columnName}"">
                    <mat-header-cell *matHeaderCellDef> {property.Title} </mat-header-cell>
                    <mat-cell *matCellDef=""let element""> {{{{element.{columnName}}}}} </mat-cell>
                </ng-container>");
            }

            string rowClickRouterLink = null;
            string newRouterLink = null;

            Request.Screen foundParentScreen = null;
            if (ScreenSection.NavigateToScreenId.HasValue)
            {
                var navigateScreen = (from s in Project.Screens
                                      where s.Id == ScreenSection.NavigateToScreenId
                                      select s).FirstOrDefault();


                var parentProperty = (from p in entity.Properties
                                      where p.PropertyType == PropertyTypeEnum.ParentRelationship
                                      select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    foundParentScreen = (from s in Project.Screens
                                         where s.EntityId == parentProperty.ParentEntityId &&
                                         s.ScreenType == ScreenTypeEnum.Edit
                                         select s).SingleOrDefault();
                }

                if (navigateScreen != null)
                {
                    rowClickRouterLink = $@"[routerLink]=""['{(foundParentScreen != null ? "." : string.Empty)}/{navigateScreen.Path}', row.id]""";
                    newRouterLink = $@"[routerLink]=""['{(foundParentScreen != null ? "." : string.Empty)}/{navigateScreen.Path}']""";
                }
            }

            if (ScreenSection.MenuItems != null)
            {
                foreach (var menuItem in ScreenSection.MenuItems)
                {
                    switch (menuItem.MenuItemType)
                    {
                        case MenuItemTypeEnum.ApplicationLink:
                            var screenTo = Project.Screens.SingleOrDefault(s => s.Id == menuItem.ScreenId);
                            menuItems.Add($@"                <a mat-raised-button [routerLink]=""['{(foundParentScreen != null ? "." : string.Empty)}/{screenTo.Path}']"">
                        <span class='{menuItem.Icon}'></span> {menuItem.Title}
                     </a>");
                            break;
                        case MenuItemTypeEnum.New:
                            break;
                        case MenuItemTypeEnum.ServerFunction:
                            menuItems.Add($@"                <button mat-raised-button (click)=""{menuItem.InternalName.Camelize()}()"">{menuItem.Title}</button>");
                            break;
                        default:
                            break;
                    }
                }
            }
            
            if (!string.IsNullOrWhiteSpace(newRouterLink))
            {
                menuItems.Add($@"                <a mat-raised-button {newRouterLink}>New</a>");
            }
            return $@"        <mat-card class=""screen-section-search container"" fxFlex>
            <mat-card-header>
                <mat-card-title>{ScreenSection.Title}</mat-card-title>
{string.Join(Environment.NewLine, menuItems)}
            </mat-card-header>
            <mat-card-content>
                <mat-table #table [dataSource]=""{ScreenSection.InternalName.Camelize()}DataSource"">
{string.Join(Environment.NewLine, columns)}
                    <mat-header-row *matHeaderRowDef=""{ScreenSection.InternalName.Camelize()}Columns""></mat-header-row>
                    <mat-row *matRowDef=""let row; columns: {ScreenSection.InternalName.Camelize()}Columns;"" {rowClickRouterLink}></mat-row>
                </mat-table>
            </mat-card-content>
        </mat-card>";
        }
        }
}
