using Newtonsoft.Json;
using System;

namespace DataMeshGroup.Fusion
{
    public class DecimalJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            else
            {
                var result = Convert.ToDecimal(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                return result;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(!(value is decimal))
            {
                throw new ArgumentException($"Invalid argument {nameof(value)}. DecimalJsonConverter allocated to a non-decimal field", nameof(value));
            }
            
            writer.WriteRawValue(((decimal)value).ToString("G16", System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}
