using Oven.Interfaces;

namespace Oven.Templates.React.ProjectFiles.Webpack
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
const webpack = require('webpack');
const merge = require('webpack-merge');

const development = require('./webpack.config.development.js');
const production = require('./webpack.config.production.js');

require('babel-polyfill');

const TARGET = process.env.NODE_ENV || 'start';

const PATHS = {
    app: path.join(__dirname, './src'),
    build: path.join(__dirname, './wwwroot')
};

process.env.BABEL_ENV = TARGET;

const common = {
    entry: [
        'babel-polyfill',
        PATHS.app
    ],

    output: {
        path: PATHS.build,
        filename: 'bundle.js',
        publicPath: ''
    },

    resolve: {
        extensions: ['.jsx', '.js', '.json'],
        modules: ['node_modules', PATHS.app]
    },
    optimization: {
        splitChunks: {
            cacheGroups: {
                commons: { test: /[\\/]node_modules[\\/]/, name: 'vendors', chunks: 'all' }
            }
        }
    },
    
    module: {
        rules: [{
            test: /\.jsx?$/,
            loaders: ['babel-loader'],
            exclude: /node_modules/
        },{
            test: /\.jpe?g$|\.gif$|\.png$|\.svg$|\.woff$|\.ttf$|\.wav$|\.mp3$/,
            loader: 'file-loader'
        }]
    }
};

if (TARGET === 'start') {
    module.exports = merge(development, common);
}

if (TARGET === 'build') {
    module.exports = merge(production, common);
}";
        }
    }
}
