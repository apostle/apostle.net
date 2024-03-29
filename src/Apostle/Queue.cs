﻿using Apostle.JsonConverters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apostle
{
    /// <summary>
    /// Stores a collection of Mail instances that will be sent in bulk to the Apostle.io api
    /// </summary>
    [JsonConverter(typeof(MailQueueConverter))]
    public class Queue : List<Mail>
    {
        /// <summary>
        /// Ensures that the Apostle configuration is valid and then delivers the mail
        /// </summary>
        public DeliveryResult Deliver()
        {
            // ensure the config has been setup
            Apostle.Validate();
            return MailSender.Deliver(this);
        }
    }
}