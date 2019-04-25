using Humanizer;
using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ClientApp
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
            return new string[] { "ClientApp" };
        }

        /// <summary>
        /// Get file path
        /// </summary>
        /// <returns></returns>
        public string GetFileContent()
        {
            return $@"{{
  ""name"": ""{Project.InternalName.Kebaberize().ToLower()}"",
  ""version"": ""{Project.Version}"",
  ""description"": ""{Project.Title}"",
  ""private"": true,
  ""dependencies"": {{
    ""@material-ui/core"": ""1.5.1"",
    ""@material-ui/icons"": ""2.0.3"",
    ""history"": ""4.7.2"",
    ""prop-types"": ""15.6.2"",
    ""react"": ""16.5.2"",
    ""react-admin"": ""2.8.4"",
    ""react-dom"": ""16.5.2"",
    ""react-redux"": ""5.0.7"",
    ""react-router"": ""4.3.1"",
    ""react-router-dom"": ""4.3.1"",
    ""redux"": ""4.0.1"",
    ""react-scripts"": ""2.1.8"",
    ""reactstrap"": ""^6.3.0"",
    ""rimraf"": ""^2.6.2"",
    ""typescript"": ""^3.4.1""
  }},
  ""devDependencies"": {{
    ""@types/jest"": ""^24.0.11"",
    ""@types/node"": ""^11.12.2"",
    ""@types/react"": ""^16.8.10"",
    ""@types/react-dom"": ""^16.8.3"",
    ""@types/react-redux"": ""^7.0.6"",
    ""@types/react-router"": ""^4.4.5"",
    ""ajv"": ""6.10.0"",
    ""cross-env"": ""^5.2.0""
  }},
  ""eslintConfig"": {{
    ""extends"": ""react-app""
  }},
  ""scripts"": {{
    ""start"": ""rimraf ./build && react-scripts start"",
    ""build"": ""react-scripts build"",
    ""test"": ""cross-env CI=true react-scripts test --env=jsdom"",
    ""eject"": ""react-scripts eject"",
    ""lint"": ""eslint ./src/""
  }},
  ""browserslist"": [
    "">0.2%"",
    ""not dead"",
    ""not ie <= 11"",
    ""not op_mini all""
  ]
}}";
        }
        
    }
}
