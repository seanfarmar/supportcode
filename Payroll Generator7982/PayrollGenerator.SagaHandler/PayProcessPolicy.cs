namespace PayrollGenerator.SagaHandler
{
    using System;
    using NServiceBus;
    using NServiceBus.Saga;
    using Messages;

    public class PayProcessPolicy : Saga<PayProcessData>, IAmStartedByMessages<PayProcessStarter>, IHandleMessages<PayProcessEnder>, IHandleMessages<PayProcessResponse>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<PayProcessStarter>(m => m.ProcessId).ToSaga(m=> m.ProcessId);
            ConfigureMapping<PayProcessEnder>(x => x.ProcessId).ToSaga(m => m.ProcessId);
            ConfigureMapping<PayProcessEnder>(x => x.ProcessId).ToSaga(m => m.ProcessId);
        }

        public void Handle(PayProcessStarter message)
        {
            Data.ProcessId = message.ProcessId;
            
            var payProcess = new PayProcess() { ProcessId = message.ProcessId};
            
            Bus.Send(payProcess);
        }

        private void PlaceOrderReturnCodeHandler(IAsyncResult asyncResult)
        {
            // FileWriter.WriteToFile(this.Data.PersonCount.ToString());
            // use the auditing infrestructure for that?

            this.Data.PersonCount--;

            if (this.Data.PersonCount == 0)
            {
                PayProcessEnder payProcessEnder = new PayProcessEnder() { ProcessId = this.Data.ProcessId };
                Bus.Send(payProcessEnder);
            }
        }

        public void Handle(PayProcessEnder payProcessEnder)
        {
            this.MarkAsComplete();
        }

        public void Handle(PayProcessResponse message)
        {
            Data.PersonCount = message.NumberOfEmployeesInBatch;

            // now we can kick of the processing
        }
    }

    //public class FileWriter
    //{
    //    public static void WriteToFile(string message)
    //    {
    //        try
    //        {
    //            Directory.CreateDirectory(string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\Logging"));

    //            string loggingPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"\Logging\", DateTime.Now.ToShortDateString().Replace("/", "-"), ".txt");

    //            StreamWriter writer = new StreamWriter(loggingPath, true);

    //            writer.WriteLine(string.Concat("Time: ", DateTime.Now.TimeOfDay, "------ Message: ", message));
                
    //            writer.Close();
    //        }
    //        catch
    //        {
    //            throw;
    //        }
    //    }
    //}
}
