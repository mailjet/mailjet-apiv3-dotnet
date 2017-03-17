using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
    public class MailjetResponse
    {
        private JObject _content;

        public bool IsSuccessStatusCode { get; private set; }
        public int StatusCode { get; private set; }

        public MailjetResponse(bool isSuccessStatusCode, int statusCode, JObject content)
        {
            IsSuccessStatusCode = isSuccessStatusCode;
            StatusCode = statusCode;
            _content = content;
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

            result = new JArray();
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
            string errorInfo;
            if (!TryGetValue("ErrorInfo", out errorInfo))
            {
                errorInfo = string.Empty;
            }

            return errorInfo;
        }

        public string GetErrorMessage()
        {
            string errorMessage;
            if (!TryGetValue("ErrorMessage", out errorMessage))
            {
                errorMessage = string.Empty;
            }

            return errorMessage;
        }

    public bool TryGetValue<T>(string key, out T value)
        {
            JToken token;
            if (!_content.TryGetValue(key, StringComparison.OrdinalIgnoreCase, out token))
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
