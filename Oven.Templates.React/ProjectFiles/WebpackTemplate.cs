using Oven.Interfaces;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// Webpack common config
    /// </summary>
    public class WebpackTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "webpack.config.js";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);

    const outputDir = (env && env.publishDir)
        ? env.publishDir
        : __dirname;

    return [{
        mode: isDevBuild ? 'development' : 'production',

        devtool: 'inline-source-map',

        stats: { modules: false },

        entry: {
            'App': path.join(outputDir, './src')
        },

        watchOptions: {
            ignored: /node_modules/
        },

        output: {
            filename: 'bundle.js',
            path: path.join(outputDir, './wwwroot'),
            publicPath: '/'
        },

        resolve: {
            // '.js' and '.jsx' as resolvable extensions.
            extensions: ['.js', '.jsx',]
        },

        devServer: {
            hot: isDevBuild
        },

        module: {
            rules: [
                {
                    test: /\.jsx?$/,
                    loaders: ['babel-loader'],
                    exclude: /node_modules/
                }
            ]
        },

        plugins: [
            new CleanWebpackPlugin(path.join(outputDir, 'wwwroot')),
            new HtmlWebpackPlugin({
                template: 'Views/Shared/_LayoutTemplate.cshtml',
                filename: '../Views/Shared/_Layout.cshtml',
                hash: true
            })
        ]
    }];
};";
        }
    }
}
