using NServiceBus;
using System;
using System.Threading;

#region Handler
public class CommandMessageHandler : IHandleMessages<Command>
{
    IBus bus;

    public CommandMessageHandler(IBus bus)
    {
        this.bus = bus;
    }

    public void Handle(Command message)
    {
        Console.WriteLine("Hello from CommandMessageHandler with Id: {0} - Sleeping for 10 sec.", message.Id);

        Thread.Sleep(TimeSpan.FromSeconds(10));

        bus.Return(message.Id%2 == 0 ? ErrorCodes.Fail : ErrorCodes.None);

        Console.WriteLine("Hello from CommandMessageHandler with Id: {0} - Returning", message.Id);
    }
}
#endregion