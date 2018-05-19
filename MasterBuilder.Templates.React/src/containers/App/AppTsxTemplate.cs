using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.Src.Containers.App
{
    /// <summary>
    /// index.tsx Template
    /// </summary>
    public class AppTsxTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppTsxTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "App.tsx";
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
            return @"import * as React from 'react';

// global styles for entire app
import './styles/app.scss';

export interface AppProps {
}

export default class App extends React.Component<AppProps, undefined> {
    render() {
        return (
            <div className=""app"">
                <h1>Hello World!</h1>
                <p>Foo to the barz</p>
            </div>
        );
    }
}
";

            var x = @"
import { HashRouter, Route }      from 'react-router-dom';


// global styles for entire app
import './styles/app.scss';

/* application containers */
import Header     from '../Header';
import LeftNavBar from '../LeftNavBar';
import Home       from '../Home';

injectTapEventPlugin();

export class App extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <MuiThemeProvider muiTheme={getMuiTheme()}>
        <div>
          <Header />
          <div className=""container"">
            <HashRouter>
              <div>
                <Route exact path=""/"" component={Home}/>
              </div>
            </HashRouter>
          </div>
          <LeftNavBar />
        </div>
      </MuiThemeProvider>
    );
  }
}

export default connect(null)(App);";
        }
    }
}