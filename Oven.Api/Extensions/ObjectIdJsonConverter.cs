using System;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Oven.Api
{
    /// <summary>
    /// Object Id Json Converter
    /// </summary>
    public class ObjectIdJsonConverter : JsonConverter
    {
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
    }
}