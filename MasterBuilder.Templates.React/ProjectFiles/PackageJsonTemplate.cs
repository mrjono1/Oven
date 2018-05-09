using Humanizer;
using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.ProjectFiles
{
    /// <summary>
    /// package.json configuration
    /// </summary>
    public class PackageJsonTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public PackageJsonTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "package.json";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { };
        }

        /// <summary>
        /// Get file path
        /// </summary>
        /// <returns></returns>
        public string GetFileContent()
        {
            return $@"{{
  ""name"": ""{Project.InternalName.Kebaberize()}"",
  ""version"": ""{Project.Version}"",
  ""description"": ""{Project.Title}"",
  ""scripts"": {{
    ""clean"": ""rimraf wwwroot"",
    ""build"": ""webpack --progress --verbose --colors --display-error-details --config webpack/common.config.js"",
    ""build:production"": ""npm run clean && npm run build"",
    ""lint"": ""eslint src"",
    ""start"": ""cross-env NODE_ENV=development node bin/server.js"",
    ""test"": ""bin/test.sh""
  }},
  ""devDependencies"": {{
    ""aspnet-webpack"": ""^2.0.3"",
    ""autoprefixer"": ""^6.6.1"",
    ""babel-core"": ""^6.9.1"",
    ""babel-eslint"": ""^6.0.4"",
    ""babel-loader"": ""^6.2.4"",
    ""babel-plugin-transform-class-properties"": ""^6.9.1"",
    ""babel-plugin-transform-decorators-legacy"": ""^1.3.4"",
    ""babel-polyfill"": ""^6.9.1"",
    ""babel-preset-es2015"": ""^6.9.0"",
    ""babel-preset-react"": ""^6.5.0"",
    ""babel-preset-react-hmre"": ""^1.1.1"",
    ""babel-preset-stage-1"": ""^6.24.1"",
    ""babel-register"": ""^6.9.0"",
    ""copy-webpack-plugin"": ""^3.0.1"",
    ""cross-env"": ""^5.0.0"",
    ""css-loader"": ""^0.28.0"",
    ""enzyme"": ""^2.3.0"",
    ""eslint"": ""^2.11.1"",
    ""eslint-config-airbnb"": ""3.1.0"",
    ""eslint-plugin-react"": ""^3.15.0"",
    ""exports-loader"": ""^0.6.2"",
    ""express"": ""^4.13.4"",
    ""extract-text-webpack-plugin"": ""^2.1.0"",
    ""file-loader"": ""^0.8.5"",
    ""ghooks"": ""^2.0.0"",
    ""html-webpack-plugin"": ""^2.15.0"",
    ""ignore-styles"": ""^2.0.0"",
    ""imports-loader"": ""^0.6.5"",
    ""jest"": ""^21.0.2"",
    ""keymirror"": ""^0.1.1"",
    ""morgan"": ""^1.6.1"",
    ""node-sass"": ""^4.7.2"",
    ""offline-plugin"": ""^4.9.0"",
    ""postcss-loader"": ""^1.3.3"",
    ""precss"": ""^1.4.0"",
    ""react-addons-test-utils"": ""^15.1.0"",
    ""react-hot-loader"": ""^1.3.0"",
    ""react-transform-hmr"": ""^1.0.1"",
    ""redux-logger"": ""2.4.0"",
    ""resolve-url-loader"": ""^1.4.3"",
    ""rimraf"": ""^2.5.0"",
    ""sass-extract-loader"": ""^1.1.0"",
    ""sass-loader"": ""^6.0.3"",
    ""style-loader"": ""^0.13.0"",
    ""url-loader"": ""^0.5.7"",
    ""webpack"": ""^3.0.0"",
    ""webpack-dev-middleware"": ""^1.5.0"",
    ""webpack-dev-server"": ""^1.14.1"",
    ""webpack-hot-middleware"": ""^2.6.0"",
    ""webpack-merge"": ""^0.7.3""
  }},
  ""dependencies"": {{
    ""axios"": ""^0.17.1"",
    ""localforage"": ""^1.5.6"",
    ""material-ui"": ""^0.20.0"",
    ""moment"": ""^2.20.1"",
    ""prop-types"": ""^15.6.0"",
    ""react"": ""^16.2.0"",
    ""react-dom"": ""^16.2.0"",
    ""react-redux"": ""^5.0.6"",
    ""react-router"": ""^4.2.0"",
    ""react-router-dom"": ""^4.2.2"",
    ""react-tap-event-plugin"": ""^3.0.2"",
    ""redux"": ""^3.7.2"",
    ""redux-thunk"": ""^2.2.0""
  }}
}}";
        }
        
    }
}
