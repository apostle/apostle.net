using System.Collections.Generic;

namespace Apostle.Net
{
    /// <summary>
    /// Stores a collection of Mail instances that will be sent in bulk to the Apostle.io api
    /// </summary>
    public class MailQueue
    {
        private readonly Queue<Mail> _mailQueue = new Queue<Mail>();

        /// <summary>
        /// Adds a mail instance to the queue of mails that will be sent
        /// </summary>
        /// <param name="mail">A Mail instance that is ready to be queued for sending</param>
        public void Add(Mail mail)
        {
            _mailQueue.Enqueue(mail);
        }

        /// <summary>
        /// Ensures that the Apostle configuration is valid and then delivers the mail
        /// </summary>
        public void Deliver()
        {
            Apostle.Validate();
            MailSender.Deliver(_mailQueue);
        }
    }
}