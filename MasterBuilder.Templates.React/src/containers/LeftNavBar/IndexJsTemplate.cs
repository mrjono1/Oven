using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers.LeftNavBar
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
            return new string[] { "src", "containers", "LeftNavBar" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"import React, {{ Component }}   from 'react';
import {{ connect }}            from 'react-redux';
import {{ bindActionCreators }} from 'redux';
import {{ Drawer,
         AppBar,
         Divider }}           from 'material-ui';

/* component styles */
import {{ styles }} from './styles.scss';

/* actions */
import * as uiActionCreators   from 'core/actions/actions-ui';

class LeftNavBar extends Component {{
  constructor(props) {{
    super(props);
  }}

  closeNav=() => {{
    this.props.actions.ui.closeNav();
  }}

  render() {{
    return (
      <div className={{styles}} >
        <Drawer
          docked={{false}}
          open={{this.props.ui.leftNavOpen}}
          onRequestChange={{this.closeNav}}>
          <AppBar title=""{Project.Title}"" />
          <Divider />
        </Drawer>
      </div>
    );
  }}

}}

function mapStateToProps(state) {{
  return {{
    ui: state.ui
  }};
}}

function mapDispatchToProps(dispatch) {{
  return {{
    actions: {{
      ui   : bindActionCreators(uiActionCreators, dispatch)
    }}
  }};
}}

export default connect(mapStateToProps, mapDispatchToProps)(LeftNavBar);";
        }
    }
}