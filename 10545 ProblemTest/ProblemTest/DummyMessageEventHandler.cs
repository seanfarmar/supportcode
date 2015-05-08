using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NServiceBus;
using ProblemTest.Models;

namespace ProblemTest
{
    public class DummyMessageEventHandler : IHandleMessages<DummyMessageEvent>
    {
        public void Handle(DummyMessageEvent message)
        {
            Console.WriteLine(message);
        }
    }
}