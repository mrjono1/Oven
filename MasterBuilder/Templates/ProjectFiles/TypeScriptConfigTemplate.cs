namespace MasterBuilder.Templates.ProjectFiles
{
    public class TypeScriptConfigTemplate
    {
        public static string FileName()
        {
            return "tsconfig.json";
        }
        public static string Evaluate()
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
