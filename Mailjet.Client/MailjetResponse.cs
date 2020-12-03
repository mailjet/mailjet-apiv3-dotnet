using Newtonsoft.Json.Linq;
using System;

namespace Mailjet.Client
{
    public class MailjetResponse
    {
        public JObject Content { get; private set; }
        public bool IsSuccessStatusCode { get; private set; }
        public int StatusCode { get; private set; }

        public MailjetResponse(bool isSuccessStatusCode, int statusCode, JObject content)
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

        public JArray GetData()
        {
            JArray result;
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

            result = new JArray(Content);
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
            JToken token;
            if (!Content.TryGetValue(key, StringComparison.OrdinalIgnoreCase, out token))
            {
                value = default(T);
                return false;
            }

            value = token.Value<T>();
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
