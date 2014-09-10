namespace Slb.Messages
{
    using System;
    using NServiceBus;

    // you can use unobtrusive mode, see the other project
    public interface IAddWork : IMessage
    {
        Guid Id { get; set; }

        int Seconds { get; set; }
    }
}
