using NServiceBus;
using System;

public class ShipOrderCommand :
    ICommand
{
    public Guid OrderId { get; set; }
    public DateTime ShippingDate { get; set; }
}