namespace WcfServiceLibrary1
{
    using System.ServiceModel;
    using Messages;
    using NServiceBus;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CreateProductService : ICreateProductService
    {
        private readonly IBus bus;

        public CreateProductService()
        {
            if (null == this.bus)
                this.bus = Configure.Instance.Builder.Build<IBus>();
        }

        public int Create(CreateProductMessage message)
        {
            this.bus.SendLocal(message);

            return message.ProductNumber == 1111 ? ErrorCodes.Fail : ErrorCodes.Success;
        }
    }

    public class ErrorCodes
    {
        public static int Fail = 500;
        public static int Success = 200;
    }
}