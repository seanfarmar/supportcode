namespace PayrollGenerator.Handler
{
    using System;
    using System.Data.SqlClient;
    using Messages;
    using NServiceBus;

    public class ProcessOrderCommandHandler : IHandleMessages<PayProcess>
    {
        public IBus Bus { get; set; }

        public void Handle(PayProcess payProcess)
        {
            // this handler will build the batch for all employees ?
            // this can be done with dipendancy injection and a repository pattern?

            using (var conn = new SqlConnection("Password=sasql;Persist Security Info=True;User ID=sa;Initial Catalog=Fidelity;Data Source=ghyath_serhal\\v2012"))
            {
                using (var cmd = new SqlCommand(string.Format("insert into aaaa values('{0}')", payProcess.Count), conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            // now we have the employee list in a table in the DB
            // simulate the result
            var numberOfEmployeesInBatch = 20;

            Console.WriteLine("Executed successfully...");

            Bus.Reply(new PayProcessResponse { ProcessId = payProcess.ProcessId, NumberOfEmployeesInBatch = numberOfEmployeesInBatch });
        }
    }
}
