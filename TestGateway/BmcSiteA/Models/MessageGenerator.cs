using Headquarter.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BmcSiteA.Models
{
    public class MessageGenerator
    {
        private readonly IBus _bus;

        public MessageGenerator(IBus bus)
        {
            _bus = bus;
        }

        public void GenerateMessage()
        {
            try
            {
                _bus.SendToSites(new[] { "HO" }, new UpdatePrice { ProductId = 1, NewPrice = 100.0, ValidFrom = DateTime.Now });
            }
            catch (Exception ex)
            {
            }
        }
    }
}