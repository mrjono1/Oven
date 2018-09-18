using Humanizer;
using Oven.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oven.Templates.React.Src.Containers.Sections
{
    /// <summary>
    /// Search Section Template
    /// </summary>
    public class SearchSectionTemplate : ISectionTemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;
        private readonly ScreenSection ScreenSection;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchSectionTemplate(Project project, Screen screen, ScreenSection screenSection)
        {
            Project = project;
            Screen = screen;
            ScreenSection = screenSection;
        }

        public IEnumerable<string> Imports()
        {
            return new string[]{
                "import Table from '@material-ui/core/Table';",
                "import TableBody from '@material-ui/core/TableBody';",
                "import TableCell from '@material-ui/core/TableCell';",
                "import TableHead from '@material-ui/core/TableHead';",
                "import TableRow from '@material-ui/core/TableRow';",
                "import { Link } from 'react-router-dom';",
                "import Button from '@material-ui/core/Button';",
                "import AddIcon from '@material-ui/icons/Add';"
            };
        }

        internal string Evaluate()
        {
            var columnHeaders = new List<string>();
            var columns = new List<string>();


            var navigateScreen = (from s in Project.Screens
                                  where s.Id == ScreenSection.NavigateToScreenId
                                  select s).SingleOrDefault();

            string route = null;
            string newButton = null;
            if (navigateScreen != null)
            {
                route = $@"`/{navigateScreen.Path}/${{item.id}}`";
                
                newButton = $@"<Button variant=""fab"" color=""primary"" aria-label=""add"" component={{Link}} to={{'/{navigateScreen.Path}/new'}}>
    <AddIcon/>
</Button>";
                
                // TODO: New item feature
                //if (ScreenSection.ParentScreenSection == null)
                //{
                //    var objectPath = GetParent(ScreenSection.ParentScreenSection, null);
                //    newRouterLink = $@" *ngIf=""{objectPath}"" [routerLink]=""['/{navigateScreen.Path}', {{ {parentProperty.InternalName.Camelize()}Id: {objectPath}.controls.id.value}}]""";
                //}
                //else if (parentProperty != null)
                //{
                //    newRouterLink = $@" *ngIf=""{parentProperty.InternalName.Camelize()}"" [routerLink]=""['/{navigateScreen.Path}', {{ {parentProperty.InternalName.Camelize()}Id: {parentProperty.InternalName.Camelize()}.id}}]""";
                //}
                //else
                //{
                //    newRouterLink = $@"[routerLink]=""['/{navigateScreen.Path}']""";
                //}
            }

            foreach (var searchColumn in ScreenSection.SearchSection.SearchColumns.OrderBy(_ => _.Ordinal))
            {
                if (searchColumn.PropertyType != PropertyType.PrimaryKey)
                {
                    columnHeaders.Add($@"            <TableCell>{searchColumn.TitleValue}</TableCell>");
                    if (route != null)
                    {
                        columns.Add($@"<TableCell>
    <Link to={{{route}}}>{{item.{searchColumn.InternalNameJavascript}}}</Link>
</TableCell>");
                    }
                    else
                    {
                        columns.Add($@"<TableCell>{{item.{searchColumn.InternalNameJavascript}}}</TableCell>");
                    }
                }
            }

            return $@"<Table>
    <TableHead>
        <TableRow>
{string.Join(Environment.NewLine, columnHeaders)}
        </TableRow>
    </TableHead>
    <TableBody>
        {{{ScreenSection.Entity.InternalName.Camelize()}Items.map(item => {{
            return (
                <TableRow key={{item.id}} hover>
{string.Join(Environment.NewLine, columns).IndentLines(20)}
                </TableRow>
            );
        }})}}
    </TableBody>
</Table>
{newButton}";
        }

        private string GetParent(ScreenSection screenSection, string objectPath)
        {
            var parentSection = screenSection.ParentScreenSection;

            var result = $"{screenSection.Entity.InternalName.Camelize()}";

            if (parentSection == null)
            {
                if (!string.IsNullOrEmpty(objectPath))
                {
                    result = $"{result}Form.controls.{objectPath}";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(objectPath))
                {
                    result = $"{result}.controls.{objectPath}";
                }

                result = GetParent(parentSection, result);
            }

            return result;
        }

        internal IEnumerable<string> Props()
        {
            return new string[]
            {
                $"{ScreenSection.Entity.InternalName.Camelize()}Items"
            };
        }

        internal IEnumerable<string> MapDispatchToProps()
        {
            return new string[]
            {
                $"{ScreenSection.Entity.InternalName.Camelize()}Actions: bindActionCreators(createEntityActions('{ScreenSection.Entity.InternalName.Camelize()}', '{ScreenSection.Entity.InternalNamePlural.Camelize()}', '{ScreenSection.Entity.InternalName.ToUpperInvariant()}'), dispatch)"
            };
        }

        internal IEnumerable<string> MapStateToProps()
        {
            return new string[]
            {
                $"{ScreenSection.Entity.InternalName.Camelize()}Items: state.{ScreenSection.Entity.InternalName.Camelize()}.all.items"
            };
        }

        private string GetRowTemplate()
        {
            //var columns = new List<string>();
            //foreach (var searchColumn in ScreenSection.SearchSection.SearchColumns)
            //{
            //    columns.Add($@"<td>{searchColumn.}</td>");
            //}
            return $@"import React from 'react';

class {ScreenSection.InternalName}Row extends React.Component {{
    render() {{
        return (
            <tr>
                <td>
                    <Link to={{`/client/${{this.props.clientKey}}`}}>{{this.props.name}} {{enabledBadge}}</Link>
                </td>
            </tr>
        );
    }}
}}

export default {ScreenSection.InternalName}Row;";
        }

        public IEnumerable<string> Constructor()
        {
            return new string[] 
            {
                $"        this.props.{ScreenSection.Entity.InternalName.Camelize()}Actions.fetchItemsIfNeeded();"
            };
        }

        public IEnumerable<string> Methods()
        {
            return new string[] { };
        }

        public IEnumerable<string> Functions()
        {
            return new string[] { };
        }
    }
}