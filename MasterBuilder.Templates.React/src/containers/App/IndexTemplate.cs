using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers.App
{
    /// <summary>
    /// index.js Template
    /// </summary>
    public class IndexTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "index.jsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "containers", "App" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import React from 'react';
import PropTypes from 'prop-types';
import {{ connect }} from 'react-redux';
import injectTapEventPlugin from 'react-tap-event-plugin';
import {{ Route }} from 'react-router-dom'
import AppRoutes from '../../components/AppRoutes';

import {{ withStyles }} from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import Hidden from '@material-ui/core/Hidden';
import Divider from '@material-ui/core/Divider';
import MenuIcon from '@material-ui/icons/Menu';
import LeftDrawer from '../../components/LeftDrawer';

injectTapEventPlugin();
const drawerWidth = 240;

// global styles for entire app
import './styles/app.scss';

const styles = theme => ({{
    root: {{
        flexGrow: 1,
        height: 430,
        zIndex: 1,
        overflow: 'hidden',
        position: 'relative',
        display: 'flex',
        width: '100%',
    }},
    appBar: {{
        position: 'absolute',
        marginLeft: drawerWidth,
        [theme.breakpoints.up('md')]: {{
            width: `calc(100% - ${{drawerWidth}}px)`,
        }},
    }},
    navIconHide: {{
        [theme.breakpoints.up('md')]: {{
            display: 'none',
        }},
    }},
    toolbar: theme.mixins.toolbar,
    drawerPaper: {{
        width: drawerWidth,
        [theme.breakpoints.up('md')]: {{
            position: 'relative',
        }},
    }},
    content: {{
        flexGrow: 1,
        backgroundColor: theme.palette.background.default,
        padding: theme.spacing.unit * 3,
    }},
}});

class App extends React.Component {{
    state = {{
        mobileOpen: false,
    }};

    handleDrawerToggle = () => {{
        this.setState({{ mobileOpen: !this.state.mobileOpen }});
    }};

    render() {{
        const {{ classes, theme }} = this.props;

        const drawer = (
            <div>
                <div className={{classes.toolbar}} />
                <Divider />
                <LeftDrawer/>
            </div>
        );

        return (
            <div className={{classes.root}}>
                <AppBar className={{classes.appBar}}>
                    <Toolbar>
                        <IconButton
                            color=""inherit""
                            aria-label=""open drawer""
                            onClick={{this.handleDrawerToggle}}
                            className={{classes.navIconHide}}
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography variant=""title"" color=""inherit"" noWrap>
                            {Project.Title}
                        </Typography>
                    </Toolbar>
                </AppBar>
                <Hidden mdUp>
                    <Drawer
                        variant=""temporary""
                        anchor={{theme.direction === 'rtl' ? 'right' : 'left'}}
                        open={{this.state.mobileOpen}}
                        onClose={{this.handleDrawerToggle}}
                        classes={{{{
                            paper: classes.drawerPaper,
                        }}}}
                        ModalProps={{{{
                            keepMounted: true, // Better open performance on mobile.
                        }}}}
                    >
                        {{drawer}}
                    </Drawer>
                </Hidden>
                <Hidden smDown implementation=""css"">
                    <Drawer
                        variant=""permanent""
                        open
                        classes={{{{
                            paper: classes.drawerPaper,
                        }}}}
                    >
                        {{drawer}}
                    </Drawer>
                </Hidden>
                <main className={{classes.content}}>
                    <div className={{classes.toolbar}} />
                    <AppRoutes />
                </main>
            </div>
        );
    }}
}}

App.propTypes = {{
    classes: PropTypes.object.isRequired,
    theme: PropTypes.object.isRequired,
}};

export default withStyles(styles, {{ withTheme: true }})(connect(null)(App));";
        }
    }
}