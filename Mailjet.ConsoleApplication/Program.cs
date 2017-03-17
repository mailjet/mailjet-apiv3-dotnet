using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            
            MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["apiSecret"]);

            MailjetRequest request = new MailjetRequest()
            {
                Resource = Apikey.Resource
            };

            Console.WriteLine("Call GetAsync");
            Console.WriteLine();

            MailjetResponse response = await client.GetAsync(request);

            Console.WriteLine(string.Format("StatusCode: {0}", response.StatusCode));
            Console.WriteLine();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}", response.GetTotal()));
                Console.WriteLine();

                Console.WriteLine("Data");
                Console.WriteLine(response.GetData());
                Console.WriteLine();

                Console.WriteLine(string.Format("Count: {0}", response.GetCount()));
                Console.WriteLine();
            }
            else
            {
                string errorInfo;
                if (response.TryGetValue("ErrorInfo", out errorInfo))
                {
                    Console.WriteLine(string.Format("ErrorInfo: {0}", errorInfo));
                    Console.WriteLine();
                }

                string errorMessage;
                if (response.TryGetValue("ErrorMessage", out errorMessage))
                {
                    Console.WriteLine(string.Format("ErrorMessage: {0}", errorMessage));
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }
    }
}
