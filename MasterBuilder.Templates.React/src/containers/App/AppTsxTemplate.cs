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
import TopAppBar from '../../components/TopAppBar/TopAppBar';

// global styles for entire app
import './styles/app.scss';
import 'typeface-roboto';

export interface AppProps {
}

export default class App extends React.Component<AppProps, undefined> {
    render() {
        return (
            <div className=""app"">
                <TopAppBar/>
                <h1>Hello World!</h1>
                <p>Foo to the barz</p>
            </div>
        );
    }
}";
        }
    }
}