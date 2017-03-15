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
        
        // REST/DATA/send API. By default, the constructor will initialize it with REST
        // as most of the resources will use it 
        private string _path;

        // Mailjet resource string
        private string _resource;

        // Mailjet resource action, if any
        private string _action;

        // Resource ID
        private string _id;

        // Resource action ID
        private string _actionId;

        private string _data;

        // Filters HashMap.
        // Every filter should be string values. So Integer will be cast into Strings
        Dictionary<string, string> _filters = new Dictionary<string, string>();

        // The request body is a JObject that will be cast into a String before the call
        JObject _body = new JObject();



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
            _path = "/REST";
            _resource = resourceInfo.Resource;
            _action = resourceInfo.Action;
            _id = id;
            _actionId = actionid;
        }

        public MailjetRequest Property(string key, Object value)
        {
            _body.Add(key, new JValue(value));
            return this;
        }

        public override string ToString()
        {
            dynamic jObject = new JObject();
            jObject.Resource = _resource;
            jObject.ID = _id;
            jObject.ActionID = _actionId;
            jObject.Body = _body;

            return jObject.ToString();
        }
    }
}
