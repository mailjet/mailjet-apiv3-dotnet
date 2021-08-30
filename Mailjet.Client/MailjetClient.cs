using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
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
        /// Create MailJet client with predefined HttpClient instance
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

        public string BaseAdress
        {
            get { return _httpClient.BaseAddress != null ? _httpClient.BaseAddress.ToString() : null; }
            set { _httpClient.BaseAddress = !string.IsNullOrEmpty(value) ? new Uri(value) : null; }
        }

        public async Task<MailjetResponse> GetAsync(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var responseMessage = await _httpClient.GetAsync(url).ConfigureAwait(false);

            JObject content = await GetContent(responseMessage).ConfigureAwait(false);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public async Task<MailjetResponse> PostAsync(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPost = new StringContent(output, Encoding.UTF8, MailjetConstants.JsonMediaType);
            var responseMessage = await _httpClient.PostAsync(url, contentPost).ConfigureAwait(false);

            JObject content = await GetContent(responseMessage).ConfigureAwait(false);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public async Task<MailjetResponse> PutAsync(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPut = new StringContent(output, Encoding.UTF8, MailjetConstants.JsonMediaType);
            var responseMessage = await _httpClient.PutAsync(url, contentPut).ConfigureAwait(false);

            JObject content = await GetContent(responseMessage).ConfigureAwait(false);
            MailjetResponse mailjetResponse = new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
            return mailjetResponse;
        }

        public async Task<MailjetResponse> DeleteAsync(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var responseMessage = await _httpClient.DeleteAsync(url).ConfigureAwait(false);

            JObject content = await GetContent(responseMessage).ConfigureAwait(false);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        private async Task<JObject> GetContent(HttpResponseMessage responseMessage)
        {
            string cnt = null;

            if (responseMessage.Content != null)
            {
                cnt = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            JObject content;
            if (!string.IsNullOrEmpty(cnt) && responseMessage.Content.Headers.ContentType.MediaType == MailjetConstants.JsonMediaType)
            {
                content = JObject.Parse(cnt);
            }
            else
            {
                content = new JObject();
                content.Add("StatusCode", new JValue((int)responseMessage.StatusCode));
            }

            if (!responseMessage.IsSuccessStatusCode && !content.ContainsKey(MailjetConstants.ErrorInfo))
            {
                if (responseMessage.StatusCode == ((HttpStatusCode)429))
                {
                    content.Add(MailjetConstants.ErrorInfo, new JValue(MailjetConstants.TooManyRequestsMessage));
                }
                else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    content.Add(MailjetConstants.ErrorInfo, new JValue(MailjetConstants.InternalServerErrorGeneralMessage));
                }
                else
                {
                    content.Add(MailjetConstants.ErrorInfo, new JValue(responseMessage.ReasonPhrase));
                }
            }

            return content;
        }
        public MailjetResponse Get(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var responseMessage = _httpClient.GetAsync(url).Result;

            JObject content = GetContentResult(responseMessage);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public MailjetResponse Post(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPost = new StringContent(output, Encoding.UTF8, MailjetConstants.JsonMediaType);
            var responseMessage = _httpClient.PostAsync(url, contentPost).Result;

            JObject content = GetContentResult(responseMessage);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public MailjetResponse Put(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPut = new StringContent(output, Encoding.UTF8, MailjetConstants.JsonMediaType);
            var responseMessage = _httpClient.PutAsync(url, contentPut).Result;

            JObject content = GetContentResult(responseMessage);
            MailjetResponse mailjetResponse = new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
            return mailjetResponse;
        }

        public MailjetResponse Delete(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var responseMessage = _httpClient.DeleteAsync(url).Result;

            JObject content = GetContentResult(responseMessage);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }
        private JObject GetContentResult(HttpResponseMessage responseMessage)
        {
            string cnt = null;

            if (responseMessage.Content != null)
            {
                cnt = responseMessage.Content.ReadAsStringAsync().Result;
            }

            JObject content;
            if (!string.IsNullOrEmpty(cnt) && responseMessage.Content.Headers.ContentType.MediaType == MailjetConstants.JsonMediaType)
            {
                content = JObject.Parse(cnt);
            }
            else
            {
                content = new JObject();
                content.Add("StatusCode", new JValue((int)responseMessage.StatusCode));
            }

            if (!responseMessage.IsSuccessStatusCode && !content.ContainsKey(MailjetConstants.ErrorInfo))
            {
                if (responseMessage.StatusCode == ((HttpStatusCode)429))
                {
                    content.Add(MailjetConstants.ErrorInfo, new JValue(MailjetConstants.TooManyRequestsMessage));
                }
                else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    content.Add(MailjetConstants.ErrorInfo, new JValue(MailjetConstants.InternalServerErrorGeneralMessage));
                }
                else
                {
                    content.Add(MailjetConstants.ErrorInfo, new JValue(responseMessage.ReasonPhrase));
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
    }
}
