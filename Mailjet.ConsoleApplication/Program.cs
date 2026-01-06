using Mailjet.Client;
using Mailjet.Client.Resources;

IMailjetClient client = new MailjetClient(
    Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC") ?? throw new InvalidOperationException("MJ_APIKEY_PUBLIC not set"),
    Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE") ?? throw new InvalidOperationException("MJ_APIKEY_PRIVATE not set"));

MailjetRequest request = new MailjetRequest { Resource = Apikey.Resource };
MailjetResponse response = await client.GetAsync(request);

if (response.IsSuccessStatusCode)
{
    Console.WriteLine($"Total: {response.GetTotal()}, Count: {response.GetCount()}");
    Console.WriteLine(response.GetData());
}
else
{
    Console.WriteLine($"StatusCode: {response.StatusCode}");
    Console.WriteLine($"ErrorInfo: {response.GetErrorInfo()}");
    Console.WriteLine($"ErrorMessage: {response.GetErrorMessage()}");
}

Console.ReadLine();
