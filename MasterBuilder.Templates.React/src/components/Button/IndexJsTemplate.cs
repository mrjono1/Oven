using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Components.Button
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
            return new string[] { "src", "components", "Button" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"/**
 * Button - A common button
 */

import React                    from 'react';
import { FlatButton,
         RaisedButton,
         FloatingActionButton,
         IconButton }           from 'material-ui';
import PropTypes                from 'prop-types';

/* component styles */
import { styles } from './styles.scss';

export default function Button(props) {
  const buttonElem = createButton(props);
  return (
    <div className={styles}>
      {buttonElem}
    </div>
  );
}

function createButton(props) {
  const {
    label,
    className,
    onTouchTap,
    icon,
    disabled,
    primary,
    secondary } = props;

  let buttonElem;

  if(props.floating) {
    buttonElem = <FloatingActionButton
                  label={label}
                  onTouchTap={onTouchTap}
                  className={className}
                  icon={icon}
                  disabled={disabled}
                  secondary={true}>
                  {props.icon}
                 </FloatingActionButton>

  } else if(props.floating && props.secondary) {
    buttonElem = <FloatingActionButton
                  label={label}
                  onTouchTap={onTouchTap}
                  className={className}
                  icon={icon}
                  disabled={disabled}
                  secondary={true} />

  } else if(props.iconOnly){
    buttonElem= <IconButton
                  label={label}
                  onTouchTap={onTouchTap}
                  className={className}
                  disabled={disabled}
                  icon={icon}>{props.icon}</IconButton>;

  } else if(props.raised && props.secondary) {
    buttonElem = <RaisedButton
                  label={label}
                  onTouchTap={onTouchTap}
                  className={className}
                  icon={icon}
                  disabled={disabled}
                  secondary={true} />

  } else if(props.raised) {
    buttonElem = <RaisedButton
                  label={label}
                  className={className}
                  onTouchTap={onTouchTap}
                  primary={primary}
                  secondary={secondary}
                  disabled={disabled} />
  } else if(props.flat) {
    buttonElem = <FlatButton
                  onTouchTap={onTouchTap}
                  className={className}
                  icon={icon}
                  disabled={disabled} />
  } else {
    buttonElem = <FlatButton
                  onTouchTap={onTouchTap}
                  className={className}
                  icon={icon}
                  label={label}
                  disabled={disabled} />
  }

  return buttonElem;
}

Button.propTypes = {
  raised   : PropTypes.bool,
  floating : PropTypes.bool,
  disabled : PropTypes.bool
};

Button.defaultProps = {
  type      : 'button',
  raised    : false,
  label     : '',
  className : 'btn',
  disabled : false,
  primary : true,
  secondary: false
}";
        }
    }
}