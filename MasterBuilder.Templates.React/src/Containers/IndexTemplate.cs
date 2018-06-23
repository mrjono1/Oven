using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using MasterBuilder.Templates.React.src.Containers.Sections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MasterBuilder.Templates.React.Src.Containers
{
    /// <summary>
    /// index.js Template
    /// </summary>
    public class IndexTemplate : ITemplate
    {
        private readonly Project Project;
        private readonly Screen Screen;

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexTemplate(Project project, Screen screen)
        {
            Project = project;
            Screen = screen;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return $"{Screen.InternalName}Page.jsx";
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
            var sections = new List<string>();
            var imports = new List<string>();
            var mapDispatchToProps = new List<string>();
            var mapStateToProps = new List<string>();
            var componentWillMount = new List<string>();
            var render = new List<string>();
            var props = new List<string>
            {
                "classes"
            };
            //var hasFormSection = false;

            foreach (var screenSection in Screen.ScreenSections)
            {
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionType.Form:
                        var formSection = new FormSectionTemplate(Project, Screen, screenSection);
                        sections.Add(formSection.Body);
                        imports.AddRange(formSection.Imports());
                        componentWillMount.AddRange(formSection.ComponentWillMount());
                        mapStateToProps.AddRange(formSection.MapStateToProps());
                        mapDispatchToProps.AddRange(formSection.MapDispatchToProps());
                        props.AddRange(formSection.Props());
                        render.AddRange(formSection.Render());
                        break;
                    case ScreenSectionType.Search:
                        var searchSection = new SearchSectionTemplate(Project, Screen, screenSection);
                        imports.AddRange(searchSection.Imports());
                        sections.Add(searchSection.Evaluate());
                        componentWillMount.AddRange(searchSection.ComponentWillMount());
                        mapStateToProps.AddRange(searchSection.MapStateToProps());
                        mapDispatchToProps.AddRange(searchSection.MapDispatchToProps());
                        props.AddRange(searchSection.Props());
                        break;
                    case ScreenSectionType.MenuList:
                        var menuListSection = new MenuListSectionTemplate(Project, Screen, screenSection);
                        sections.Add(menuListSection.Evaluate());
                        imports.AddRange(menuListSection.Imports());
                        break;
                    case ScreenSectionType.Html:
                        var htmlSection = new HtmlSectionTemplate(Project, Screen, screenSection);
                        sections.Add(htmlSection.Evaluate());
                        break;
                    default:
                        break;
                }
            }

            //            var screenActionsPartial = new ScreenActionsPartial(Screen, hasFormSection);
            //            var screenActionsSection = screenActionsPartial.GetScreenActions();

            //            if (hasFormSection)
            //            {
            //                return $@"<form #formDir=""ngForm"" (ngSubmit)=""onSubmit()"" novalidate class=""screen"">{(string.IsNullOrEmpty(screenActionsSection) ? string.Empty : string.Concat(Environment.NewLine, screenActionsSection))}
            //    <div fxLayout=""column"" fxLayoutGap=""20px"" fxLayoutAlign=""start center"">
            //{string.Join(Environment.NewLine, sections)}
            //    </div>
            //</form>";
            //            }
            //            else
            //            {
            //                return $@"<div class=""screen"">{(string.IsNullOrEmpty(screenActionsSection) ? string.Empty : string.Concat(Environment.NewLine, screenActionsSection))}
            //    <div fxLayout=""column"" fxLayoutGap=""20px"" fxLayoutAlign=""start center"">
            //{string.Join(Environment.NewLine, sections)}
            //    </div>
            //</div>";
            //            }

            var gridSections = new List<string>();
            foreach (var section in sections)
            {
                gridSections.Add($@"    <Grid item xs={{12}}>
      <Paper className={{classes.paper}}>
{section}
      </Paper>
    </Grid>");
            }

            return $@"import React from 'react';
import {{ connect }} from 'react-redux';
import {{ bindActionCreators }} from 'redux';
import PropTypes from 'prop-types';
import {{ withStyles }} from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
{string.Join(Environment.NewLine, imports.Distinct())}

const styles = theme => ({{
    root: {{
        flexGrow: 1,
    }},
    paper: {{
        padding: theme.spacing.unit * 2,
        textAlign: 'center',
        color: theme.palette.text.secondary,
    }},
}});


class {Screen.InternalName}Page extends React.Component {{
    componentWillMount() {{
{string.Join(Environment.NewLine, componentWillMount.Distinct())}
    }}
    render() {{
        const {{ {string.Join(", ", props.Distinct())} }} = this.props;
{string.Join(Environment.NewLine, render.Distinct())}
    return (
<div className={{classes.root}}>
  <Grid container spacing={{24}}>
    <Grid item xs={{12}}>
      <Paper className={{classes.paper}}>Screen: {Screen.Title}</Paper>
    </Grid>
{string.Join(Environment.NewLine, gridSections)}
  </Grid>
</div>
    );
    }}
}}

{Screen.InternalName}Page.propTypes = {{
    classes: PropTypes.object.isRequired
}};

function mapStateToProps(state, ownProps) {{
    return {{
        {string.Join($",{Environment.NewLine}        ", mapStateToProps.Distinct())}
    }};
}}

function mapDispatchToProps(dispatch) {{
    return {{ 
        {string.Join($",{Environment.NewLine}        ", mapDispatchToProps.Distinct())}
    }};
}}

export default withStyles(styles)(connect(mapStateToProps, mapDispatchToProps)({Screen.InternalName}Page));";
        }
    }
}