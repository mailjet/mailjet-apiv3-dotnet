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
        private ResourceInfo _resourceInfo;

        // Resource ID
        private string _id;

        // Resource action ID
        private string _actionId;

        // Filters HashMap.
        // Every filter should be string values. So Integer will be cast into Strings
        Dictionary<string, string> _filters = new Dictionary<string, string>();

        // The request body is a JObject that will be cast into a String before the call
        public JObject Body { get; set; }

        /// <summary>
        /// Make a Mailjet request with a single resource.
        /// </summary>
        /// <param name="resourceInfo">The resource object</param>
        public MailjetRequest(ResourceInfo resourceInfo)
        {
            InitRequest(resourceInfo);
        }

        /// <summary>
        /// In case of a simple view. Call a single resource with a single ID
        /// </summary>
        /// <param name="resourceInfo">Resource object</param>
        /// <param name="id">Resource ID</param>
        public MailjetRequest(ResourceInfo resourceInfo, string id)
        {
            InitRequest(resourceInfo, id);
        }

        /// <summary>
        /// In case of a simple view. Call a single resource with a single ID
        /// </summary>
        /// <param name="resourceInfo">Resource object</param>
        /// <param name="id">Resource ID</param>
        public MailjetRequest(ResourceInfo resourceInfo, long id)
        {
            InitRequest(resourceInfo, id.ToString());
        }

        /// <summary>
        /// Build a Request with an actionID
        /// </summary>
        /// <param name="resourceInfo">Resource object</param>
        /// <param name="id">Resource ID</param>
        /// <param name="actionid">Resource action ID</param>
        public MailjetRequest(ResourceInfo resourceInfo, long id, long actionid)
        {
            InitRequest(resourceInfo, id.ToString(), actionid.ToString());
        }

        private void InitRequest(ResourceInfo resourceInfo, string id = null, string actionid = null)
        {
            _resourceInfo = resourceInfo;
            _id = id;
            _actionId = actionid;
            Body = new JObject();
        }

        public MailjetRequest Property(string key, Object value)
        {
            Body.Add(key, new JValue(value));
            return this;
        }

        public string BuildUrl()
        {
            return _resourceInfo.BuildUrl(_id, _actionId);
        }

        public override string ToString()
        {
            dynamic jObject = new JObject();
            jObject.Resource = _resourceInfo;
            jObject.ID = _id;
            jObject.ActionID = _actionId;
            jObject.Body = Body;

            return jObject.ToString();
        }
    }
}
