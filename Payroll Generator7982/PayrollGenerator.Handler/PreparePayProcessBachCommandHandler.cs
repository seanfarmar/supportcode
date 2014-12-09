namespace PayrollGenerator.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Messages.Commands;
    using Messages.Response;
    using NServiceBus;

    public class PreparePayProcessBachCommandHandler : IHandleMessages<PreparePayProcessBachCommand>
    {
        public IBus Bus { get; set; }

        public void Handle(PreparePayProcessBachCommand preparePayProcessBachCommand)
        {
            // this handler will build the batch for all employees
            // this can be done with dependency injection and a repository pattern

            //using (var conn = new SqlConnection("Password=sasql;Persist Security Info=True;User ID=sa;Initial Catalog=Fidelity;Data Source=ghyath_serhal\\v2012"))
            //{
            //    using (var cmd = new SqlCommand(string.Format("insert into aaaa values('{0}')", preparePayProcessBachCommand.Count), conn))
            //    {
            //        conn.Open();
            //        cmd.ExecuteNonQuery();
            //        conn.Close();
            //    }
            //}

            // now we have the employee list (of the ones we need to process a payment to) in a table in the DB
            // do a query to the db and get the list of Employee ids 
            //======
            // simulate that
            var employeeIdList = new List<int>();

            for (int i = 0; i < 15; i++)
            {
                employeeIdList.Add(i);
            }

            int numberOfEmployeesInBatch = employeeIdList.Count;

            Console.WriteLine("Batch executed successfully...");

            Bus.Reply(new PreparePayProcessBachResponse
            {
                ProcessId = preparePayProcessBachCommand.ProcessId,
                NumberOfEmployeesInBatch = numberOfEmployeesInBatch,
                EmployeeIdList = employeeIdList
            });
        }
    }
}