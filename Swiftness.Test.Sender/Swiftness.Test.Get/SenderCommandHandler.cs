using NServiceBus;
using Swiftness.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swiftness.Test.Get
{
    public class SenderCommandHandler : IHandleMessages<SenderCommand>
    {
        IBus bus;

        public SenderCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(SenderCommand message)
        {

        }
    }
}
