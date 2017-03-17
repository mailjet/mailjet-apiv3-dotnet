using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
    public class MailjetRequest
    {
        // Mailjet resource string
        public ResourceInfo Resource { get; set; }

        // Resource ID
        public ResourceId ResourceId { get; set; }

        // Resource action ID
        public long? ActionId { get; set; }

        // Every filter should be string values. So Integer will be cast into Strings
        public IDictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

        // The request body is a JObject that will be cast into a String before the call
        public JObject Body { get; set; } = new JObject();

        public MailjetRequest Filter(string key, string value)
        {
            Filters.Add(key, value);
            return this;
        }

        public MailjetRequest Filter(string key, int value)
        {
            return Filter(key, value.ToString());
        }

        public MailjetRequest Property(string key, Object value)
        {
            Body.Add(key, new JValue(value));
            return this;
        }

        public string BuildUrl()
        {
            string resourceId = ResourceId != null ? ResourceId.Id : null;
            string actionId = ActionId.HasValue ? ActionId.Value.ToString() : null;

            return Resource.BuildUrl(resourceId, actionId);
        }

        public override string ToString()
        {
            dynamic jObject = new JObject();
            jObject.Resource = Resource;
            jObject.ResourceId = ResourceId != null ? ResourceId.Id : null;
            jObject.ActionID = ActionId.HasValue ? ActionId.Value.ToString() : null;
            jObject.Filters = Filters;
            jObject.Body = Body;

            return jObject.ToString();
        }
    }
}
