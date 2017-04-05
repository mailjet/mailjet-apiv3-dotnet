using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Mailjet.Client
{
    public class MailjetClientHandler : HttpClientHandler
    {
        public MailjetClientHandler()
        {
        }

        public MailjetClientHandler(string proxyUri)
        {
            Proxy = new DefaultProxy(proxyUri);
            UseProxy = true;
        }
    }
}
