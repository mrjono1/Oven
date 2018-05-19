using MasterBuilder.Interfaces;
using MasterBuilder.Request;

namespace MasterBuilder.Templates.React.ProjectFiles
{
    /// <summary>
    /// tslint.json configuration
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
    ""jsRules"": {
        ""class-name"": true,
        ""comment-format"": [
            true,
            ""check-space""
        ],
        ""indent"": [
            true,
            ""spaces""
        ],
        ""no-duplicate-variable"": true,
        ""no-eval"": true,
        ""no-trailing-whitespace"": true,
        ""no-unsafe-finally"": true,
        ""one-line"": [
            true,
            ""check-open-brace"",
            ""check-whitespace""
        ],
        ""quotemark"": [
            true,
            ""double""
        ],
        ""semicolon"": [
            true,
            ""always""
        ],
        ""triple-equals"": [
            true,
            ""allow-null-check""
        ],
        ""variable-name"": [
            true,
            ""ban-keywords""
        ],
        ""whitespace"": [
            true,
            ""check-branch"",
            ""check-decl"",
            ""check-operator"",
            ""check-separator"",
            ""check-type""
        ]
    },
    ""rules"": {
        ""class-name"": true,
        ""comment-format"": [
            true,
            ""check-space""
        ],
        ""indent"": [
            true,
            ""spaces""
        ],
        ""no-eval"": true,
        ""no-internal-module"": true,
        ""no-trailing-whitespace"": true,
        ""no-unsafe-finally"": true,
        ""no-var-keyword"": true,
        ""one-line"": [
            true,
            ""check-open-brace"",
            ""check-whitespace""
        ],
        ""quotemark"": [
            true,
            ""double""
        ],
        ""semicolon"": [
            true,
            ""always""
        ],
        ""triple-equals"": [
            true,
            ""allow-null-check""
        ],
        ""typedef-whitespace"": [
            true,
            {
                ""call-signature"": ""nospace"",
                ""index-signature"": ""nospace"",
                ""parameter"": ""nospace"",
                ""property-declaration"": ""nospace"",
                ""variable-declaration"": ""nospace""
            }
        ],
        ""variable-name"": [
            true,
            ""ban-keywords""
        ],
        ""whitespace"": [
            true,
            ""check-branch"",
            ""check-decl"",
            ""check-operator"",
            ""check-separator"",
            ""check-type""
        ]
    }
}";
        }
    }
}