﻿using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jsonifier
{
    internal class CustomVlqBase128LeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            PropertyInfo? prop = value.GetType().GetProperty("Value");
            object? b128Int = prop.GetValue(value);
            JToken token = (b128Int != null) ? JToken.FromObject(b128Int, serializer) : JValue.CreateNull();
            token.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Name.StartsWith("VlqBase128Le");
        }
    }
}
