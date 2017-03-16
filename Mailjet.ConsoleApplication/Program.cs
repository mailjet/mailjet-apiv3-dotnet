using Mailjet.Client;
using System;
using System.Collections.Generic;
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
            MailjetClient client = new MailjetClient("", "");
            MailjetRequest request = new MailjetRequest(new ResourceInfo("apikey"));

            Console.WriteLine("GetAsync");
            MailjetResponse response = await client.GetAsync(request);

            Console.WriteLine("Response:");
            Console.WriteLine(response.Content);

            Console.ReadLine();
        }
    }
}
