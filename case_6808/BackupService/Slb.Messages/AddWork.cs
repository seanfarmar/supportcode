namespace Slb.Messages
{
    using System;

    public class AddWork : IAddWork
    {
        public Guid Id { get; set; }

        public int Seconds { get; set; }
    }
}
