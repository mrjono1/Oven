using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src
{
    /// <summary>
    /// Layout.js Template
    /// </summary>
    public class LayoutTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public LayoutTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Layout.jsx";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"import React from 'react';
import { connect } from 'react-redux';
import { Layout } from 'react-admin';
import CustomMenu from './Menu';

const darkTheme = {
    palette: {
        type: 'dark' // Switching the dark mode on is a single property value change.
    }
};

const lightTheme = {
    palette: {
        secondary: {
            light: '#5f5fc4',
            main: '#283593',
            dark: '#001064',
            contrastText: '#fff'
        }
    }
};

const CustomLayout = (props) => <Layout 
    {...props}
    menu={CustomMenu}
/>;

export default connect(
    state => ({
        theme: state.theme === 'dark' ? darkTheme : lightTheme
    }),
    {}
)(CustomLayout);";
        }
    }
}