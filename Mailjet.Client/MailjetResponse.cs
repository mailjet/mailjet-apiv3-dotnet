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
            int total = 0;

            JToken tocken;
            if (Content.TryGetValue("Total", StringComparison.OrdinalIgnoreCase, out tocken))
            {
                total = tocken.Value<int>();
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
    }
}
