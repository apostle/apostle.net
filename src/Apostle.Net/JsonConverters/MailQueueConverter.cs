using Newtonsoft.Json;
using System;

namespace Apostle.Net.JsonConverters
{
    public class MailQueueConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(MailQueue));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var mailQueue = (MailQueue)value;

            // start the json document
            writer.WriteStartObject();

            // create the recipients object (name)
            writer.WritePropertyName("recipients");

            // start the recipients object (body)
            writer.WriteStartObject();

            // loop through each mail instance and serialize it
            foreach (var mail in mailQueue)
            {
                // name the mail object using the ToAddress's value
                writer.WritePropertyName(mail.ToAddress);
                //writer.WriteStartObject();

                // use the default serializer for the mail object
                serializer.Serialize(writer, mail);

                // end this mail object
                //writer.WriteEndObject();
            }

            // end the "recipients" object
            writer.WriteEndObject();

            // end the json document
            writer.WriteEndObject();
        }
    }
}