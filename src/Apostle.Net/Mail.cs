using Newtonsoft.Json;

namespace Apostle.Net
{
    /// <summary>
    /// Represents the mail object of the Apostle.io service.
    /// </summary>
    [JsonConverter(typeof(MailConverter))]
    public class Mail
    {
        /// <summary>
        /// Instantiates a new instance of a Mail class with the minimum required
        /// parameters.
        /// </summary>
        /// <param name="templateSlug">The name of the Apostle.io template slug that identifies the mail template</param>
        /// <param name="toAddress">The mail recipient's email address</param>
        public Mail(string templateSlug, string toAddress)
        {
            TemplateSlug = templateSlug;
            ToAddress = toAddress;

            // TODO: Validate that the toAddress is a valid email format
            //(http://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx)
        }

        public string TemplateSlug { get; private set; }
        public string ToAddress { get; private set; }

        /// <summary>
        /// Ensures that the Apostle configuration is valid and then delivers the mail
        /// </summary>
        public void Deliver()
        {
            Apostle.Validate();
            MailSender.Deliver(this);
        }
    }
}