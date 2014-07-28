using System;
using Messages;
using NServiceBus;

namespace Server.Handlers
{
    public class CancelOrderTwoHandler : IHandleMessages<CancelOrderTwo>
    {
        private readonly IBus bus;

        public CancelOrderTwoHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(CancelOrderTwo message)
        {
            Console.WriteLine("=================================CancelOrderTwo=====================================");

            if (message.OrderId % 2 == 0)
                bus.Return((int) ErrorCodes.Fail);
            else
                bus.Return((int) ErrorCodes.None);
        }
    }
}