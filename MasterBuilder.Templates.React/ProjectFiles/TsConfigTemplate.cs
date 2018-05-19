using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.ProjectFiles
{
    /// <summary>
    /// tsconfig.json
    /// </summary>
    public class TsConfigTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public TsConfigTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "tsconfig.json";
        }

        /// <summary>
        /// Get file content
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
            return @"{
    ""compilerOptions"": {
        ""outDir"": ""./wwwroot/"",
        ""sourceMap"": true,
        ""noImplicitAny"": false,
        ""module"": ""commonjs"",
        ""target"": ""es5"",
        ""jsx"": ""react"",
        ""lib"": [""es5"", ""es6"", ""dom""]
    },
    ""include"": [
        ""./src/**/*""
    ]
}";
        }
    }
}