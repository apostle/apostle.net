using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apostle.Net
{
    /// <summary>
    /// Represents the mail object of the Apostle.io service.
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// Instantiates a new instance of a Mail class with the minimum required
        /// parameters.
        /// </summary>
        /// <param name="templateId">The name of the Apostle.io template slug that identifies the mail template</param>
        /// <param name="toAddress">The mail recipient's email address</param>
        public Mail(string templateId, string toAddress)
        {
            TemplateId = templateId;
            ToAddress = toAddress;

            // initialize the Headers and Data dictionaries
            Headers = new Dictionary<string, string>();
            Data = new Dictionary<string, object>();

            // TODO: Validate that the toAddress is a valid email format
            //(http://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx)
        }

        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonProperty("layout_id")]
        public string LayoutId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("reply_to")]
        public string ReplyTo { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; private set; }

        [JsonIgnore]
        public string ToAddress { get; private set; }

        /// <summary>
        /// Ensures that the Apostle configuration is valid and then delivers the mail
        /// </summary>
        public DeliveryResult Deliver()
        {
            // ensure the configuration is setup
            Apostle.Validate();

            // so that we only have to write one json serializer, we'll just
            // send this Mail as a MailQueue with just one recipient
            var mailQueue = new MailQueue { this };
            return mailQueue.Deliver();
        }
    }
}