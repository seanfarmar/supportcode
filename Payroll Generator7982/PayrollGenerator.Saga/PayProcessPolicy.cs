namespace PayrollGenerator.Saga
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Messages.Commands;
    using Messages.Response;
    using NServiceBus;
    using NServiceBus.Saga;

    public class PayProcessPolicy : Saga<PayProcessData>,
        IAmStartedByMessages<PayProcessStarter>,
        IHandleMessages<PreparePayProcessBachResponse>,
        IHandleMessages<ProcessPayResponse>
    {
        public void Handle(PayProcessStarter message)
        {
            Data.ProcessId = message.ProcessId;

            var preparePayProcessBachCommand = new PreparePayProcessBachCommand {ProcessId = message.ProcessId};

            Console.WriteLine("Sending a PreparePayProcessBachCommand with processId: {0}",preparePayProcessBachCommand.ProcessId);
            
            Bus.Send(preparePayProcessBachCommand);
        }

        public void Handle(PreparePayProcessBachResponse message)
        {
            Data.PersonCount = message.NumberOfEmployeesInBatch;

            Data.EmployeeIdList = message.EmployeeIdList;

            Data.BatchReady = true;

            Console.WriteLine("Handling PreparePayProcessBachResponse");

            // now we can kick of the processing
            foreach (int employeeId in message.EmployeeIdList)
            {
                Console.WriteLine("Sending ProcessPayCommand for employee {0}", employeeId);

                Bus.Send(new ProcessPayCommand {ProcessId = message.ProcessId, EmployeeId = employeeId});
            }
        }

        public void Handle(ProcessPayResponse message)
        {
            // this is not production code, you may do some logic here...

            if(Data.EmployeeCompleteIdList == null)
                Data.EmployeeCompleteIdList = new List<int>();

            if (!Data.EmployeeCompleteIdList.Contains(message.EmployeeId))
                Data.EmployeeCompleteIdList.Add(message.EmployeeId);
            
            // this is for debugging:
            var stingList = new StringBuilder();
            
            Data.EmployeeCompleteIdList.ForEach(i => stingList.Append(i).Append(","));

            Console.WriteLine("Handling ProcessPayResponse with employeeId {0}, EmployeeCompleteIdList: {1}",
                message.EmployeeId, stingList);
            // end debugging

            bool areEqual = Data.EmployeeIdList.OrderBy(x => x)
                .SequenceEqual(Data.EmployeeCompleteIdList.OrderBy(x => x));

            if (areEqual)
            {
                // publish an event?
                // and then complete, you might want to keep the saga data and make it a logical complete i.e. Data.Complete == true
                Console.WriteLine("Completing the saga");

                MarkAsComplete();
            }
        }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<PayProcessStarter>(m => m.ProcessId).ToSaga(m => m.ProcessId);
            ConfigureMapping<PreparePayProcessBachResponse>(x => x.ProcessId).ToSaga(m => m.ProcessId);
            ConfigureMapping<ProcessPayResponse>(x => x.ProcessId).ToSaga(m => m.ProcessId);
        }
    }
}