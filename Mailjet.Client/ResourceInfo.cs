using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
    public class ResourceInfo
    {
        public string Resource { get; private set; }
        public string Action { get; private set; }

        public ResourceInfo(string resource, string action = null)
        {
            Resource = resource;
            Action = action ?? string.Empty;
        }
    }
}
