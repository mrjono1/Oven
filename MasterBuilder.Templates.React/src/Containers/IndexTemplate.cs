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
            var hasFormSection = false;

            foreach (var screenSection in Screen.ScreenSections)
            {
                switch (screenSection.ScreenSectionType)
                {
                    case ScreenSectionType.Form:
                    //    var formSection = new FormSectionTemplate(Project, Screen, screenSection);
                    //    sections.Add(formSection.Evaluate());
                        hasFormSection = true;
                        break;
                    case ScreenSectionType.Search:
                   //     var searchSection = new SearchSectionTemplate(Project, Screen, screenSection);
                     //   sections.Add(searchSection.Evaluate());
                        break;
                    case ScreenSectionType.MenuList:
                        var menuListSection = new MenuListSectionTemplate(Project, Screen, screenSection);
                        sections.Add(menuListSection.Evaluate());
                        imports.AddRange(menuListSection.Imports());
                        break;
                    case ScreenSectionType.Html:
                        sections.Add($@"        <div class=""screen-section-html container mat-elevation-z2"" fxFlex>
{screenSection.Html}
        </div>");
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
                gridSections.Add($@"                    <Grid item xs={{12}}>
                        <Paper className={{classes.paper}}>
{section}
                        </Paper>
                    </Grid>");
            }

            return $@"import React from 'react';
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
    render() {{
        const {{ classes }} = this.props;

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
    classes: PropTypes.object.isRequired,
}};

export default withStyles(styles)({Screen.InternalName}Page);";
        }
    }
}