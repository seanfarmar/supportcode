using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Messages;

namespace Client
{
    class Program
    {
        private static readonly ChannelFactory<ICancelOrderService> ChannelFactory = new ChannelFactory<ICancelOrderService>("");
        private static readonly ChannelFactory<ICancelOrderTwoService> ChannelFactoryTwo = new ChannelFactory<ICancelOrderTwoService>("");

        private static void Main()
        {
            Console.WriteLine("This will send requests to the CancelOrder WCF service");
            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

            ICancelOrderService client = ChannelFactory.CreateChannel();
            ICancelOrderTwoService clientTwo = ChannelFactoryTwo.CreateChannel();
            
            int orderId = 1;

            try
            {
                while (Console.ReadLine() != null)
                {
                    var message = new CancelOrder
                                  {
                                      OrderId = orderId++
                                  };

                    Console.WriteLine("Sending CancelOrder message with OrderId {0}.", message.OrderId);

                    ErrorCodes returnCode = client.Process(message);

                    Console.WriteLine("Error code returned: " + returnCode);

                    var messageTwo = new CancelOrderTwo
                    {
                        OrderId = orderId++
                    };

                    Console.WriteLine("Sending CancelOrderTwo message with OrderId {0}.", messageTwo.OrderId);

                    ErrorCodes returnCodeTwo = clientTwo.Process(messageTwo);

                    Console.WriteLine("returnCodeTwo code returned: " + returnCodeTwo);
                }
            }
            finally
            {
                try
                {
                    ((IChannel)client).Close(); 
                    ((IChannel)clientTwo).Close();
                }
                catch
                {
                    ((IChannel)client).Abort(); 
                    ((IChannel)clientTwo).Abort();
                }
            }
        }
    }
}