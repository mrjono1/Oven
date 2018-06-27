using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.ProjectFiles.Webpack
{
    /// <summary>
    /// Webpack dev config
    /// </summary>
    public class WebpackDevelopmentTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebpackDevelopmentTemplate(Project project)
        {
            Project = project;
        }
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "webpack.config.development.js";
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
            return @"const webpack = require('webpack');
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const precss = require('precss');
const autoprefixer = require('autoprefixer');

module.exports = {
    mode: 'development',
    devtool: 'source-map',
    entry: [
        'webpack-hot-middleware/client',
        './src/index'
    ],
    output: {
        publicPath: ''
    },
    
    module: {
        noParse: [new RegExp('node_modules/localforage/dist/localforage.js')],
        rules: [{
            test: /\.scss$/,
            use: [
                { loader: 'style-loader' },
                {
                    loader: 'css-loader',
                    options: {
                        localIdentName: '[hash:base64:5][path]-[local]'
                    }
                },{
                    loader: 'postcss-loader',
                    options: {
                        plugins: function () {
                            return [
                                precss,
                                autoprefixer
                            ];
                        }
                    }
                },{
                    loader: 'sass-loader',
                    options: {
                        sourceMap: true,
                        data: '@import ""variables"";',
                        includePaths: [
                            path.join(__dirname, '..', '/src/containers/App/styles')
                        ]
                    }
                }
            ]
        }]
    },
    
    plugins: [
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: '""development""'
            },
            __DEVELOPMENT__: true
        }),
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NoEmitOnErrorsPlugin(),
        new webpack.NamedModulesPlugin(), // prints more readable module names in the browser console on HMR updates
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
