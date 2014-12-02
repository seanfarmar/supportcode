namespace WebSQP
{
    using System;
    using Messages;
    using NServiceBus;

    public class Starter : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Bus.SendLocal(new Message {Guid = Guid.NewGuid()});
        }

        public void Stop()
        {            
        }
    }
}
