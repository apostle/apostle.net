﻿using Apostle.Enums;

namespace Apostle
{
    public class DeliveryResult
    {
        public string ApiResponse { get; set; }
        public string JsonRequest { get; set; }
        public string Message { get; set; }
        public string RequestData { get; set; }
        public DeliveryStatus Status { get; set; }
    }
}