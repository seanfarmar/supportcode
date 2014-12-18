using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class EventHappened : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public EventHappened()
        {
            Id = Guid.NewGuid();
            Name = string.Format("Name-{0}", Id.ToString());
        }
    }
}
