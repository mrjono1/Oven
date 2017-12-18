using MasterBuilder.Helpers;
using System;

namespace MasterBuilder.Templates.ProjectFiles
{
    /// <summary>
    /// Webpack additions
    /// </summary>
    public class WebPackAdditionsTemplate : ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "webpack.additions.js";
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
            return @"/* [ Webpack Additions ]
 *
 * This file contains ADD-ONS we are adding on-top of the traditional JavaScriptServices repo
 * We do this so that those already using JavaScriptServices can easily figure out how to combine this repo into it.
 */

// Shared rules[] we need to add
const sharedModuleRules = [
  // sass
  { test: /\.scss$/, loaders: ['to-string-loader', 'css-loader', 'sass-loader'] },
  // font-awesome
  { test: /\.(woff2?|ttf|eot|svg)$/, loader: 'url-loader?limit=10000' }
];


module.exports = {
  sharedModuleRules
};";
        }
    }
}
