using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.React.ProjectFiles
{
    /// <summary>
    /// .eslintrc configuration
    /// </summary>
    public class EslintrcTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public EslintrcTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return ".eslintrc";
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
  ""extends"": [ ""eslint:recommended"", ""plugin:react/recommended"" ],
  ""parser"": ""babel-eslint"",
  ""parserOptions"": {{
    ""ecmaVersion"": 6,
    ""ecmaFeatures"": {{
      ""jsx"": true
    }},
    ""sourceType"": ""module""
  }},
  ""env"": {{
    ""browser"": true,
    ""node"": true,
    ""es6"": true
  }},
  ""plugins"": [
    ""react""
  ],
  ""rules"": {{
    ""no-console"": 0,
    ""no-debugger"": 2,
    ""new-cap"": 0,
    ""strict"": 0,
    ""no-underscore-dangle"": 0,
    ""no-use-before-define"": 0,
    ""eol-last"": 0,
    ""quotes"": [ 2, ""single""],
    ""indent"": [""error"", 4, {{ ""SwitchCase"": 1 }}],
    ""jsx-quotes"": 1,
    ""react/jsx-no-undef"": 1,
    ""react/jsx-uses-react"": 1,
    ""react/jsx-uses-vars"": 1,
    ""react/prop-types"": 0,
    ""react/jsx-closing-bracket-location"": 0,
    ""space-infix-ops"": 0,
    ""comma-dangle"": [ 2, ""never""],
    ""prop-types"": [ 0, ""never""],
    ""no-multi-spaces"": [1, {{
      ""exceptions"": {{
          ""VariableDeclarator"": true,
          ""ImportDeclaration"": true,
          ""Property"": true
      }}
    }}],
    ""react/jsx-key"": 0
  }}
}}";
        }
    }
}