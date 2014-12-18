using Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Subscriber
{
    public class EventSubscriber : IHandleMessages<EventHappened>
    {
        public void Handle(EventHappened message)
        {
            Thread.Sleep(TimeSpan.FromSeconds(6));

            Console.WriteLine("Handled {0}", message.Name);
        }
    }
}
