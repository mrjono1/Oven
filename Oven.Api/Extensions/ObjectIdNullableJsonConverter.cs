using System;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Oven.Api
{
    /// <summary>
    /// Object Id Nullable Json Converter
    /// </summary>
    public class ObjectIdNullableJsonConverter : JsonConverter
    {
        /// <summary>
        /// Can Convert
        /// </summary>
        public override bool CanConvert(Type objectType) =>
            objectType == typeof(ObjectId?);

        /// <summary>
        /// Read Json
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            reader.Value == null ? null : ObjectId.Parse(reader.Value as string) as ObjectId?;

        /// <summary>
        /// Write Json
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue(value == null ? null : ((ObjectId)value).ToString());
    }
}