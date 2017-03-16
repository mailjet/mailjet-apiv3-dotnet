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
        public JObject Content { get; set; }

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

            JToken tocken;
            if (Content.TryGetValue("Data", StringComparison.OrdinalIgnoreCase, out tocken))
            {
                result = tocken.Value<JArray>();
            }
            else if (Content.TryGetValue("Sent", StringComparison.OrdinalIgnoreCase, out tocken))
            {
                result = tocken.Value<JArray>();
            }
            else
            {
                result = new JArray();
            }

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

        private bool TryGetValue<T>(string key, out T value)
        {
            JToken tocken;
            if (Content.TryGetValue(key, StringComparison.OrdinalIgnoreCase, out tocken))
            {
                value = tocken.Value<T>();
                return true;
            }

            value = default(T);
            return false;
        }
    }
}
