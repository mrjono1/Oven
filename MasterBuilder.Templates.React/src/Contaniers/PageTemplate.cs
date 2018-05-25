using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers
{
    /// <summary>
    /// Screen Template
    /// </summary>
    public class PageTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public PageTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Screen.InternalName}Page.tsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "containers" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"/**
 * {Screen.Title} Page
 */

import {{ StyleRulesCallback, Typography, WithStyles, withStyles }} from '@material-ui/core';
import * as React from 'react';
import {{ connect }} from 'react-redux';
import {{ RouteComponentProps }} from 'react-router';

export namespace {Screen.InternalName}Page {{
  export interface Props extends RouteComponentProps<void> {{
  }}
}}

class {Screen.InternalName}Page extends React.Component<WithStyles & {Screen.InternalName}Page.Props> {{

  render() {{
    return (
      <div className={{this.props.classes.root}}>
        <Typography variant=""display1"" gutterBottom>
          Home
        </Typography>
      </div>
    );
  }}
}}

const styles: StyleRulesCallback = theme => ({{
  root: {{
    textAlign: 'center',
    paddingTop: theme.spacing.unit * 20
  }},
}});

function mapStateToProps() {{
  return {{}};
}}

export default (withStyles(styles)<{{}}>(connect(mapStateToProps)({Screen.InternalName}Page)));";
        }
    }
}