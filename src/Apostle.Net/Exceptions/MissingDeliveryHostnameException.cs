using System;
using System.Runtime.Serialization;

namespace Apostle.Net.Exceptions
{
    [Serializable]
    public class MissingDeliveryHostnameException : Exception
    {
        public MissingDeliveryHostnameException()
        {
        }

        public MissingDeliveryHostnameException(string message)
            : base(message)
        {
        }

        public MissingDeliveryHostnameException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MissingDeliveryHostnameException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}