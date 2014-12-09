namespace ClassLibrary1
{
    using System;
    using ClassLibrary2;
    using NServiceBus;

    class MyCommandHandler : IHandleMessages<MyCommand>
    {
        public void Handle(MyCommand message)
        {
            Console.WriteLine("Handling MyCommand");
        }
    }
}
