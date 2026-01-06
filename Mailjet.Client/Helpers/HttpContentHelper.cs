using System.Net;
using System.Text.Json.Nodes;

namespace Mailjet.Client.Helpers;

public static class HttpContentHelper
{
    public static async Task<JsonObject> GetContentAsync(HttpResponseMessage responseMessage)
    {
        string? cnt = null;

        if (responseMessage.Content != null)
        {
            cnt = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        JsonObject content;
        if (!string.IsNullOrEmpty(cnt) && responseMessage.Content?.Headers.ContentType?.MediaType == MailjetConstants.JsonMediaType)
        {
            content = JsonNode.Parse(cnt)?.AsObject() ?? new JsonObject();
        }
        else
        {
            content = new JsonObject
            {
                ["StatusCode"] = (int)responseMessage.StatusCode
            };
        }

        if (!responseMessage.IsSuccessStatusCode && !content.ContainsKey(MailjetConstants.ErrorInfo))
        {
            if (responseMessage.StatusCode == ((HttpStatusCode)429))
            {
                content[MailjetConstants.ErrorInfo] = MailjetConstants.TooManyRequestsMessage;
            }
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                content[MailjetConstants.ErrorInfo] = MailjetConstants.InternalServerErrorGeneralMessage;
            }
            else
            {
                content[MailjetConstants.ErrorInfo] = responseMessage.ReasonPhrase;
            }
        }

        return content;
    }
}
