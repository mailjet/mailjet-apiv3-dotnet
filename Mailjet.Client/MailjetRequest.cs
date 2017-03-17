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

        // Filters HashMap.
        // Every filter should be string values. So Integer will be cast into Strings
        Dictionary<string, string> _filters = new Dictionary<string, string>();

        // The request body is a JObject that will be cast into a String before the call
        public JObject Body { get; set; } = new JObject();

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
            jObject.ResourceId = ResourceId != null ? ResourceId.Id : string.Empty;
            jObject.ActionID = ActionId.ToString();
            jObject.Body = Body;

            return jObject.ToString();
        }
    }
}
