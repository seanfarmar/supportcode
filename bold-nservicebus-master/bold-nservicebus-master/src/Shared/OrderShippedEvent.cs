using System;
using NServiceBus;

namespace Shared
{
    public class OrderShippedEvent : IEvent
    {
        public DateTime ShippingDate { get; set; }
        public Guid Id { get; set; }
    }
}