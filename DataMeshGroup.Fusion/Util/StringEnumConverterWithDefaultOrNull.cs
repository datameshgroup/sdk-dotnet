using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace DataMeshGroup.Fusion
{
    /// <summary>
    /// Specialized StringEnumConverter that returns the default enum value instead of throwing if the string
    /// cannot be matched to an enum value during deserialization.
    /// </summary>
    public class StringEnumConverterWithDefaultOrNull<TEnum> : StringEnumConverter where TEnum : struct
    {
        public StringEnumConverterWithDefaultOrNull()
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader?.Value == null)
            {
                return null;
            }
            
            string s = reader.Value.ToString();

            // If value provided isn't available, try using "Unknown". If that doesn't work just use the default value
            if (!Enum.TryParse(s, ignoreCase: true, result: out TEnum t))
            {
                _ = Enum.TryParse("Unknown", ignoreCase: true, result: out t);
            }
            return t;
        }
    }
}