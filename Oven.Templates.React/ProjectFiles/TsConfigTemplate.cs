using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// tsconfig.json configuration
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
            return $@"{{
  ""compilerOptions"": {{
    /* Basic Options */
    ""target"": ""ES5"" /* Specify ECMAScript target version: 'ES3' (default), 'ES5', 'ES2015', 'ES2016', 'ES2017','ES2018' or 'ESNEXT'. */,
    ""module"": ""commonjs"" /* Specify module code generation: 'none', 'commonjs', 'amd', 'system', 'umd', 'es2015', or 'ESNext'. */,
    ""lib"": [
      ""es2017"",
      ""dom""
    ] /* Specify library files to be included in the compilation. */,
    ""allowJs"": true /* Allow javascript files to be compiled. */,
    ""jsx"": ""react"" /* Specify JSX code generation: 'preserve', 'react-native', or 'react'. */,
    
    /* Strict Type-Checking Options */
    // ""strict"": true /* Enable all strict type-checking options. */,
    ""noImplicitAny"": false /* Raise error on expressions and declarations with an implied 'any' type. */,
    /* Module Resolution Options */
    ""moduleResolution"": ""node"" /* Specify module resolution strategy: 'node' (Node.js) or 'classic' (TypeScript pre-1.6). */,
    ""allowSyntheticDefaultImports"": true /* Allow default imports from modules with no default export. This does not affect code emit, just typechecking. */,
    ""esModuleInterop"": true /* Enables emit interoperability between CommonJS and ES Modules via creation of namespace objects for all imports. Implies 'allowSyntheticDefaultImports'. */,
    ""skipLibCheck"": true
  }}
}}";
        }
    }
}