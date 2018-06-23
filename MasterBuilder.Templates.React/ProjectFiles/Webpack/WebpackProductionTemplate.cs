using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.ProjectFiles.Webpack
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
const path = require('path');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  mode: 'production',
  entry: [],

  output: {
    publicPath: ''
  },

  module: {
    rules: [
      {
        test: /\.scss$/,
        use: ExtractTextPlugin.extract({
          fallback: 'style-loader',
          use: [
          {
            loader: 'css-loader'
          },
          {
            loader: 'postcss-loader'
          }, 
          {
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
        })
      }
    ]
  },

  plugins: [
    new webpack.DefinePlugin({
      'process.env': {
        NODE_ENV: '""production""'
      },
      __DEVELOPMENT__: false
    }),
    new ExtractTextPlugin({ filename: 'bundle.css'}),
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