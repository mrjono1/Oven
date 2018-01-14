using MasterBuilder.Interfaces;

namespace MasterBuilder.Templates.ProjectFiles
{
    /// <summary>
    /// Type script config
    /// </summary>
    public class TypeScriptConfigTemplate: ITemplate
    {
        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "tsconfig.json";
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
  ""compilerOptions"": {
    ""moduleResolution"": ""node"",
    ""module"": ""es2015"",
    ""target"": ""es5"",
    ""alwaysStrict"": true,
    ""noImplicitAny"": false,
    ""sourceMap"": true,
    ""experimentalDecorators"": true,
    ""emitDecoratorMetadata"": true,
    ""skipDefaultLibCheck"": true,
    ""skipLibCheck"": true,
    ""allowUnreachableCode"": false,
    ""lib"": [
      ""es2016"",
      ""dom""
    ],
    ""types"": [ ""node"" ]
  },
  ""include"": [
    ""ClientApp""
  ]
}";
        }
    }
}
