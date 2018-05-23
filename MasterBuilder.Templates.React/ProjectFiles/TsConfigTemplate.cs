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
    ""outDir"": ""./wwwroot"",
    ""module"": ""esnext"",
    ""target"": ""es5"",
    ""lib"": [""es6"", ""dom""],
    ""sourceMap"": true,
    ""allowJs"": true,
    ""jsx"": ""react"",
    ""moduleResolution"": ""node"",
    ""forceConsistentCasingInFileNames"": true,
    ""noImplicitReturns"": true,
    ""noImplicitThis"": true,
    ""noImplicitAny"": true,
    ""strictNullChecks"": true,
    ""suppressImplicitAnyIndexErrors"": true,
    ""noUnusedLocals"": true
  },
  ""exclude"": [
    ""node_modules"",
    ""build"",
    ""scripts"",
    ""acceptance-tests"",
    ""webpack"",
    ""jest"",
    ""src/setupTests.ts""
  ]
}";
        }
    }
}