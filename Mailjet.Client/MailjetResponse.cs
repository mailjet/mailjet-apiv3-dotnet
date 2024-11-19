using System;
using System.Text.Json.Nodes;

namespace Mailjet.Client
{
    public class MailjetResponse
    {
        public JsonObject Content { get; private set; }
        public bool IsSuccessStatusCode { get; private set; }
        public int StatusCode { get; private set; }

        public MailjetResponse(bool isSuccessStatusCode, int statusCode, JsonObject content)
        {
            IsSuccessStatusCode = isSuccessStatusCode;
            StatusCode = statusCode;
            Content = content;
        }

        public int GetTotal()
        {
            int total;
            if (!TryGetValue("Total", out total))
            {
                total = 0;
            }

            return total;
        }

        public JsonArray GetData()
        {
            JsonArray result;
            if (TryGetValue("Data", out result))
            {
                return result;
            }

            if (TryGetValue("Sent", out result))
            {
                return result;
            }

            //for Send API v3.1
            if (TryGetValue("Messages", out result))
            {
                return result;
            }

            result = new JsonArray(Content.DeepClone());
            return result;
        }

        public int GetCount()
        {
            int count;
            if (!TryGetValue("Count", out count))
            {
                count = 0;
            }

            return count;
        }

        public string GetErrorInfo()
        {
            if (!TryGetValue(MailjetConstants.ErrorInfo, out string errorInfo))
            {
                errorInfo = string.Empty;
            }

            return errorInfo;
        }

        public string GetErrorMessage()
        {
            if (!TryGetValue("ErrorMessage", out string errorMessage))
            {
                errorMessage = string.Empty;
            }

            return errorMessage;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            JsonNode node;
            if (!Content.TryGetPropertyValue(key, out node))
            {
                value = default(T);
                return false;
            }

            value = node.GetValue<T>();
            return true;
        }

        public bool TryGetValue(string key, out JsonArray value)
        {
            JsonNode node;
            if (!Content.TryGetPropertyValue(key, out node))
            {
                value = null;
                return false;
            }

            value = node.AsArray();
            return true;
        }

        public T GetValue<T>(string key)
        {
            T result;
            if (!TryGetValue(key, out result))
            {
                throw new Exception(string.Format("No entry found for key: {0}", key));
            }

            return result;
        }
    }
}
