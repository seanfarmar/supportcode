namespace Swiftness.Test.Get
{
    using Common;
    using NServiceBus;

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
