using System.Net.Http.Headers;
using System.Text;

namespace Mailjet.Client;

public static class MailjetClientExtensions
{
    public static void SetDefaultSettings(this HttpClient client)
    {
        client.BaseAddress = new Uri(MailjetConstants.DefaultBaseAdress);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MailjetConstants.JsonMediaType));
        client.DefaultRequestHeaders.UserAgent.ParseAdd(MailjetConstants.UserAgent);
    }

    public static void UseBearerAuthentication(this HttpClient client, string token)
    {
        ArgumentNullException.ThrowIfNull(token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public static void UseBasicAuthentication(this HttpClient client, string apiKey, string apiSecret)
    {
        ArgumentNullException.ThrowIfNull(apiKey);
        ArgumentNullException.ThrowIfNull(apiSecret);
        var byteArray = Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }
}
