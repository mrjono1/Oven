using Oven.Interfaces;
using Oven.Request;

namespace Oven.Templates.Shared.Extensions
{
    /// <summary>
    /// ObjectId Json Converter Template
    /// </summary>
    public class ObjectIdJsonConverterTemplate : ITemplate
    {
        private readonly Project Project;

        /// <summary>
        /// Constructor
        /// </summary>
        public ObjectIdJsonConverterTemplate(Project project)
        {
            Project = project;
        }

        /// <summary>
        /// Get file name
        /// </summary>
        public string GetFileName()
        {
            return "ObjectIdJsonConverter.cs";
        }

        /// <summary>
        /// Get file path
        /// </summary>
        public string[] GetFilePath()
        {
            return new[] { "Extensions" };
        }

        /// <summary>
        /// Get file content
        /// </summary>
        public string GetFileContent()
        {
            return $@"using System;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Oven.Shared
{{
    /// <summary>
    /// Object Id Json Converter
    /// </summary>
    public class ObjectIdJsonConverter : JsonConverter
    {{
        /// <summary>
        /// Can Convert
        /// </summary>
        public override bool CanConvert(Type objectType) =>
            objectType == typeof(ObjectId);

        /// <summary>
        /// Read Json
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            ObjectId.Parse(reader.Value as string);

        /// <summary>
        /// Write Json
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(((ObjectId)value).ToString());
    }}
}}";
        }
    }
}