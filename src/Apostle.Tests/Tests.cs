using System.Configuration;
using System.Diagnostics;
using Apostle.Enums;
using Apostle.Exceptions;
using NUnit.Framework;

namespace Apostle.Tests
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
            var deliveryResult = mail.Deliver();

            Assert.IsTrue(deliveryResult.Status.Equals(DeliveryStatus.Queued));
            Debug.Write(deliveryResult.JsonRequest);
        }

        [Test]
        public void Can_Send_An_Email_With_All_Attributes()
        {
            var mail = GenerateTestMailWithFullData();

            var deliveryResult = mail.Deliver();

            Assert.IsTrue(deliveryResult.Status.Equals(DeliveryStatus.Queued));
            Debug.Write(deliveryResult.JsonRequest);
        }

        [Test]
        public void Can_Send_Multiple_Emails_With_Full_Data()
        {
            var mailQueue = new Queue();

            for (int i = 0; i < 5; i++)
            {
                mailQueue.Add(GenerateTestMailWithFullData());
            }

            var deliveryResult = mailQueue.Deliver();

            Assert.IsTrue(deliveryResult.Status.Equals(DeliveryStatus.Queued));
            Debug.Write(deliveryResult.JsonRequest);
        }

        [Test]
        public void Can_Send_Multiple_Emails_With_Minimal_Data()
        {
            var mailQueue = new Queue();

            // create three mail instances
            mailQueue.Add(new Mail("welcome", _testEmailAddress));
            mailQueue.Add(new Mail("welcome", _testEmailAddress));
            mailQueue.Add(new Mail("welcome", _testEmailAddress));
            var deliveryResult = mailQueue.Deliver();

            Assert.IsTrue(deliveryResult.Status.Equals(DeliveryStatus.Queued));
            Debug.Write(deliveryResult.JsonRequest);
        }

        [SetUp]
        public void SetUp()
        {
            // reset the delivery host in case individual tests change it
            Apostle.DeliveryHost = DeliveryHosts.Apostle;
        }

        private Mail GenerateTestMailWithFullData()
        {
            var mail = new Mail("welcome", _testEmailAddress);

            mail.Name = "John Smith";
            mail.From = string.Concat("test.from.", _testEmailAddress);
            mail.Headers.Add("Test Header", "Test Header Value");
            mail.Headers.Add("Test Header 2", "Test Header 2 Value");
            mail.Headers.Add("Test Header 3", "Test Header 3 Value");
            mail.LayoutId = "standard";
            mail.ReplyTo = string.Concat("test.reply.to.", _testEmailAddress);
            mail.Data.Add("Example Data", "Example data values. This can be of any type");
            mail.Data.Add("Example Array Data", new[] { 1, 2, 3, 4 });
            mail.Data.Add("items", new[] { new { Name = "Widget", Price = 4.00, Quantity = 2 }, new { Name = "Widget 2", Price = 5.00, Quantity = 1 }, new { Name = "Widget 3", Price = 33.00, Quantity = 13 } });

            return mail;
        }
    }
}