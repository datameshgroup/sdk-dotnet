using Newtonsoft.Json;
using System;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Converts Base64↔String
    /// </summary>
    public class Base64JsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (reader.TokenType == JsonToken.Null) ? null : System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.Value as string));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value as string)));
        }
    }
}
