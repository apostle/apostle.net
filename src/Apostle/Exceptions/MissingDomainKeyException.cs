using System;
using System.Runtime.Serialization;

namespace Apostle.Exceptions
{
    [System.Serializable]
    public class MissingDomainKeyException : Exception
    {
        public MissingDomainKeyException()
        {
        }

        public MissingDomainKeyException(string message)
            : base(message)
        {
        }

        public MissingDomainKeyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MissingDomainKeyException(System.Runtime.Serialization.SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}