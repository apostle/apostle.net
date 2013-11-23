using Newtonsoft.Json;
using System;

namespace Apostle.Net
{
    /// <summary>
    /// Custom json serializer for the Mail class. The Apostle.io api expects a json payload
    /// that is not possible with the default serialization options using Newtonsoft.Json
    /// </summary>
    public class MailConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Mail));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var mail = (Mail)value;

            writer.WriteStartObject();
            writer.WritePropertyName("recipients");
            writer.WriteStartObject();
            writer.WritePropertyName(mail.ToAddress);
            writer.WriteStartObject();
            writer.WritePropertyName("template_id");
            writer.WriteValue(mail.TemplateSlug);
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
    }
}