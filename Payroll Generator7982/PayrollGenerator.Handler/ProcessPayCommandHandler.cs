namespace PayrollGenerator.Handler
{
    using System;
    using Messages.Commands;
    using Messages.Response;
    using NServiceBus;

    public class ProcessPayCommandHandler : IHandleMessages<ProcessPayCommand>
    {
        public IBus Bus { get; set; }

        public void Handle(ProcessPayCommand message)
        {
            Console.WriteLine("Processing pay for employeeId: {0}, in process batch id: {1}", message.EmployeeId,
                message.ProcessId);

            Bus.Reply(new ProcessPayResponse {EmployeeId = message.EmployeeId, ProcessId = message.ProcessId});

            // testing failure scenarios 
            if (message.EmployeeId % 2 == 1) throw new Exception("Just throwing the odd numbers to see if that breaks the sending handler");
        }
    }
}