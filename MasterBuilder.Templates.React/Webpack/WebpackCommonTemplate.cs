using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.React.Webpack
{
    /// <summary>
    /// Webpack common config
    /// </summary>
    public class WebpackCommonTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "common.config.js";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "webpack" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return @"const path = require('path');
const webpack = require('webpack');
const merge = require('webpack-merge');
const autoprefixer = require('autoprefixer');

const development = require('./dev.config.js');
const production = require('./prod.config.js');

require('babel-polyfill');

const TARGET = process.env.npm_lifecycle_event;

const PATHS = {
  app: path.join(__dirname, '../src'),
  build: path.join(__dirname, '../wwwroot')
};

process.env.BABEL_ENV = TARGET;

const common = {
  entry: [
    'babel-polyfill',
    PATHS.app
  ],

  output: {
    path: PATHS.build,
    filename: 'bundle.js'
  },

  resolve: {
    extensions: ['.jsx', '.js', '.json', '.scss'],
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
    },
      {
        test: /\.jpe?g$|\.gif$|\.png$|\.svg$|\.woff$|\.ttf$|\.wav$|\.mp3$/,
        loader: 'file-loader'
      }]
  },

  plugins: [
    new webpack.LoaderOptionsPlugin({
      options: {
        context: __dirname,
        postcss: [
          autoprefixer()
        ]
      }
    })
  ]
};

if (TARGET === 'start' || !TARGET) {
  module.exports = merge(development, common);
}

if (TARGET === 'build' || !TARGET) {
  module.exports = merge(production, common);
}";
        }
    }
}
