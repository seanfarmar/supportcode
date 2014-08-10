namespace WcfServiceLibrary1
{
    using System;
    using Messages;
    using NServiceBus;

    public class CreateProductMessageHandler : IHandleMessages<CreateProductMessage>
    {
        public void Handle(CreateProductMessage message)
        {
            // Creat a product...
            Console.WriteLine("Creeating Product guid: {0}, name: {1} Number: {2}", message.GuidId, message.Name, message.ProductNumber);
           
        }
    }
}