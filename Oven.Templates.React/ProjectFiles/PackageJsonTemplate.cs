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
    ""babel-core"": ""6.26.3"",
    ""babel-eslint"": ""8.2.6"",
    ""babel-loader"": ""7.1.4"",
    ""babel-plugin-transform-class-properties"": ""^6.9.1"",
    ""babel-plugin-syntax-object-rest-spread"": ""6.13.0"",
    ""babel-preset-es2015"": ""^6.9.0"",
    ""babel-preset-react"": ""^6.5.0"",
    ""babel-preset-react-hmre"": ""^1.1.1"",
    ""eslint"": ""5.3.0"",
    ""eslint-plugin-react"": ""7.10.0"",
    ""file-loader"": ""^0.8.5"",
    ""html-webpack-plugin"": ""3.2.0"",
    ""rimraf"": ""^2.5.0"",
    ""webpack"": ""4.8.3"",
    ""webpack-cli"": ""2.1.3"",
    ""webpack-hot-middleware"": ""2.22.2"",
    ""webpack-merge"": ""^4.1.2""
  }},
  ""dependencies"": {{
    ""@material-ui/core"": ""1.5.1"",
    ""@material-ui/icons"": ""2.0.3"",
    ""history"": ""4.7.2"",
    ""prop-types"": ""15.6.2"",
    ""react"": ""16.5.2"",
    ""react-admin"": ""2.4.0"",
    ""react-dom"": ""16.5.2"",
    ""react-redux"": ""5.0.7"",
    ""react-router"": ""4.3.1"",
    ""react-router-dom"": ""4.3.1"",
    ""redux"": ""4.0.1""
  }}
}}";
        }
        
    }
}
