namespace Swiftness.Test.Common
{
    using NServiceBus;

    public class SenderProcess
    {
        public void Process(IBus bus)
        {
            CoreLog4NetLoggerService _logger = new CoreLog4NetLoggerService();

            SenderCommand command = new SenderCommand();
            for (int i = 0; i < 10000; i++)
            {
                _logger.Info("Start Send");
                bus.Send(command);
                _logger.Info("End Send");
            }
        }
    }
}
