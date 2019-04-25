using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.DataAccessLayer.Extensions
{
    /// <summary>
    /// String Extensions Template
    /// </summary>
    public class StringExtensions : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public StringExtensions(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "StringExtensions.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new string[] { "Extensions" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {

            return $@"using MongoDB.Bson;

namespace System
{{
    public static class StringExtensions
    {{
        public static ObjectId ObjectIdValue(this string value)
        {{
            return ObjectId.Parse(value);
        }}
        public static ObjectId? ObjectIdNullableValue(this string value)
        {{
            if (string.IsNullOrWhiteSpace(value))
            {{
                return null;
            }}
            else
            {{
                return ObjectId.Parse(value);
            }}
        }}
    }}
}}";
        }
    }
}
