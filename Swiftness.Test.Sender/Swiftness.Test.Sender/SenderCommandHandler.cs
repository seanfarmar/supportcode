using NServiceBus;
using Swiftness.Test.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swiftness.Test.Sender
{
    public partial class SenderCommandHandler : IWantToRunWhenBusStartsAndStops
    {
        private CoreLog4NetLoggerService _logger;
        private IBus bus;

        public SenderCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Start()
        {
            for (int i = 0; i < 10000; i++)
            {
                bus.Send(new SenderCommand());
            }
        }

        /*public void Handle(SenderCommand command)
        {
            try
            {
                _logger = new CoreLog4NetLoggerService();
                new SenderProcess().Process(bus);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }*/


        public void Stop()
        {
            
        }
    }
}
