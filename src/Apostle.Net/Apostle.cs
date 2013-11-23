using Apostle.Net.Exceptions;

namespace Apostle.Net
{
    /// <summary>
    /// Main configuration for the Apostle.io service. This class is responsible for storing and validating the domain key
    /// and the delivery host
    /// </summary>
    public static class Apostle
    {
        static Apostle()
        {
            DeliveryHost = DeliveryHosts.Apostle;
        }

        public static DeliveryHosts DeliveryHost { get; set; }
        public static string DomainKey { get; set; }

        /// <summary>
        /// The Mail class calls this method to ensure that we have a domain key set before trying to send mail.
        /// </summary>
        internal static void Validate()
        {
            if (string.IsNullOrEmpty(DomainKey))
            {
                throw new MissingDomainKeyException("You must first set the Apostle.DomainKey before attempting to deliver mail");
            }

            if (null == DeliveryHost)
            {
                throw new MissingDeliveryHostnameException("You must first set the Apostle.DeliveryHost before attempting to deliver mail");
            }
        }
    }
}