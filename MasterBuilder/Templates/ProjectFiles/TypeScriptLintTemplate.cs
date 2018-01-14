using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ProjectFiles
{
    /// <summary>
    /// Type script lint
    /// </summary>
    public class TypeScriptLintTemplate: ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "tslist.json";
        }

        /// <summary>
        /// Get file path
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
  ""defaultSeverity"": ""warn"",
  ""rules"": {
    ""align"": false,
    ""ban"": false,
    ""class-name"": true,
    ""comment-format"": [
      true,
      ""check-space""
    ],
    ""component-class-suffix"": true,
    ""component-selector"": [
      true,
      ""element"",
      ""app"",
      ""kebab-case""
    ],
    ""curly"": false,
    ""directive-class-suffix"": true,
    ""directive-selector"": [
      true,
      ""attribute"",
      ""app"",
      ""camelCase""
    ],
    ""eofline"": true,
    ""forin"": true,
    ""import-destructuring-spacing"": true,
    ""indent"": [
      true,
      ""spaces""
    ],
    ""interface-name"": false,
    ""jsdoc-format"": true,
    ""label-position"": true,
    ""max-line-length"": [
      true,
      200
    ],
    ""member-access"": false,
    ""member-ordering"": [
      true,
      ""public-before-private"",
      ""static-before-instance"",
      ""variables-before-functions""
    ],
    ""no-any"": false,
    ""no-arg"": true,
    ""no-attribute-parameter-decorator"": true,
    ""no-bitwise"": true,
    ""no-conditional-assignment"": true,
    ""no-consecutive-blank-lines"": false,
    ""no-console"": [
      true,
      ""debug"",
      ""info"",
      ""time"",
      ""timeEnd"",
      ""trace""
    ],
    ""no-construct"": true,
    ""no-constructor-vars"": false,
    ""no-debugger"": true,
    ""no-duplicate-variable"": true,
    ""no-empty"": false,
    ""no-eval"": true,
    ""no-forward-ref"": true,
    ""no-inferrable-types"": false,
    ""no-input-rename"": true,
    ""no-internal-module"": true,
    ""no-null-keyword"": true,
    ""no-output-rename"": true,
    ""no-require-imports"": false,
    ""no-shadowed-variable"": false,
    ""no-string-literal"": false,
    ""no-switch-case-fall-through"": true,
    ""no-trailing-whitespace"": false,
    ""no-unused-expression"": true,
    ""no-unused-variable"": true,
    ""no-use-before-declare"": true,
    ""no-var-keyword"": true,
    ""no-var-requires"": false,
    ""object-literal-sort-keys"": false,
    ""one-line"": [
      true,
      ""check-open-brace"",
      ""check-catch"",
      ""check-else"",
      ""check-finally"",
      ""check-whitespace""
    ],
    ""pipe-naming"": [
      true,
      ""camelCase"",
      ""app""
    ],
    ""quotemark"": [
      true,
      ""single"",
      ""avoid-escape""
    ],
    ""radix"": true,
    ""semicolon"": [
      true,
      ""always""
    ],
    ""switch-default"": true,
    ""trailing-comma"": [
      true,
      {
        ""multiline"": ""never"",
        ""singleline"": ""never""
      }
    ],
    ""triple-equals"": [
      true,
      ""allow-null-check""
    ],
    ""typedef"": false,
    ""typedef-whitespace"": [
      true,
      {
        ""call-signature"": ""nospace"",
        ""index-signature"": ""nospace"",
        ""parameter"": ""nospace"",
        ""property-declaration"": ""nospace"",
        ""variable-declaration"": ""nospace""
      },
      {
        ""call-signature"": ""space"",
        ""index-signature"": ""space"",
        ""parameter"": ""space"",
        ""property-declaration"": ""space"",
        ""variable-declaration"": ""space""
      }
    ],
    ""use-host-property-decorator"": true,
    ""use-input-property-decorator"": true,
    ""use-life-cycle-interface"": true,
    ""use-output-property-decorator"": true,
    ""use-pipe-transform-interface"": true,
    ""variable-name"": [
      true,
      ""check-format"",
      ""allow-leading-underscore"",
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
  ""rulesDirectory"": [
    ""node_modules/codelyzer""
  ]
}";
        }
    }
}
