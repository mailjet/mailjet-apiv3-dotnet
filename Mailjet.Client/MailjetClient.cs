using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
    /// <summary>
    /// Mailjet API wrapper
    /// </summary>
    public class MailjetClient
    {
        private const string _baseUrl = "https://api.mailjet.com/v3";
        private const string _userAgent = "mailjet-api-v3-net/1.0";
        private const string _mediaType = "application/json";

        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpClientHandler;

        public MailjetClient(string apiKey, string apiSecret, string baseUrl = _baseUrl)
        {
            _httpClientHandler = new HttpClientHandler();
            _httpClient = new HttpClient(_httpClientHandler);

            // Set base URL
            _httpClient.BaseAddress = new Uri(baseUrl);

            // Set accepted media type
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));

            // Set user-agent
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);

            // Set basic authentification
            var byteArray = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", apiKey, apiSecret));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<MailjetResponse> GetAsync(MailjetRequest request)
        {
            string url = _baseUrl + request.BuildUrl();

            MailjetResponse mailjetResponse = new MailjetResponse();

            var responseMessage = await _httpClient.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                mailjetResponse.Content = await responseMessage.Content.ReadAsAsync<JObject>();
            }

            return mailjetResponse;
        }
    }
}
