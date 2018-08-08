using MasterBuilder.Interfaces;
using MasterBuilder.Request;
using System.Collections.Generic;
using System;
using System.Linq;
using MasterBuilder.Templates.React.Src.Containers.Sections;

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
            var sections = new List<ISectionTemplate>();
            var sectionBodys = new List<string>();
            var mapDispatchToProps = new List<string>();
            var mapStateToProps = new List<string>();
            var render = new List<string>();
            var props = new List<string>
            {
                "classes"
            };

            foreach (var screenSection in Screen.ScreenSections)
            {
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionType.Form:
                        var formSection = new FormSectionTemplate(Project, Screen, screenSection);
                        sections.Add(formSection);

                        sectionBodys.Add(formSection.Body);
                        mapStateToProps.AddRange(formSection.MapStateToProps());
                        mapDispatchToProps.AddRange(formSection.MapDispatchToProps());
                        props.AddRange(formSection.Props());
                        render.AddRange(formSection.Render());
                        break;

                    case ScreenSectionType.Search:
                        var searchSection = new SearchSectionTemplate(Project, Screen, screenSection);
                        sections.Add(searchSection);

                        sectionBodys.Add(searchSection.Evaluate());
                        mapStateToProps.AddRange(searchSection.MapStateToProps());
                        mapDispatchToProps.AddRange(searchSection.MapDispatchToProps());
                        props.AddRange(searchSection.Props());
                        break;

                    case ScreenSectionType.MenuList:
                        var menuListSection = new MenuListSectionTemplate(Project, Screen, screenSection);
                        sections.Add(menuListSection);

                        sectionBodys.Add(menuListSection.Evaluate());
                        break;

                    case ScreenSectionType.Html:
                        var htmlSection = new HtmlSectionTemplate(Project, Screen, screenSection);
                        sections.Add(htmlSection);

                        sectionBodys.Add(htmlSection.Evaluate());
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
            foreach (var section in sectionBodys)
            {
                gridSections.Add($@"<Grid item xs={{12}}>
    <Paper className={{classes.paper}}>
{section.IndentLines(8)}
    </Paper>
</Grid>");
            }

            var constructorExpressions = new List<string>();
            var imports = new List<string>();
            var methods = new List<string>();
            foreach (var section in sections)
            {
                constructorExpressions.AddRange(section.Constructor());
                imports.AddRange(section.Imports());
                methods.AddRange(section.Methods());
            }

            return $@"import React from 'react';
import {{ connect }} from 'react-redux';
import {{ bindActionCreators }} from 'redux';
import PropTypes from 'prop-types';
import createEntityActions from '../actions/entityActions';
import {{ withStyles }} from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
{string.Join(Environment.NewLine, imports.Distinct().OrderBy(a => a))}

const styles = theme => ({{
    root: {{
        flexGrow: 1
    }},
    paper: {{
        padding: theme.spacing.unit * 2,
        textAlign: 'center',
        color: theme.palette.text.secondary
    }}
}});


class {Screen.InternalName}Page extends React.Component {{
    constructor(props) {{
        super(props);
{string.Join(Environment.NewLine, constructorExpressions.Distinct().OrderBy(a => a))}
    }}
    render() {{
        const {{ {string.Join(", ", props.Distinct().OrderBy(a => a))} }} = this.props;
{string.Join(Environment.NewLine, render.Distinct().OrderBy(a => a))}
        return (
            <div className={{classes.root}}>
                <Grid container spacing={{24}}>
                    <Grid item xs={{12}}>
                        <Paper className={{classes.paper}}>Screen: {Screen.Title}</Paper>
                    </Grid>
{string.Join(Environment.NewLine, gridSections).IndentLines(20)}
                </Grid>
            </div>
        );
    }}
{string.Join(Environment.NewLine, methods.Distinct().OrderBy(a => a))}
}}

{Screen.InternalName}Page.propTypes = {{
    classes: PropTypes.object.isRequired
}};

function mapStateToProps(state{(mapStateToProps.Where(a => a.Contains("ownProps.")).Any() ? ", ownProps": "")}) {{
    return {{
        {string.Join($",{Environment.NewLine}        ", mapStateToProps.Distinct().OrderBy(a => a))}
    }};
}}

function mapDispatchToProps(dispatch) {{
    return {{ 
        {string.Join($",{Environment.NewLine}        ", mapDispatchToProps.Distinct().OrderBy(a => a))}
    }};
}}

export default withStyles(styles)(connect(mapStateToProps, mapDispatchToProps)({Screen.InternalName}Page));";
        }
    }
}