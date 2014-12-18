using Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    public class EventPublisher : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("{0} start", GetType().Name);

            Schedule
                .Every(TimeSpan.FromSeconds(5))
                .Action(PublishEvent);
        }

        public void Stop()
        {
            Console.WriteLine("{0} stops", GetType().Name);
        }

        private void PublishEvent()
        {
            Console.WriteLine("{0} published", typeof(EventHappened).Name);
            Bus.Publish<EventHappened>();
        }
    }
}
