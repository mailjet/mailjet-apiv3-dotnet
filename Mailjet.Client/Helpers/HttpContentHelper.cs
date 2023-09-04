using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mailjet.Client.Helpers
{
    public static class HttpContentHelper
    {
        public static async Task<JObject> GetContentAsync(HttpResponseMessage responseMessage)
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
    }
}
