using Oven.Interfaces;
using Oven.Request;
using System.Linq;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// Webpack common config
    /// </summary>
    public class WebpackTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebpackTemplate(Project project)
        {
            Project = project;
        }
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
            return $@"const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const WorkboxPlugin = require('workbox-webpack-plugin');
const ManifestPlugin = require('webpack-manifest-plugin');

module.exports = (env) => {{
    const isDevBuild = !(env && env.prod);

    const plugins = [
        new CleanWebpackPlugin(path.join(__dirname, 'wwwroot')),
        new HtmlWebpackPlugin({{
            template: 'Views/Shared/_LayoutTemplate.cshtml',
            filename: '../Views/Shared/_Layout.cshtml',
            hash: true
        }}),
        new ManifestPlugin({{
            seed: {{
                name: '{Project.Title}',
                short_name: '{new string(Project.Title.Replace(" ","").Take(12).ToArray())}',
                theme_color: '{Project.ThemeColour}',
                display: 'standalone',
                start_url: '/'
            }}
        }})
    ];
    if (!isDevBuild) {{
        plugins.push(new WorkboxPlugin.GenerateSW({{
            // these options encourage the ServiceWorkers to get in there fast 
            // and not allow any straggling 'old' SWs to hang around
            clientsClaim: true,
            skipWaiting: true
        }}));
    }}

    return [{{
        mode: isDevBuild ? 'development' : 'production',

        devtool: isDevBuild ? 'eval-source-map' : 'source-map',

        stats: {{ modules: false }},

        entry: {{
            'app': path.join(__dirname, (isDevBuild ? './src/devIndex.jsx' : './src/'))
        }},

        watchOptions: {{
            ignored: /node_modules/
        }},

        output: {{
            filename: 'bundle.js',
            path: path.join(__dirname, './wwwroot'),
            publicPath: '/'
        }},

        resolve: {{
            // '.js' and '.jsx' as resolvable extensions.
            extensions: ['.js', '.jsx',]
        }},

        devServer: {{
            hot: isDevBuild
        }},

        module: {{
            rules: [
                {{
                    test: /\.jsx?$/,
                    loaders: ['babel-loader'],
                    exclude: /node_modules/
                }}
            ]
        }},

        optimization: {{
            splitChunks: {{
                cacheGroups: {{
                    commons: {{ test: /[\\/]node_modules[\\/]/, name: 'vendors', chunks: 'all' }}
                }}
            }}
        }},
        plugins: plugins
    }}];
}};";
        }
    }
}
