using System;
using System.Runtime.InteropServices;
using NServiceBus;

namespace Shared
{
    public class FailSendCommand : ICommand
    {
        public Guid Id { get; set; }
        public DateTime SentAt { get; set; }
    }
}