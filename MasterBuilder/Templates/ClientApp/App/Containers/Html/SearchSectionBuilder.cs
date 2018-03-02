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

            foreach (var column in ScreenSection.SearchSection.SearchColumns)
            {
                switch (column.PropertyType)
                {
                    case PropertyType.ParentRelationship:
                    case PropertyType.PrimaryKey:
                        continue;
                    default:
                        columns.Add($@"                <!-- {column.TitleValue} Column -->
                <ng-container matColumnDef=""{column.InternalNameCSharp.Camelize()}"">
                    <mat-header-cell *matHeaderCellDef> {column.TitleValue} </mat-header-cell>
                    <mat-cell *matCellDef=""let element""> {{{{element.{column.InternalNameCSharp.Camelize()}}}}} </mat-cell>
                </ng-container>");
                        break;
                }
            }

            string rowClickRouterLink = null;
            string newRouterLink = null;

            Request.Screen foundParentScreen = null;
            if (ScreenSection.NavigateToScreenId.HasValue)
            {
                var navigateScreen = (from s in Project.Screens
                                      where s.Id == ScreenSection.NavigateToScreenId
                                      select s).FirstOrDefault();


                var parentProperty = (from p in ScreenSection.SearchSection.Entity.Properties
                                      where p.PropertyType == PropertyType.ParentRelationship
                                      select p).SingleOrDefault();
                if (parentProperty != null)
                {
                    foundParentScreen = (from s in Project.Screens
                                         where s.EntityId == parentProperty.ParentEntityId &&
                                         s.ScreenType == ScreenType.Form
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
                        case MenuItemType.ApplicationLink:
                            var screenTo = Project.Screens.SingleOrDefault(s => s.Id == menuItem.ScreenId);
                            menuItems.Add($@"                <a mat-raised-button [routerLink]=""['{(foundParentScreen != null ? "." : string.Empty)}/{screenTo.Path}']"">
                        <span class='{menuItem.Icon}'></span> {menuItem.Title}
                     </a>");
                            break;
                        case MenuItemType.New:
                            break;
                        case MenuItemType.ServerFunction:
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
            var visibile = ScreenSection.VisibilityExpression == null ? string.Empty : $@" *ngIf=""{ScreenSection.InternalName.Camelize()}ScreenSectionVisible()""";

            return $@"        <mat-card class=""screen-section-search container"" fxFlex{visibile}>
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
