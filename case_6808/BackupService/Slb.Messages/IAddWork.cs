namespace Slb.Messages
{
    using System;

    public interface IAddWork : IMessage
    {
        Guid Id { get; set; }

        int Seconds { get; set; }
    }
}
