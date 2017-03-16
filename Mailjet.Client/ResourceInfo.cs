using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
    public class ResourceInfo
    {
        // REST/DATA/send API. By defaul initialized to REST as most of the resources will use it 
        protected const string _path = "/REST";

        public string Resource { get; private set; }

        // Mailjet resource action, if any
        public string Action { get; private set; }

        public ResourceInfo(string resource, string action = null)
        {
            Resource = resource;
            Action = action ?? string.Empty;
        }

        public virtual string BuildUrl(string resourceId, string actionId)
        {
            string url = CombineUri(_path, Resource);

            if (!string.IsNullOrEmpty(resourceId))
            {
                url = CombineUri(url, resourceId);
            }

            if (!string.IsNullOrEmpty(Action))
            {
                url = CombineUri(url, Action);
            }

            if (!string.IsNullOrEmpty(actionId))
            {
                url = CombineUri(url, actionId);
            }

            return url;
        }

        protected string CombineUri(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }
    }
}
