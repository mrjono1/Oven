using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Src
{
    /// <summary>
    /// Menu.jsx Template
    /// </summary>
    public class MenuTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "Menu.jsx";
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
            return $@"import React from 'react';
import {{ connect }} from 'react-redux';
import {{ MenuItemLink, getResources }} from 'react-admin';
import {{ withRouter }} from 'react-router-dom';
import LabelIcon from '@material-ui/icons/Label';

const CustomMenu = ({{ resources, onMenuClick }}) => (
    <div>
        {{resources.map(resource => (
            <MenuItemLink 
                to={{`/${{resource.name}}`}}
                primaryText={{resource.options.label}}
                leftIcon={{<LabelIcon />}}
                onClick={{onMenuClick}}
            />
        ))}}
        <MenuItemLink 
            to=""/custom-route""
            primaryText=""Miscellaneous""
            leftIcon={{<LabelIcon />}}
            onClick={{onMenuClick}}
        />
    </div>
);

const mapStateToProps = state => ({{
    resources: getResources(state)
}});

export default withRouter(connect(mapStateToProps)(CustomMenu));";
        }
    }
}