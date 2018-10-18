using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles.Webpack
{
    /// <summary>
    /// Webpack prod config
    /// </summary>
    public class WebpackProductionTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebpackProductionTemplate(Project project)
        {
            Project = project;
        }
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "webpack.config.production.js";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        /// <returns></returns>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    mode: 'production',
    entry: [],
        
    plugins: [
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: '""production""'
            },
            __DEVELOPMENT__: false
        }),
        new HtmlWebpackPlugin({
            template: 'Views/Shared/_LayoutTemplate.cshtml',
            filename: '../Views/Shared/_Layout.cshtml',
            hash: true
        })
    ]
};";
        }
    }
}
