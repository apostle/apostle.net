using Apostle.Net.Exceptions;
using NUnit.Framework;
using System.Configuration;

namespace Apostle.Net.Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly string _testEmailAddress = ConfigurationManager.AppSettings["TestEmailAddress"];

        public Tests()
        {
            // set our apostle.io domain key
            Apostle.DomainKey = ConfigurationManager.AppSettings["ApostleDomainKey"];
        }

        [Test]
        [ExpectedException(typeof(MissingDeliveryHostnameException))]
        public void Can_Not_Send_Mail_With_Null_Delivery_Host()
        {
            Apostle.DeliveryHost = null;

            // create a mail instance
            var mail = new Mail("welcome", _testEmailAddress);
            mail.Deliver();
        }

        [Test]
        public void Can_Send_A_Minimal_Email_With_No_Data()
        {
            // create a mail instance
            var mail = new Mail("welcome", _testEmailAddress);
            mail.Deliver();
        }

        [SetUp]
        public void SetUp()
        {
            // reset the delivery host in case individual tests change it
            Apostle.DeliveryHost = DeliveryHosts.Apostle;
        }
    }
}