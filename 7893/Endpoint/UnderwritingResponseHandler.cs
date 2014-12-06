namespace Endpoint
{
    using System;
    using UnderwritingResponse;
    using NServiceBus;

    public class UnderwritingResponseHandler : IHandleMessages<UnderwritingResponse>
    {
        public void Handle(UnderwritingResponse message)
        {
            Console.WriteLine("handling UnderwritingResponse");
        }
    }
}
