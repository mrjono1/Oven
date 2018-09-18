using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.Src.Components.TextField
{
    /// <summary>
    /// index.js Template
    /// </summary>
    public class IndexJsTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexJsTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "index.js";
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "src", "components", "TextField" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"/**
 * Text Field - A common Text Field
 */

import React                              from 'react';
import { TextField as MaterialTextField } from 'material-ui';
import PropTypes                          from 'prop-types';


/* component styles */
import { styles } from './styles.scss';

export default function TextField(props) {
  return (
    <div className={styles}>
      <MaterialTextField {...props} />
    </div>
  );
}

TextField.propTypes = {
  hintText: PropTypes.string,
  type    : PropTypes.string
};

TextField.defaultProps = {
  type: 'text'
}";
        }
    }
}