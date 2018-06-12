using Humanizer;
using MasterBuilder.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                $"import * as {ScreenSection.Entity.InternalName.Camelize()}Actions from '../modules/{ScreenSection.Entity.InternalName.Camelize()}/actions';"
            };
        }

        internal string Evaluate()
        {
            var columns = new List<string>();
            foreach (var searchColumn in ScreenSection.SearchSection.SearchColumns.OrderBy(_ => _.Ordinal))
            {
                if (searchColumn.PropertyType != PropertyType.PrimaryKey)
                {
                    columns.Add($@"            <TableCell>{searchColumn.TitleValue}</TableCell>");
                }
            }
            return $@"      <Table>
        <TableHead>
          <TableRow>
{string.Join(Environment.NewLine, columns)}
          </TableRow>
        </TableHead>
        <TableBody>
        </TableBody>
      </Table>";
//            return $@"import React from 'react';
//import PropTypes from 'prop-types';
//import {{ withStyles }} from '@material-ui/core/styles';
//import Table from '@material-ui/core/Table';
//import TableBody from '@material-ui/core/TableBody';
//import TableCell from '@material-ui/core/TableCell';
//import TableHead from '@material-ui/core/TableHead';
//import TableRow from '@material-ui/core/TableRow';
//import Paper from '@material-ui/core/Paper';

//const styles = theme => ({{
//  root: {{
//    width: '100%',
//    marginTop: theme.spacing.unit * 3,
//    overflowX: 'auto',
//  }},
//  table: {{
//    minWidth: 700,
//  }},
//}});

//let id = 0;
//function createData(name, calories, fat, carbs, protein) {{
//  id += 1;
//  return {{ id, name, calories, fat, carbs, protein }};
//}}

//const data = [
//  createData('Frozen yoghurt', 159, 6.0, 24, 4.0),
//  createData('Ice cream sandwich', 237, 9.0, 37, 4.3),
//  createData('Eclair', 262, 16.0, 24, 6.0),
//  createData('Cupcake', 305, 3.7, 67, 4.3),
//  createData('Gingerbread', 356, 16.0, 49, 3.9),
//];

//function SimpleTable(props) {{
//  const {{ classes }} = props;

//  return (
//    <Paper className={{classes.root}}>
//      <Table className={{classes.table}}>
//        <TableHead>
//          <TableRow>
//            <TableCell>Dessert (100g serving)</TableCell>
//            <TableCell numeric>Calories</TableCell>
//            <TableCell numeric>Fat (g)</TableCell>
//            <TableCell numeric>Carbs (g)</TableCell>
//            <TableCell numeric>Protein (g)</TableCell>
//          </TableRow>
//        </TableHead>
//        <TableBody>
//          {{data.map(n => {{
//            return (
//              <TableRow key={{n.id}}>
//                <TableCell component=""th"" scope=""row"">
//                  {{n.name}}
//                </TableCell>
//                <TableCell numeric>{{n.calories}}</TableCell>
//                <TableCell numeric>{{n.fat}}</TableCell>
//                <TableCell numeric>{{n.carbs}}</TableCell>
//                <TableCell numeric>{{n.protein}}</TableCell>
//              </TableRow>
//            );
//          }})}}
//        </TableBody>
//      </Table>
//    </Paper>
//  );
//}}

//SimpleTable.propTypes = {{
//  classes: PropTypes.object.isRequired,
//}};

//export default withStyles(styles)(SimpleTable);";
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