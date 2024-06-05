using System;

using Newtonsoft.Json;

using yixiaozi.API.Todoist.Models;

namespace yixiaozi.API.Todoist.Serialization.Converters
{
    internal class StringEnumTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(StringEnum);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            if (StringEnum.TryParse(reader.Value?.ToString(), objectType, out var stringEnum))
            {
                return stringEnum;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((StringEnum)value).Value);
        }
    }
}
