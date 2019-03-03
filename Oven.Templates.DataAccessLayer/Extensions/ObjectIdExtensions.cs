using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.DataAccessLayer.Extensions
{
    /// <summary>
    /// Object Id Extensions Template
    /// </summary>
    public class ObjectIdExtensions : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ObjectIdExtensions(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "ObjectIdExtensions.cs";
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

            return $@"namespace MongoDB.Bson
{{
    public static class ObjectIdExtensions
    {{
        public static string StringValue(this ObjectId? objectId)
        {{
            if (objectId == null)
            {{
                return null;
            }}
            else
            {{
                return objectId.ToString();
            }}
        }}

        public static string StringValue(this ObjectId objectId)
        {{
            if (objectId == null)
            {{
                return null;
            }}
            else
            {{
                return objectId.ToString();
            }}
        }}
    }}
}}";
        }
    }
}
