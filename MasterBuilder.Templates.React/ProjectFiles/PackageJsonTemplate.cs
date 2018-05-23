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
  ""private"": true,
  ""scripts"": {{
    ""build"": ""npm run clean-dist && webpack -p --config=configs/webpack/prod.js"",
    ""clean-dist"": ""rm -f -r -d dist"",
    ""lint"": ""npm run lint:ts && npm run lint:sass"",
    ""lint:ts"": ""tslint './src/**/*.ts*' --format stylish --force"",
    ""lint:sass"": ""stylelint ./src/**/*.scss"",
    ""start"": ""npm run start-dev"",
    ""start-dev"": ""webpack-dev-server --config=configs/webpack/dev.js"",
    ""start-prod"": ""npm run build && node express.js"",
    ""test"": ""jest --watch --coverage --config=configs/jest.json""
  }},
  ""dependencies"": {{
    ""@material-ui/core"": ""1.0.0"",
    ""@material-ui/icons"": ""1.0.0"",
    ""npm"": ""^5.7.1"",
    ""react"": ""16.3.2"",
    ""react-dom"": ""16.3.2"",
    ""react-redux"": ""^5.0.7"",
    ""react-router"": ""^4.2.0"",
    ""redux"": ""^3.7.2"",
    ""redux-logger"": ""^3.0.6"",
    ""redux-persist"": ""^5.5.0"",
    ""redux-thunk"": ""^2.2.0""
  }},
  ""devDependencies"": {{
    ""@types/history"": ""^4.6.2"",
    ""@types/jest"": ""22.1.0"",
    ""@types/node"": ""9.4.0"",
    ""@types/react"": ""^16.3.2"",
    ""@types/react-dom"": ""^16.0.5"",
    ""@types/react-redux"": ""^5.0.19"",
    ""@types/react-router"": ""^4.0.18"",
    ""@types/redux-logger"": ""^3.0.5"",
    ""@types/webpack-env"": ""^1.13.5"",
    ""autoprefixer"": ""8.5.0"",
    ""awesome-typescript-loader"": ""^5.0.0"",
    ""babel-cli"": ""^6.26.0"",
    ""babel-core"": ""^6.26.3"",
    ""babel-loader"": ""^7.1.4"",
    ""babel-preset-env"": ""^1.6.1"",
    ""babel-preset-react"": ""^6.24.1"", 
    ""copy-webpack-plugin"": ""^3.0.1"",
    ""csstype"": ""^2.4.2"",
    ""extract-text-webpack-plugin"": ""4.0.0-beta.0"",
    ""html-webpack-plugin"": ""^3.2.0"",
    ""redux-devtools-extension"": ""^2.13.2"",
    ""typescript"": ""2.8.3"",
    ""precss"": ""^1.4.0"",
    ""webpack"": ""4.8.3"",
    ""webpack-cli"": ""2.1.3"",
    ""webpack-merge"": ""^4.1.2""
  }}
}}";

            /* 
             * 
             *   ""devDependencies"": {{
    ""@types/jest"": ""^22.2.3"",
    ""@types/material-ui"": ""0.21.2"",
    ""@types/node"": ""^10.0.2"",
    ""@types/react"": ""^16.3.13"",
    ""@types/react-dom"": ""^16.0.5"",
    ""@material-ui/core"": ""1.0.0"",
    ""@material-ui/icons"": ""1.0.0"",
    ""awesome-typescript-loader"": ""^5.0.0"",
    ""babel-cli"": ""^6.26.0"",
    ""babel-core"": ""^6.26.3"",
    ""babel-loader"": ""^7.1.4"",
    ""babel-preset-env"": ""^1.6.1"",
    ""babel-preset-react"": ""^6.24.1"", 
    ""copy-webpack-plugin"": ""^3.0.1"",
    ""css-loader"": ""^0.28.11"", 
    ""extract-text-webpack-plugin"": ""4.0.0-beta.0"",
    ""file-loader"": ""^1.1.11"",
    ""html-webpack-plugin"": ""^3.2.0"",
    ""image-webpack-loader"": ""^4.2.0"",
    ""jest"": ""^22.4.3"",
    ""node-sass"": ""^4.9.0"",
    ""postcss-loader"": ""^2.1.4"", 
    ""precss"": ""^1.4.0"",
    ""react"": ""^16.3.2"",
    ""react-dom"": ""^16.3.2"",
    ""react-hot-loader"": ""^4.1.2"",
    ""sass-loader"": ""^7.0.1"",
    ""style-loader"": ""^0.21.0"",
    ""tslint"": ""^5.9.1"",
    ""typeface-roboto"": ""0.0.54"",
    ""typescript"": ""^2.8.3"",
    ""uglifyjs-webpack-plugin"": ""^1.2.5"",
    ""webpack"": ""^4.6.0"",
    ""webpack-cli"": ""^2.1.2"",
    ""webpack-dev-middleware"": ""^3.1.3"",
    ""webpack-dev-server"": ""^3.1.3"",
    ""webpack-merge"": ""^4.1.2""
  }}
  */
        }

    }
}
