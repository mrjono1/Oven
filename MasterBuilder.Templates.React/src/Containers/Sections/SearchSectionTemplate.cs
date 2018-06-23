using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBuilder.Templates.React.src.Containers.Sections
{
    /// <summary>
    /// Search Section Template
    /// </summary>
    public class SearchSectionTemplate
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

        internal IEnumerable<string> Imports()
        {
            return new string[]{
                "import Table from '@material-ui/core/Table';",
                "import TableBody from '@material-ui/core/TableBody';",
                "import TableCell from '@material-ui/core/TableCell';",
                "import TableHead from '@material-ui/core/TableHead';",
                "import TableRow from '@material-ui/core/TableRow';",
                $"import * as {ScreenSection.Entity.InternalName.Camelize()}Actions from '../modules/{ScreenSection.Entity.InternalName.Camelize()}/actions';",
                "import { Link } from 'react-router-dom';"
            };
        }

        internal string Evaluate()
        {
            var columnHeaders = new List<string>();
            var columns = new List<string>();
            foreach (var searchColumn in ScreenSection.SearchSection.SearchColumns.OrderBy(_ => _.Ordinal))
            {
                if (searchColumn.PropertyType != PropertyType.PrimaryKey)
                {
                    columnHeaders.Add($@"            <TableCell>{searchColumn.TitleValue}</TableCell>");
                    columns.Add($@"                <TableCell>{{item.{searchColumn.InternalNameJavascript}}}</TableCell>");
                }
            }

            var navigateScreen = (from s in Project.Screens
                                  where s.Id == ScreenSection.NavigateToScreenId
                                  select s).SingleOrDefault();

            string route = string.Empty;
            if (navigateScreen != null)
            {
                route = $@" component={{Link}} to={{`/{navigateScreen.Path}/${{item.id}}`}}";
            }

            return $@"      <Table>
        <TableHead>
          <TableRow>
{string.Join(Environment.NewLine, columnHeaders)}
          </TableRow>
        </TableHead>
        <TableBody>
          {{{ScreenSection.Entity.InternalName.Camelize()}Items.map(item => {{
            return (
              <TableRow key={{item.id}}{route}>
{string.Join(Environment.NewLine, columns)}
              </TableRow>
            );
          }})}}
        </TableBody>
      </Table>";
        }

        internal IEnumerable<string> Props()
        {
            return new string[]
            {
                $"{ScreenSection.Entity.InternalName.Camelize()}Items"
            };
        }

        internal IEnumerable<string> ComponentWillMount()
        {
            return new string[]
            {
                $"    this.props.{ScreenSection.Entity.InternalName.Camelize()}Actions.fetchItemsIfNeeded();"
            };
        }

        internal IEnumerable<string> MapDispatchToProps()
        {
            return new string[]
            {
                $"{ScreenSection.Entity.InternalName.Camelize()}Actions: bindActionCreators({ScreenSection.Entity.InternalName.Camelize()}Actions, dispatch)"
            };
        }

        internal IEnumerable<string> MapStateToProps()
        {
            return new string[]
            {
                $"{ScreenSection.Entity.InternalName.Camelize()}Items: state.{ScreenSection.Entity.InternalNamePlural.Camelize()}.items"
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
        return (<tr>
      <td>
                <Link to={{`/client/${{this.props.clientKey}}`}}>{{this.props.name}} {{enabledBadge}}</Link>
      </td>
    </tr>);
    }}
}}

export default {ScreenSection.InternalName}Row;";
        }
    }
}