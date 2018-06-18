using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
    public enum ApiVersion
    {
        V3,
        V3_1,
        V4,
    }

    /// <summary>
    /// Mailjet API wrapper
    /// </summary>
    public class MailjetClient : IMailjetClient
    {
        private const string DefaultBaseAdress = "https://api.mailjet.com";
        private const string UserAgent = "mailjet-api-v3-net/1.0.1";
        private const string JsonMediaType = "application/json";
        private const string ApiVersionPathV3 = "v3";
        private const string ApiVersionPathV3_1 = "v3.1";
        private const string ApiVersionPathV4 = "v4";

        private HttpClient _httpClient;

        public MailjetClient(string apiKey, string apiSecret, HttpMessageHandler httpMessageHandler = null)
        {
            InitHttpClient(httpMessageHandler);

            // Set basic authentification
            var byteArray = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", apiKey, apiSecret));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public MailjetClient(string token, HttpMessageHandler httpMessageHandler = null)
        {
            InitHttpClient(httpMessageHandler);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public ApiVersion Version { get; set; } = ApiVersion.V3;

        public string BaseAdress
        {
            get { return _httpClient.BaseAddress != null ? _httpClient.BaseAddress.ToString() : null; }
            set { _httpClient.BaseAddress = !string.IsNullOrEmpty(value) ? new Uri(value) : null; }
        }

        public async Task<MailjetResponse> GetAsync(MailjetRequest request)
        {
            string url = BuildUrl(request);

            var responseMessage = await _httpClient.GetAsync(url);

            JObject content = await GetContent(responseMessage);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public async Task<MailjetResponse> PostAsync(MailjetRequest request)
        {
            string url = BuildUrl(request);

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPost = new StringContent(output, Encoding.UTF8, JsonMediaType);
            var responseMessage = await _httpClient.PostAsync(url, contentPost);

            JObject content = await GetContent(responseMessage);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public async Task<MailjetResponse> PutAsync(MailjetRequest request)
        {
            string url = BuildUrl(request);

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPut = new StringContent(output, Encoding.UTF8, JsonMediaType);
            var responseMessage = await _httpClient.PutAsync(url, contentPut);

            JObject content = await GetContent(responseMessage);
            MailjetResponse mailjetResponse = new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
            return mailjetResponse;
        }

        public async Task<MailjetResponse> DeleteAsync(MailjetRequest request)
        {
            string url = BuildUrl(request);

            var responseMessage = await _httpClient.DeleteAsync(url);

            JObject content = await GetContent(responseMessage);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        private async Task<JObject> GetContent(HttpResponseMessage responseMessage)
        {
            string cnt = null;

            if (responseMessage.Content != null)
            {
                cnt = await responseMessage.Content.ReadAsStringAsync();
            }

            JObject content;
            if (!string.IsNullOrEmpty(cnt) && responseMessage.Content.Headers.ContentType.MediaType == JsonMediaType)
            {
                content = JObject.Parse(cnt);
            }
            else
            {
                content = new JObject();
                content.Add("StatusCode", new JValue((int)responseMessage.StatusCode));

                if (!responseMessage.IsSuccessStatusCode)
                {
                    content.Add("ErrorInfo", new JValue(responseMessage.ReasonPhrase));
                }
            }

            return content;
        }

        private void InitHttpClient(HttpMessageHandler httpMessageHandler)
        {
            // Create HttpClient
            _httpClient = (httpMessageHandler != null) ? new HttpClient(httpMessageHandler) : new HttpClient();

            // Set base URI
            _httpClient.BaseAddress = new Uri(DefaultBaseAdress);

            // Set accepted media type
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));

            // Set user-agent
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        }

        private string BuildUrl(MailjetRequest request)
        {
            return UrlHelper.CombineUrl(GetApiVersionPath(), request.BuildUrl());
        }

        private string GetApiVersionPath()
        {
            switch (Version)
            {
                case ApiVersion.V3_1:
                    return ApiVersionPathV3_1;
                case ApiVersion.V4:
                    return ApiVersionPathV4;
                default:
                    return ApiVersionPathV3;
            }
        }
    }
}
