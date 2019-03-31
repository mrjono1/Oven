using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// tsconfig.json configuration
    /// </summary>
    public class TsLintTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public TsLintTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "tslint.json";
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
    ""extends"": [""tslint-react"", ""tslint:recommended"", ""tslint-config-prettier""],
    ""linterOptions"": {
        ""exclude"": [""node_modules/**/*.ts"", ""packages/*/lib"", ""packages/*/esm""]
    },
    ""rulesDirectory"": [""tslint-plugin-prettier""],
    ""jsRules"": {
        ""object-literal-sort-keys"": false,
        ""no-console"": {
            ""severity"": ""warning"",
            ""options"": [""log"", ""debug""]
        },
        ""no-empty"": false,
        ""no-shadowed-variable"": false,
        ""curly"": false,
        ""no-unused-expression"": false,
        ""triple-equals"": false
    },
    ""rules"": {
        ""jsx-no-multiline-js"": false,
        ""no-console"": {
            ""severity"": ""warning"",
            ""options"": [""log"", ""debug""]
        },
        ""object-literal-sort-keys"": false,
        ""ordered-imports"": false,
        ""jsx-boolean-value"": false,
        ""jsx-no-lambda"": false,
        ""variable-name"": false,
        ""prettier"": [
            true,
            { ""singleQuote"": true, ""tabWidth"": 4, ""trailingComma"": ""es5"" }
        ],
        ""interface-name"": [true, ""never-prefix""],
        ""member-access"": false
    }
}";
        }
    }
}