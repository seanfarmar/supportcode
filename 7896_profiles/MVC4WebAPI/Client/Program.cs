namespace Client
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    class Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }     
    }

    class Program
    {
        static void Main()
        {
            string cmd;

            Console.WriteLine("Press 's' to send a commands");

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                Console.WriteLine(Environment.NewLine);

                switch (cmd)
                {
                    case "s":
                        RunAsync().Wait();
                        Console.WriteLine("Press 's' to send a commands");
                        break;                   
                }
            }
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4810//");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP POST
                var command = new Command() { Id = Guid.NewGuid(), Name = "My Name is Pepper" };

                HttpResponseMessage response = await client.PostAsJsonAsync("api/command", command);
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Sucsesfully posted a command");
                }
            }
        }
    }
}
