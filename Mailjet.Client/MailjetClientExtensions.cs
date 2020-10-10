using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Mailjet.Client
{
    /// <summary>
    /// Extensions for setting up mailjet http client with default settings
    /// </summary>
    public static class MailjetClientExtensions
    {

        /// <summary>
        /// Setting MediaType, BaseAddress, and UserAgent to default headers
        /// </summary>
        /// <param name="client">Instance of mailjet <see cref="HttpClient"/></param>
        public static void SetDefaultSettings(this HttpClient client)
        {
            client.BaseAddress = new Uri(MailjetDefaults.DefaultBaseAdress);

            // Set accepted media type
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MailjetDefaults.JsonMediaType));

            // Set user-agent
            client.DefaultRequestHeaders.UserAgent.ParseAdd(MailjetDefaults.UserAgent);
        }

        /// <summary>
        /// Setting Bearer access token
        /// </summary>
        /// <param name="client">Instance of mailjet HttpClient</param>
        /// <param name="token">Access token</param>
        public static void UseBearerAuthentication(this HttpClient client, string token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Setting basic authentication
        /// </summary>
        /// <param name="client">Instance of mailjet HttpClient</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="apiSecret">Api secret</param>
        public static void UseBasicAuthentication(this HttpClient client, string apiKey, string apiSecret)
        {
            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));

            if (apiSecret == null)
                throw new ArgumentNullException(nameof(apiSecret));

            // Set basic authentification
            var byteArray = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", apiKey, apiSecret));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
