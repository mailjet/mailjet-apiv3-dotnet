using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
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
        private HttpClient _httpClient;

        public MailjetClient(string apiKey, string apiSecret, HttpMessageHandler httpMessageHandler = null)
        {
            InitHttpClient(httpMessageHandler);
            _httpClient.UseBasicAuthentication(apiKey, apiSecret);
        }

        public MailjetClient(string token, HttpMessageHandler httpMessageHandler = null)
        {
            InitHttpClient(httpMessageHandler);
            _httpClient.UseBearerAuthentication(token);
        }

        /// <summary>
        /// Create MailJet client with predefinet HttpClient instance
        /// </summary>
        /// <param name="httpClient"></param>
        public MailjetClient(HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (httpClient.BaseAddress == null)
            {
                httpClient.SetDefaultSettings();
            }

            _httpClient = httpClient;
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
            HttpContent contentPost = new StringContent(output, Encoding.UTF8, MailjetDefaults.JsonMediaType);
            var responseMessage = await _httpClient.PostAsync(url, contentPost);

            JObject content = await GetContent(responseMessage);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public async Task<MailjetResponse> PutAsync(MailjetRequest request)
        {
            string url = BuildUrl(request);

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPut = new StringContent(output, Encoding.UTF8, MailjetDefaults.JsonMediaType);
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
            if (!string.IsNullOrEmpty(cnt) && responseMessage.Content.Headers.ContentType.MediaType == MailjetDefaults.JsonMediaType)
            {
                content = JObject.Parse(cnt);
            }
            else
            {
                content = new JObject();
                content.Add("StatusCode", new JValue((int)responseMessage.StatusCode));

                if (!responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == ((HttpStatusCode)429))
                    {
                        content.Add(MailjetDefaults.ErrorInfo, new JValue(MailjetDefaults.TooManyRequestsMessage));
                    }
                    else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        content.Add(MailjetDefaults.ErrorInfo, new JValue(MailjetDefaults.InternalServerErrorGeneralMessage));
                    }
                    else
                    {
                        content.Add(MailjetDefaults.ErrorInfo, new JValue(responseMessage.ReasonPhrase));
                    }
                }
            }

            return content;
        }

        private void InitHttpClient(HttpMessageHandler httpMessageHandler)
        {
            // Create HttpClient
            _httpClient = (httpMessageHandler != null) ? new HttpClient(httpMessageHandler) : new HttpClient();

            _httpClient.SetDefaultSettings();
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
                    return MailjetDefaults.ApiVersionPathV3_1;
                case ApiVersion.V4:
                    return MailjetDefaults.ApiVersionPathV4;
                default:
                    return MailjetDefaults.ApiVersionPathV3;
            }
        }
    }
}
