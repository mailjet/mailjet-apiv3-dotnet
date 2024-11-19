using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Mailjet.Client.Helpers
{
    public static class HttpContentHelper
    {
        public static async Task<JsonObject> GetContentAsync(HttpResponseMessage responseMessage)
        {
            string cnt = null;

            if (responseMessage.Content != null)
            {
                cnt = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            JsonObject content;
            if (!string.IsNullOrEmpty(cnt) && responseMessage.Content.Headers.ContentType.MediaType == MailjetConstants.JsonMediaType)
            {
                content = JsonObject.Parse(cnt).AsObject();
            }
            else
            {
                content = new JsonObject();
                content.Add("StatusCode", JsonValue.Create((int)responseMessage.StatusCode));
            }

            if (!responseMessage.IsSuccessStatusCode && !content.ContainsKey(MailjetConstants.ErrorInfo))
            {
                if (responseMessage.StatusCode == ((HttpStatusCode)429))
                {
                    content.Add(MailjetConstants.ErrorInfo, JsonValue.Create(MailjetConstants.TooManyRequestsMessage));
                }
                else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    content.Add(MailjetConstants.ErrorInfo, JsonValue.Create(MailjetConstants.InternalServerErrorGeneralMessage));
                }
                else
                {
                    content.Add(MailjetConstants.ErrorInfo, JsonValue.Create(responseMessage.ReasonPhrase));
                }
            }

            return content;
        }
    }
}
