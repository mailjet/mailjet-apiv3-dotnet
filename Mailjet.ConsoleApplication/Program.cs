using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using System.Configuration;
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

            MailjetResponse response = await client.GetAsync(request);

            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

            Console.ReadLine();
        }
    }
}
