using Humanizer;
using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles
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
  ""private"": true,
  ""scripts"": {{
    ""clean"": ""rimraf wwwroot"",
    ""build"": ""webpack --colors --display-error-details"",
    ""build:production"": ""npm run clean && npm run build"",
    ""lint"": ""eslint --ext .js --ext .jsx src"",
    ""start"": ""webpack --watch --mode development --colors""
  }},
  ""devDependencies"": {{
    ""aspnet-webpack"": ""3.0.0"",
    ""autoprefixer"": ""8.5.1"",
    ""babel-core"": ""6.26.3"",
    ""babel-eslint"": ""8.2.6"",
    ""babel-loader"": ""7.1.4"",
    ""babel-plugin-transform-class-properties"": ""^6.9.1"",
    ""babel-plugin-transform-decorators-legacy"": ""^1.3.4"",
    ""babel-plugin-syntax-object-rest-spread"": ""6.13.0"",
    ""babel-polyfill"": ""^6.9.1"",
    ""babel-preset-es2015"": ""^6.9.0"",
    ""babel-preset-react"": ""^6.5.0"",
    ""babel-preset-react-hmre"": ""^1.1.1"",
    ""babel-preset-stage-1"": ""^6.24.1"",
    ""babel-register"": ""^6.9.0"",
    ""copy-webpack-plugin"": ""^3.0.1"",
    ""cross-env"": ""^5.0.0"",
    ""cross-fetch"": ""2.2.1"",
    ""enzyme"": ""^3.3.0"",
    ""eslint"": ""5.3.0"",
    ""eslint-plugin-react"": ""7.10.0"",
    ""exports-loader"": ""^0.6.2"",
    ""express"": ""^4.13.4"",
    ""extract-text-webpack-plugin"": ""4.0.0-beta.0"",
    ""file-loader"": ""^0.8.5"",
    ""ghooks"": ""^2.0.0"",
    ""html-webpack-plugin"": ""3.2.0"",
    ""imports-loader"": ""^0.6.5"",
    ""jest"": ""^21.0.2"",
    ""keymirror"": ""^0.1.1"",
    ""morgan"": ""^1.6.1"",
    ""offline-plugin"": ""5.0.3"",
    ""react-hot-loader"": ""^1.3.0"",
    ""react-transform-hmr"": ""^1.0.1"",
    ""redux-logger"": ""2.4.0"",
    ""resolve-url-loader"": ""^1.4.3"",
    ""rimraf"": ""^2.5.0"",
    ""url-loader"": ""^0.5.7"",
    ""webpack"": ""4.8.3"",
    ""webpack-cli"": ""2.1.3"",
    ""webpack-dev-middleware"": ""3.1.3"",
    ""webpack-dev-server"": ""3.1.4"",
    ""webpack-hot-middleware"": ""2.22.2"",
    ""webpack-merge"": ""^4.1.2""
  }},
  ""dependencies"": {{
    ""@material-ui/core"": ""1.5.1"",
    ""@material-ui/icons"": ""2.0.3"",
    ""history"": ""4.7.2"",
    ""localforage"": ""^1.5.6"",
    ""moment"": ""^2.20.1"",
    ""prop-types"": ""15.6.2"",
    ""react"": ""16.3.2"",
    ""react-admin"": ""2.3.2"",
    ""react-dom"": ""~16.3.0"",
    ""react-redux"": ""5.0.7"",
    ""react-router"": ""^4.2.0"",
    ""react-router-dom"": ""^4.2.2"",
    ""react-tap-event-plugin"": ""^3.0.2"",
    ""redux"": ""4.0.0""
  }}
}}";
        }
        
    }
}
